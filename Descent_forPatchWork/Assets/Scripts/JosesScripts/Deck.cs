using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Deck : MonoBehaviour
{
    //public static Deck Instance;
    private List<SkillCard> SkillCards;
    private List<SkillCard> SkillCardsDiscard;

    private List<FaithCard> FaithCards;
    private List<FaithCard> FaithCardsDiscard;

    //private void Awake()
    //{
    //    if(Instance != null && Instance != this)
    //    {
    //        Destroy(this);
    //    }
    //    else
    //    {
    //        Instance = this;
    //    }
    //}
    private void Awake()
    {
        SkillCards = new List<SkillCard>();
        SkillCardsDiscard = new List<SkillCard>();
        FaithCards = new List<FaithCard>();
        FaithCardsDiscard = new List<FaithCard>();
    }
    public void GenerateFaithCards()
    {
        for (int i = -2; i <= 2; i++)
        {
            FaithCard newCard = gameObject.AddComponent<FaithCard>();
            newCard.SetFaithValue(i);
            for (int j = 0; j != 10; j++, FaithCards.Add(newCard));
        }
    }

    public void ShuffleSkillCards()
    {
        System.Random random = new System.Random();

        for (int i = 0; i < SkillCards.Count; i++)
        {
            int j = random.Next(i, SkillCards.Count);
            (SkillCards[j], SkillCards[i]) = (SkillCards[i], SkillCards[j]);
        }
    }

    public void ShuffleFaithCards()
    {
        System.Random random = new System.Random();

        for (int i = 0; i < FaithCards.Count; i++)
        {
            int j = random.Next(i, FaithCards.Count);
            (FaithCards[j], FaithCards[i]) = (FaithCards[i], FaithCards[j]);
        }
    }

    public SkillCard DrawSkillCard()
    {
        if (SkillCards.Count == 0) {
            // Kortit loppu
            // return null;
            SkillCards = SkillCardsDiscard;
            SkillCardsDiscard.Clear();
            ShuffleSkillCards();

        }
        SkillCard card = SkillCards[0];
        SkillCards.RemoveAt(0);
        // Tämä tehtäisiin vasta kun kortti pelataan pelaajan kädestä
        SkillCardsDiscard.Add(card);

        return card;
    }

    public FaithCard DrawFaithCard()
    {
        if (FaithCards.Count == 0) {
            // Kortit loppu
            // return null;
            FaithCards = FaithCardsDiscard;
            FaithCardsDiscard.Clear();
            ShuffleFaithCards();
        }
        FaithCard card = FaithCards[0];
        FaithCards.RemoveAt(0);
        FaithCardsDiscard.Add(card);

        return card;
    }

    public void GetHeroSkillCards(string characterName)
    {
        // GUID tarkoittaa Globally Unique Identifier ja SO = Scriptable Object
        string[] guids = AssetDatabase.FindAssets(characterName, new[] { "Assets/SOCards/"+characterName });
        SkillCards.Clear();
        foreach (string SO in guids)
        {   
            string path = AssetDatabase.GUIDToAssetPath(SO);
            SkillCard sCard = AssetDatabase.LoadAssetAtPath<SkillCard>(path);
            SkillCards.Add(sCard);
        }
    }

}
