using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class OrderCards : MonoBehaviour
{
    struct PLR
    {
        public Deck SkillCards;
        public Deck FateCards;
    }

    public GameObject cardPref;

    public string currentPlayer, cardsShown;

    public Button plr1, plr2, plr3, plr4, ShowAllCards, ShowHandCards, ShowFateCards;

    Deck SkillCardsInit1;
    Deck SkillCardsInit2;
    Deck FateCardsInit1;
    Deck FateCardsInit2;

    PLR Player1Cards;
    PLR Player2Cards;
    PLR Player3Cards;
    PLR Player4Cards;


    IDictionary<string, PLR> stringPlayer = new Dictionary<string, PLR>();

    private void Awake()
    {
        Player1Cards = new PLR();
        Player2Cards = new PLR();
        Player3Cards = new PLR();
        Player4Cards = new PLR();


        SkillCardsInit1 = gameObject.AddComponent<Deck>();
        SkillCardsInit2 = gameObject.AddComponent<Deck>();
        FateCardsInit1 = gameObject.AddComponent<Deck>();
        FateCardsInit2 = gameObject.AddComponent<Deck>();


        Player1Cards.SkillCards = SkillCardsInit1;
        Player1Cards.FateCards = FateCardsInit1;
        Player2Cards.SkillCards = SkillCardsInit2;
        Player2Cards.FateCards = FateCardsInit2;

        Player1Cards.SkillCards.GetCards("Hero1", "Skillcards");
        Player1Cards.FateCards.GetCards("Hero1", "Fatecards");
        Player2Cards.SkillCards.GetCards("Boss1", "Skillcards");
    }
    private void Start()
    {
        plr1 = plr1.GetComponent<Button>();
        plr2 = plr2.GetComponent<Button>();
        plr3 = plr3.GetComponent<Button>();
        plr4 = plr4.GetComponent<Button>();
        ShowAllCards = ShowAllCards.GetComponent<Button>();
        ShowHandCards = ShowHandCards.GetComponent<Button>();
        ShowFateCards = ShowFateCards.GetComponent<Button>();

        plr1.onClick.AddListener(() => { SelectedPlayer(plr1); });
        plr2.onClick.AddListener(() => { SelectedPlayer(plr2); });
        plr3.onClick.AddListener(() => { SelectedPlayer(plr3); });
        plr4.onClick.AddListener(() => { SelectedPlayer(plr4); });
        ShowAllCards.onClick.AddListener(() => { SelectedCardType(ShowAllCards); });
        ShowHandCards.onClick.AddListener(() => { SelectedCardType(ShowHandCards); });
        ShowFateCards?.onClick.AddListener(() => { SelectedCardType(ShowFateCards); });

        stringPlayer.Add("Player1", Player1Cards);
        stringPlayer.Add("Player2", Player2Cards);
        stringPlayer.Add("Player3", Player3Cards);
        stringPlayer.Add("Player4", Player4Cards);


        currentPlayer = "Player1";
        cardsShown = "HandCards";

        HandleCardPrinting();
    }
    
    
    private void SelectedPlayer(Button btn)
    {
        if(btn.transform.name == "Player1")
        {
            currentPlayer = "Player1";
        }else if(btn.transform.name == "Player2")
        {
            currentPlayer = "Player2";
        }else if(btn.transform.name == "Player3")
        {
            currentPlayer = "Player3";
        }else if(btn.transform.name == "Player4")
        {
            currentPlayer = "Player4";
        }
        HandleCardPrinting();
    }

    private void SelectedCardType(Button btn)
    {
        if (btn.transform.name == "AllCards")
        {
            cardsShown = "AllCards";
            // Lis‰‰ fate kortit viel‰ mutta nyt ei ole 
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


    private void HandleCardPrinting()
    {
        PLR x = new PLR();
        stringPlayer.TryGetValue(currentPlayer, out x);
        List<Card> printedCards = new List<Card>();

        if (cardsShown == "AllCards")
        {
            printedCards = x.SkillCards.GetCardList().Concat(x.FateCards.GetCardList()).ToList();
        }
        else if (cardsShown == "HandCards")
        {
            printedCards = x.SkillCards.GetCardList();
        }
        else if (cardsShown == "FateCards")
        {
            printedCards = x.FateCards.GetCardList();
        }
        ShowCards(printedCards);
    }

    public void ShowCards(List<Card> cardList)
    {
        // Tyhjennet‰‰n ennenkuin printataan uudellleen.
        GameObject gameObject = this.transform.Find("CardPanel/Scroll View/Viewport/Content").gameObject;
        foreach(Transform child in gameObject.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        for (int i = 0; i < cardList.Count; i++)
        {
            cardPref.GetComponent<Image>().sprite = cardList[i].sprite;
            GameObject currentCard = Instantiate(cardPref, transform.parent);
            currentCard.transform.SetParent(this.transform.Find("CardPanel/Scroll View/Viewport/Content"));
        }
    }

}
