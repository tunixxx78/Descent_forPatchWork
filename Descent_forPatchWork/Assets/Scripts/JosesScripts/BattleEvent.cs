using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BattleEvent : MonoBehaviour
{   
    public List<Card> cards;
    public Button drawButton;
    private Image currentSprite;
    [SerializeField] private GameObject spawnPoint;

    void Awake()
    {
    
    }

    private void Start()
    {
        drawButton.onClick.AddListener(DrawCard);
    }

    public void DrawCard()
    {
        if(cards.Count != 0)
        {
            Card card = cards[Random.Range(0, cards.Count)];
            Image img = spawnPoint.GetComponent<Image>();
            img.sprite = card.sprite;
            img.color = new Color(img.color.r, img.color.g, img.color.b, 1f);
            cards.Remove(card);
        }
        else
        {
            Image img = spawnPoint.GetComponent<Image>();
            img.color = new Color(img.color.r, img.color.g, img.color.b, 0f);
        }
    }

}
