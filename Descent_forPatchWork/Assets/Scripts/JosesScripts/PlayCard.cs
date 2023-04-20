using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayCard : MonoBehaviour
{
    [SerializeField]
    private Button playButton, cancelButton;
    private string currentCard;
    void Awake()
    {
        playButton = playButton.GetComponent<Button>();  
        cancelButton = cancelButton.GetComponent<Button>();
    }

    public void PlayOrCancel(string cardId)
    {
        currentCard = cardId;
        playButton.onClick.RemoveAllListeners();
        cancelButton.onClick.RemoveAllListeners();
        playButton.onClick.AddListener(SelectPlay);
        cancelButton.onClick.AddListener(SelectCancel);
    }
    private void SelectPlay()
    {
        this.transform.parent.parent.GetComponent<OrderCards>().HandleCardPlay(currentCard);
        this.gameObject.SetActive(false);
        GameObject.Destroy(this.transform.GetChild(this.transform.childCount-1).gameObject);
    }
    private void SelectCancel()
    {
        this.gameObject.SetActive(false);
        GameObject.Destroy(this.transform.GetChild(this.transform.childCount-1).gameObject);
    }
}
