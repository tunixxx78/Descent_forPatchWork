using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class OrderCards : MonoBehaviour
{
    public Deck Player1Deck;
    public GameObject cardPref;
    private void Start()
    {
        Player1Deck = gameObject.AddComponent<Deck>();

        // These are created here for testing purposes for now
        Player1Deck.GetFaithCards("Hero1");
        Player1Deck.ShuffleFaithCards();
        Player1Deck.GetSkillCards("Hero1");
        Player1Deck.ShuffleSkillCards();

        for (var i = 0; i < Player1Deck.GetSkillCardList().Count; i++)
        {
            cardPref.GetComponent<Image>().sprite = Player1Deck.GetSkillCardList()[i].sprite;
            GameObject currentCard = Instantiate(cardPref, transform.parent);
            currentCard.transform.parent = this.transform.Find("CardPanel/Scroll View/Viewport/Content");
        }

    }
}
