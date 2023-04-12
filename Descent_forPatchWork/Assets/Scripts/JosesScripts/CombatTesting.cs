using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatTesting : MonoBehaviour
{
    public Deck Player1Deck;
    // Start is called before the first frame update
    private void Start()
    {

        Player1Deck = gameObject.AddComponent<Deck>();
        Player1Deck.GenerateFaithCards();
        Player1Deck.ShuffleFaithCards();

        // Get individual hero's cards by their name
        Player1Deck.GetHeroSkillCards("Hero1");
        Player1Deck.ShuffleSkillCards();


        SkillCard card = Player1Deck.DrawSkillCard();
        Debug.Log($"Card information Skill ID: {card.skillId} Card ATK: + {card.atk} Skill Heal: {card.heal} Long Range/Close Range: {card.longRange}/{card.closeRange} Card info: {card.info}");

        int faithValue = Player1Deck.DrawFaithCard().GetFaithValue();
        Debug.Log($"Faith card drawn was: {faithValue} ATK is now: {(card.atk + faithValue)}");
        
    }

}
