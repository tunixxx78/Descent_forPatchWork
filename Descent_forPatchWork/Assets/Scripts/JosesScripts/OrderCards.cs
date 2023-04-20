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
        for (int i = 0; i < GameManager.gm.heroesInGame.Count; i++)
        {
            Button button = playerCardButtons[i].GetComponent<Button>();
            button.onClick.AddListener(() => { SelectedPlayer(button); });
            button.gameObject.SetActive(true);
            playerButtonDict.Add(button, GameManager.gm.heroesInGame[i].GetComponent<HeroOne>().hc);

        }

        ShowAllCards = ShowAllCards.GetComponent<Button>();
        ShowHandCards = ShowHandCards.GetComponent<Button>();
        ShowFateCards = ShowFateCards.GetComponent<Button>();

        ShowAllCards.onClick.AddListener(() => { SelectedCardType(ShowAllCards); });
        ShowHandCards.onClick.AddListener(() => { SelectedCardType(ShowHandCards); });
        ShowFateCards.onClick.AddListener(() => { SelectedCardType(ShowFateCards); });

        InitializeCards();
        HandleCardPrinting();
    }

    private void InitializeCards()
    {
        playerButtonDict.TryGetValue(playerCardButtons[0], out currentHeroCards);
        cardsShown = "HandCards";
    }
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
            printedCards = currentHeroCards.SkillCards.GetCardList().Concat(currentHeroCards.FateCards.GetCardList()).ToList();
        }
        else if (cardsShown == "HandCards")
        {
            printedCards = currentHeroCards.SkillCards.GetCardList();
        }
        else if (cardsShown == "FateCards")
        {
            printedCards = currentHeroCards.FateCards.GetCardList();
        }
        ShowCards(printedCards);
    }

    // Prints the cards given in a list by the param
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

    // 
    public void HandleCardPlay(string cardId)
    {

        foreach(Card card in currentHeroCards.SkillCards.GetCardList())
        {
            if (card.cardId == cardId)
            {
                currentHeroCards.SkillCards.PlayCard(card);
                HandleCardPrinting();
                return;
            }
        }
        foreach(Card card in currentHeroCards.FateCards.GetCardList())
        {
            if (card.cardId == cardId)
            {
                currentHeroCards.FateCards.PlayCard(card);
                HandleCardPrinting();
                return;
            }
        }
        Debug.Log("Did not return.");
    }

}
