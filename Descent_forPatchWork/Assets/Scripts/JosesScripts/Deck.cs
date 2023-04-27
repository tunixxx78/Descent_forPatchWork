using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Deck : MonoBehaviour
{
    private List<Card> Cards;
    private List<Card> Discard;
    private void Awake()
    {
        Cards = new List<Card>();
        Discard = new List<Card>();
    }

    public void ShuffleCards(List<Card> ListToShuffle)
    {
        System.Random random = new System.Random();

        for (int i = 0; i < ListToShuffle.Count; i++)
        {
            int j = random.Next(i, ListToShuffle.Count);
            (ListToShuffle[j], ListToShuffle[i]) = (ListToShuffle[i], ListToShuffle[j]);
        }
    }
    public Card DrawCard()
    {
        Card card = Cards[0];
        Cards.RemoveAt(0);
        Discard.Add(card);
        return card;
    }

    // Cardtypes: "Skillcards", "FateCards", "BattleEvent".
    public void GetCards(string characterName, string cardType)
    {
        Card[] allCards = Resources.LoadAll<Card>($"SOCards/{characterName}/{cardType}" );
        Cards.Clear();
        Cards = allCards.ToList();
    }
    public List<Card> GetCardList()
    {
        return Cards;
    }
    public void PlayCard(Card card)
    {
        Discard.Add(card);
        Cards.Remove(card);
    }
}
