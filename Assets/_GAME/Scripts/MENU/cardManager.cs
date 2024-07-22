using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class cardManager : MonoBehaviour
{
    //cardScroll deck bölmelerini temsil ediyor
    //cards panel kartların açıklamalarını, cards kartların prefablarını temsil ediyor
    [Header("Elements")]
    [SerializeField] private Transform cardScroll1;
    [SerializeField] private Transform cardScroll2;
    [SerializeField] private GameObject[] cardsPanel;
    [SerializeField] private GameObject[] cards;
    [SerializeField] private GameObject[] cardsBuyButton;

    menuManager menuManager;

    [Header("Action")]
    public static Action onOpenCards;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("starPower1")&& !PlayerPrefs.HasKey("starPower2")) //oyuna ilk kez girildiğinde ilk 2 skill veriliyor
        {
            PlayerPrefs.SetInt("starPower1", 1);
            PlayerPrefs.SetInt("starPower2", 0);
        }

        //satın alınmışsa skill button kapatılıyor
        for (int i = 0; i < cardsBuyButton.Length; i++) 
        {
            if (PlayerPrefs.GetInt("cardBuy" + i)==1)
            {
                cardsBuyButton[i].SetActive(false);
            }
            else
            {
                cardsBuyButton[i].SetActive(true);
            }
        }
        
    }

    private void Start()
    {

        switch (PlayerPrefs.GetInt("starPower1"))
        {
            case 0:
                Instantiate(cards[0], cardScroll1); 
                break;
            case 1:
                Instantiate(cards[1], cardScroll1);
                break;
            case 2:
                Instantiate(cards[2], cardScroll1);
                break;
            case 3:
                Instantiate(cards[3], cardScroll1);
                break;
            case 4:
                Instantiate(cards[4], cardScroll1);
                break;
            case 5:
                Instantiate(cards[5], cardScroll1);
                break;
        }

        switch (PlayerPrefs.GetInt("starPower2"))
        {
            case 0:
                Instantiate(cards[0], cardScroll2);
                break;
            case 1:
                Instantiate(cards[1], cardScroll2);
                break;
            case 2:
                Instantiate(cards[2], cardScroll2);
                break;
            case 3:
                Instantiate(cards[3], cardScroll2);
                break;
            case 4:
                Instantiate(cards[4], cardScroll2);
                break;
            case 5:
                Instantiate(cards[5], cardScroll2);
                break;
        }
        menuManager = GameObject.FindGameObjectWithTag("menuManager").GetComponent<menuManager>();
    }

    public void cardPanelOn(int cardNumber) 
    {
        for (int i = 0; i < cardsPanel.Length; i++)
        {
            cardsPanel[i].SetActive(i == cardNumber);
        }

    }
    public void cardPanelOff(int cardNumber)
    {
        cardsPanel[cardNumber].SetActive(false);
    }


    public void cardSlot1(int cardNumbers)
    {
        int childCount = cardScroll1.childCount;

        if (cardScroll1.childCount==0 && PlayerPrefs.GetInt("starPower2") != cardNumbers) //slot boşsa ve starpower2 de aynı kart yoksa
        {
            Instantiate(cards[cardNumbers], cardScroll1);
            PlayerPrefs.SetInt("starPower1", cardNumbers);
        }
        else if(cardScroll1.childCount != 0 && PlayerPrefs.GetInt("starPower2") != cardNumbers) //slot doluysa ve starpower2 de aynı kart yoksa
        {
            for (int i = 0; i < childCount; i++)
            {
                cardScroll1.GetChild(i).gameObject.SetActive(false);
            }
            Instantiate(cards[cardNumbers], cardScroll1);
            PlayerPrefs.SetInt("starPower1", cardNumbers);
        } 
        else if (cardScroll1.childCount == 0 && PlayerPrefs.GetInt("starPower2") == cardNumbers) //slot boşsa ama starpower2 de aynı kart varsa
        {
            StartCoroutine(menuManager.popUpCreat("This skill is already in the deck"));
        }
        else if (cardScroll1.childCount != 0 && PlayerPrefs.GetInt("starPower2") == cardNumbers) //slot dolu ve starpower2 de aynı kart varsa
        {
            StartCoroutine(menuManager.popUpCreat("This skill is already in the deck"));
        }

    }
    public void cardSlot2(int cardNumbers)
    {
        int childCount = cardScroll2.childCount;

        if (cardScroll2.childCount==0&&PlayerPrefs.GetInt("starPower1")!=cardNumbers)
        {
            Instantiate(cards[cardNumbers], cardScroll2);
            PlayerPrefs.SetInt("starPower2", cardNumbers);
        }
        else if(cardScroll2.childCount != 0 && PlayerPrefs.GetInt("starPower1") != cardNumbers)
        {
            for (int i = 0; i < childCount; i++)
            {
                cardScroll2.GetChild(i).gameObject.SetActive(false);
            }
            Instantiate(cards[cardNumbers], cardScroll2);
            PlayerPrefs.SetInt("starPower2", cardNumbers);
        }
        else if (cardScroll1.childCount == 0 && PlayerPrefs.GetInt("starPower1") == cardNumbers)
        {
            StartCoroutine(menuManager.popUpCreat("This skill is already in the deck"));
        }
        else if (cardScroll1.childCount != 0 && PlayerPrefs.GetInt("starPower1") == cardNumbers)
        {
            StartCoroutine(menuManager.popUpCreat("This skill is already in the deck"));
        }

    }

    public void cardBuyCommon(int cardNumber)
    {
        
        if (PlayerPrefs.GetInt("gold") >= 1000)
        {
            PlayerPrefs.SetInt("cardBuy" + cardNumber, 1);
            int money = PlayerPrefs.GetInt("gold");
            PlayerPrefs.SetInt("gold", money - 1000);
            menuManager.goldText.text = PlayerPrefs.GetInt("gold").ToString();

            EventSystem.current.currentSelectedGameObject.SetActive(false);

            onOpenCards?.Invoke();
        }
        else
        {
            StartCoroutine(menuManager.popUpCreat("YOU DON'T HAVE ENOUGH GOLD TO BUY"));
        }
    }
    
    public void cardBuyEpic(int cardNumber)
    {
        
        if (PlayerPrefs.GetInt("gold") >= 3000)
        {
            PlayerPrefs.SetInt("cardBuy" + cardNumber, 1);
            int money = PlayerPrefs.GetInt("gold");
            PlayerPrefs.SetInt("gold", money - 3000);
            menuManager.goldText.text = PlayerPrefs.GetInt("gold").ToString();

            EventSystem.current.currentSelectedGameObject.SetActive(false);

            onOpenCards?.Invoke();
        }
        else
        {
            StartCoroutine(menuManager.popUpCreat("YOU DON'T HAVE ENOUGH GOLD TO BUY"));
        }
    }
    public void cardBuyLegendary(int cardNumber)
    {
        
        if (PlayerPrefs.GetInt("gold") >= 5000)
        {
            PlayerPrefs.SetInt("cardBuy" + cardNumber, 1);
            int money = PlayerPrefs.GetInt("gold");
            PlayerPrefs.SetInt("gold", money - 5000);
            menuManager.goldText.text = PlayerPrefs.GetInt("gold").ToString();

            EventSystem.current.currentSelectedGameObject.SetActive(false);

            onOpenCards?.Invoke();
        }
        else
        {
            StartCoroutine(menuManager.popUpCreat("YOU DON'T HAVE ENOUGH GOLD TO BUY"));
        }
    }

}
