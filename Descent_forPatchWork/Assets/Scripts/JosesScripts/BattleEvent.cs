using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BattleEvent : MonoBehaviour
{   
    private Deck deck;
    private int index;
    [SerializeField] private GameObject spawnPoint;

    void Awake()
    {
        deck = gameObject.AddComponent<Deck>();
    }

    private void Start()
    {
 
    }

    public void DrawCard()
    {
        index = 1;
        if (deck.GetCardList().Count > 0)
        {
            ShowCard(deck.DrawCard());
        }
    }
    public void DrawPreviousCards()
    {
        if (deck.GetDiscardList().Count != 0 && deck.GetDiscardList().Count - index - 1 >= 0)
        {
            index++;
            ShowCard(deck.GetDiscardList()[^index]);
        }
        
    }

    public void ShowCard(Card card) {
        if (card != null)
        {
            Image img = spawnPoint.GetComponent<Image>();
            img.sprite = card.sprite;
            img.color = new Color(img.color.r, img.color.g, img.color.b, 1f);
        }
        else
        {
            Image img = spawnPoint.GetComponent<Image>();
            img.color = new Color(img.color.r, img.color.g, img.color.b, 0f);
        }
    }

    public void Shuffle()
    {
        deck.DiscardToCards();
        deck.ShuffleCards(deck.GetCardList());
        Image img = spawnPoint.GetComponent<Image>();
        img.color = new Color(img.color.r, img.color.g, img.color.b, 0f);
    }

    public void InitializeBattleEvent()
    {
        deck.GetCardList().Clear();
        deck.GetDiscardList().Clear();
        deck.GetCards("BattleEvent", GameManager.gm.currentMissionIndex.ToString());
        Image img = spawnPoint.GetComponent<Image>();
        img.color = new Color(img.color.r, img.color.g, img.color.b, 0f);
        index = 1;
    }
}
