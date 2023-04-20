using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class OrderCards : MonoBehaviour
{
    public GameObject cardPref;
    public string cardsShown;
    public Button ShowAllCards, ShowHandCards, ShowFateCards;
    public List<Button> playerCardButtons;

    private HeroCards currentHeroCards;
    private IDictionary<Button, HeroCards> playerButtonDict;

    private void Awake()
    {
        playerButtonDict = new Dictionary<Button, HeroCards>();
    }
    private void Start()
    {
        InitializeCards();
        InitializeButtons();
        
        ShowAllCards = ShowAllCards.GetComponent<Button>();
        ShowHandCards = ShowHandCards.GetComponent<Button>();
        ShowFateCards = ShowFateCards.GetComponent<Button>();

        ShowAllCards.onClick.AddListener(() => { SelectedCardType(ShowAllCards); });
        ShowHandCards.onClick.AddListener(() => { SelectedCardType(ShowHandCards); });
        ShowFateCards.onClick.AddListener(() => { SelectedCardType(ShowFateCards); });

        playerButtonDict.TryGetValue(playerCardButtons[0], out currentHeroCards);
        cardsShown = "HandCards";

        HandleCardPrinting();
    }

    // Gets each currently in the game hero's skillcards and fatecards
    public void InitializeCards()
    {
        for(int i = 0; i < GameManager.gm.heroesInGame.Count; i++)
        {
            if(i == 0)
            {
                GameManager.gm.heroesInGame[i].GetComponent<HeroOne>().hc.skillCards.GetCards("Turo", "Skillcards");
                GameManager.gm.heroesInGame[i].GetComponent<HeroOne>().hc.fateCards.GetCards("Turo", "Fatecards");
            }
            else
            {
                GameManager.gm.heroesInGame[i].GetComponent<HeroOne>().hc.skillCards.GetCards("Tero", "Skillcards");
                GameManager.gm.heroesInGame[i].GetComponent<HeroOne>().hc.fateCards.GetCards("Tero", "Fatecards");
            }
        }
        /* 
        foreach(GameObject gameObject in GameManager.gm.heroesInGame)
        {
            gameObject.GetComponent<HeroOne>().hc.SkillCards.GetCards(gameObject.GetComponent<HeroOne>().hb.plrName, "Skillcards");
            gameObject.GetComponent<HeroOne>().hc.SkillCards.GetCards(gameObject.GetComponent<HeroOne>().hb.plrName, "Fatecards");
        }
        */
    }
    // Sets active the correct amount of player buttons and inserts buttons as keys to a dictionary with the corresponding decks as values
    public void InitializeButtons()
    {
        playerButtonDict.Clear();
        for (int i = 0; i < GameManager.gm.heroesInGame.Count; i++)
        {
            Button button = playerCardButtons[i].GetComponent<Button>();
            button.onClick.AddListener(() => { SelectedPlayer(button); });
            button.gameObject.SetActive(true);
            playerButtonDict.Add(button, GameManager.gm.heroesInGame[i].GetComponent<HeroOne>().hc);
        }
    }
    // Changes which player cards are being shown
    private void SelectedPlayer(Button btn)
    {
        playerButtonDict.TryGetValue(btn, out currentHeroCards);
        HandleCardPrinting();
    }

    // Switch for the type of cards shown in cardview.
    private void SelectedCardType(Button btn)
    {
        if (btn.transform.name == "AllCards")
        {
            cardsShown = "AllCards";
        }
        else if (btn.transform.name == "HandCards")
        {
            cardsShown = "HandCards";
        }
        else if(btn.transform.name == "FateCards")
        {
            cardsShown = "FateCards";
        }
        HandleCardPrinting();
    }

    // Handling method to get the wanted list of cards to ShowCards() method
    public void HandleCardPrinting()
    {
        List<Card> printedCards = new();
        if (cardsShown == "AllCards")
        {
            printedCards = currentHeroCards.skillCards.GetCardList().Concat(currentHeroCards.fateCards.GetCardList()).ToList();
        }
        else if (cardsShown == "HandCards")
        {
            printedCards = currentHeroCards.skillCards.GetCardList();
        }
        else if (cardsShown == "FateCards")
        {
            printedCards = currentHeroCards.fateCards.GetCardList();
        }
        ShowCards(printedCards);
    }

    // Prints the cards to given to CardView
    public void ShowCards(List<Card> cardList)
    {
        // Emptying the content before adding updated content
        GameObject gameObject = this.transform.Find("Container/CardPanel/Scroll View/Viewport/Content").gameObject;
        foreach(Transform child in gameObject.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        for (int i = 0; i < cardList.Count; i++)
        {
            cardPref.GetComponent<Image>().sprite = cardList[i].sprite;
            cardPref.GetComponent<InteractableCard>().cardId = cardList[i].cardId;
            GameObject currentCard = Instantiate(cardPref, transform.parent);
            currentCard.transform.SetParent(gameObject.transform);
        }
    }

    // Loop through player card lists and "play" the first correct card found
    public void HandleCardPlay(string cardId)
    {
        
        foreach(Card card in currentHeroCards.skillCards.GetCardList())
        {
            if (card.cardId == cardId)
            {
                currentHeroCards.skillCards.PlayCard(card);
                HandleCardPrinting();
                return;
            }
        }
        foreach(Card card in currentHeroCards.fateCards.GetCardList())
        {
            if (card.cardId == cardId)
            {
                currentHeroCards.fateCards.PlayCard(card);
                HandleCardPrinting();
                return;
            }
        }
        Debug.Log("Did not return.");
    }

}
