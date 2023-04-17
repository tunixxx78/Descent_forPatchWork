using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.TextCore.Text;

class GenericList<T>
{
    public List<T> list;
}
public class Deck : MonoBehaviour
{
    public List<SkillCard> SkillCards;
    public List<SkillCard> SkillCardsDiscard;

    public List<FaithCard> FaithCards;
    public List<FaithCard> FaithCardsDiscard;

    private void Awake()
    {
        SkillCards = new List<SkillCard>();
        SkillCardsDiscard = new List<SkillCard>();
        FaithCards = new List<FaithCard>();
        FaithCardsDiscard = new List<FaithCard>();
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
    public void GetFaithCards(string characterName)
    {
        // GUID tarkoittaa Globally Unique Identifier ja SO = Scriptable Object
        string[] guids = AssetDatabase.FindAssets(characterName, new[] { "Assets/SOCards/" + characterName + "/Faithcards" });
        SkillCards.Clear();
        foreach (string SO in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(SO);
            SkillCard sCard = AssetDatabase.LoadAssetAtPath<SkillCard>(path);
            SkillCards.Add(sCard);
        }
    }
    public void GetSkillCards(string characterName)
    {
        // GUID tarkoittaa Globally Unique Identifier ja SO = Scriptable Object
        string[] guids = AssetDatabase.FindAssets(characterName, new[] { "Assets/SOCards/" + characterName + "/Skillcards" });
        SkillCards.Clear();
        foreach (string SO in guids)
        {   
            string path = AssetDatabase.GUIDToAssetPath(SO);
            SkillCard sCard = AssetDatabase.LoadAssetAtPath<SkillCard>(path);
            SkillCards.Add(sCard);
        }
    }
    public List<SkillCard> GetSkillCardList()
    {
        return SkillCards;
    }
    public List<FaithCard> GetFaithCardList()
    {
        return FaithCards;
    }
}
