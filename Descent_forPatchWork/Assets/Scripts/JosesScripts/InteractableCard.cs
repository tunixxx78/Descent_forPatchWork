using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractableCard : MonoBehaviour
{
    private GameObject copy;
    public string cardId;

    public void SelectOnClick()
    {
        copy = new GameObject();
        copy.AddComponent<Image>().sprite = this.gameObject.GetComponent<Image>().sprite;
        copy.GetComponent<Image>().rectTransform.sizeDelta = new Vector2(250, 340);
        copy.GetComponent<Image>().rectTransform.localScale = new Vector3(2, 2, 1); 
        copy.GetComponent<Image>().rectTransform.position = new Vector3(Screen.width * 0.5f, Screen.height * 0.6f, 0);
        copy.transform.SetParent(GameObject.Find("/Canvas/MapPanel/CardView/Container/PlayCardPanel").transform);
        GameObject PlayCardPanel = GameObject.Find("/Canvas/MapPanel/CardView/Container/PlayCardPanel");
        PlayCardPanel.SetActive(true);
        PlayCardPanel.GetComponent<PlayCard>().PlayOrCancel(cardId);

    }
}
