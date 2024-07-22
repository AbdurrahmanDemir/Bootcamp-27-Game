using DG.Tweening;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class menuManager : MonoBehaviour
{
    [Header("elements")]
    [SerializeField] public TextMeshProUGUI goldText;
    [SerializeField] private TextMeshProUGUI trophyText;
    [SerializeField] public TextMeshProUGUI energyText;
    [SerializeField] private GameObject sceneLoadingPanel;    
    [SerializeField] private  Slider loadingSlider;
    [SerializeField] private GameObject[] menuPanels;
    [SerializeField] private GameObject noAdsButton;
    [SerializeField] private GameObject startedpack1Button;
    [SerializeField] private GameObject startedpack2Button;
    public GameObject inboxPanel;
    public GameObject questPanel;
    public GameObject leaderboardPanel;

    private bool isInboxPanel = false;
    private bool isQuestPanel = false;
    private bool isLeaderboardPanel = false;
    int adsCounter=0;
    public GameObject _popUpPrefabs;
    public TextMeshProUGUI _popUpText;
    public AudioSource menuMusic;
    public AudioSource goldMusic;

    [Header("Action")]
    public static Action onAdsWatch;

    [Header("arenaKey")]
    [SerializeField] private GameObject[] arenaKeys;
    [SerializeField] private GameObject[] complatedObjects;



    dataManager _dataManager = new dataManager();
   
    private void Awake()
    {
        Screen.SetResolution(450, 750, true);

        menuMusic.Play();

        if (!PlayerPrefs.HasKey("starPower1") && !PlayerPrefs.HasKey("starPower2")) //oyuna ilk kez girildiğinde ilk 2 skill veriliyor
        {
            PlayerPrefs.SetInt("starPower1", 1);
            PlayerPrefs.SetInt("starPower2", 0);
        }

        if (!PlayerPrefs.HasKey("firstLogin"))
        {
            _dataManager.GoldCalculator(1500);
            _dataManager.TrophyCalculator(0);
            _dataManager.EnergyCalculator(150);
            PlayerPrefs.SetInt("firstLogin", 1);

            SceneManager.LoadScene("tutorial");
        }        

        if (!PlayerPrefs.HasKey("complatedArena"))
        {

            PlayerPrefs.SetString("complatedArena", "0");
        }

        if (!PlayerPrefs.HasKey("noads"))
            PlayerPrefs.SetInt("noads", 0);
        if (!PlayerPrefs.HasKey("startedpack1"))
            PlayerPrefs.SetInt("startedpack1", 0);
    }

    private void Start()
    {
        ArenaKey();
        

        goldText.text = PlayerPrefs.GetInt("gold").ToString();
        trophyText.text = PlayerPrefs.GetInt("trophy").ToString();
        energyText.text = PlayerPrefs.GetInt("energy").ToString();

        if (PlayerPrefs.GetInt("noads") == 1)
        {
            NoAdsRemove();
        }
        if (PlayerPrefs.GetInt("startedpack1") == 1)
        {
            startedPack1Remove();
        }

    }
    public void NoAdsRemove()
    {
        noAdsButton.SetActive(false);
    }
    public void startedPack1Remove()
    {
        startedpack1Button.SetActive(false);
        startedpack2Button.SetActive(true);
    }


    public void ManagerStage(int arenaNumber)
    {
        int arenaEnergy = arenaNumber * 5;
        if (PlayerPrefs.GetInt("energy") >=arenaEnergy)
        {
            StartCoroutine(loadingScene(arenaNumber));
            int energy = PlayerPrefs.GetInt("energy");
            PlayerPrefs.SetInt("energy", energy - arenaEnergy);

        }
        else
        {
            StartCoroutine(popUpCreat("THERE IS NOT ENOUGH ENERGY. YOU CAN WATCH THE VIDEO FROM THE MARKET"));

        }
        
    }
    IEnumerator loadingScene(int arenaNumber)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(arenaNumber);

        sceneLoadingPanel.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 10f);
            loadingSlider.value = progress;
            yield return null;

        }

    }

    public void menuPanel(int menuNumber)
    {
        adsCounter++;
        for (int i = 0; i < menuPanels.Length; i++)
        {
            
            menuPanels[i].SetActive(i==menuNumber);
            
            if (adsCounter >= 10)
            {
                adsCounter = 0;

                //add
                
            }

        }

    }

    public IEnumerator popUpCreat(string massage)
    {

        _popUpPrefabs.SetActive(true);
        _popUpText.text = massage;

        yield return new WaitForSeconds(2.8f);
        _popUpPrefabs.SetActive(false);

    }

    private void ArenaKey()
    {
        int completedArena = int.Parse(PlayerPrefs.GetString("complatedArena"));

        for (int i = 1; i <= 8; i++)
        {
            arenaKeys[i - 1].SetActive(i > completedArena);
            complatedObjects[i - 1].SetActive(i <= completedArena);
        }

    }

    public void rewardedEnergy25()
    {
        _dataManager.EnergyCalculator(25);
        energyText.text = PlayerPrefs.GetInt("energy").ToString();
        
        goldMusic.Play();
        StartCoroutine(popUpCreat("25 ENERJI KAZANDIN"));

        EventSystem.current.currentSelectedGameObject.SetActive(false);


        //add

    }

    public void rewardedGold50()
    {
        _dataManager.GoldCalculator(50);
        goldText.text = PlayerPrefs.GetInt("gold").ToString();
        StartCoroutine(popUpCreat("50 ALTIN KAZANDIN"));
        goldMusic.Play();

        EventSystem.current.currentSelectedGameObject.SetActive(false);


        //add



        onAdsWatch?.Invoke();

    }
    public void energy150Buy()
    {
        if (PlayerPrefs.GetInt("gold") >= 200)
        {
            _dataManager.EnergyCalculator(150);
            int energy = PlayerPrefs.GetInt("gold");
            PlayerPrefs.SetInt("gold", energy - 200);
            energyText.text = PlayerPrefs.GetInt("energy").ToString();
            goldText.text = PlayerPrefs.GetInt("gold").ToString();
        }
        else
        {
            StartCoroutine(popUpCreat("not enough gold"));
        }

    }

    public void shopOpenButton()
    {
        for (int i = 0; i < menuPanels.Length; i++)
        {

            menuPanels[i].SetActive(i == 0);
        }

    }
    
    public void inboxGift()
    {
        if (!PlayerPrefs.HasKey("inboxGiftt"))
        {
            _dataManager.GoldCalculator(500);
            _dataManager.EnergyCalculator(100);
            goldText.text = PlayerPrefs.GetInt("gold").ToString();
            goldMusic.Play();
            PlayerPrefs.SetInt("inboxGiftt", 1);


            EventSystem.current.currentSelectedGameObject.SetActive(false);

        }
        else
        {
            StartCoroutine(popUpCreat("You already received the gift"));
        }
            

    }
    public void onClickTween(RectTransform Rtransform)
    {
        Rtransform.DOScale(Rtransform.localScale * .02f, .01f).SetLoops(2, LoopType.Yoyo);
    }
    public void InboxPanel()
    {
        isInboxPanel = !isInboxPanel;

        inboxPanel.SetActive(isInboxPanel);
    }

    public void QuestPanel()
    {
        isQuestPanel = !isQuestPanel;

        questPanel.SetActive(isQuestPanel);
        goldText.text = PlayerPrefs.GetInt("gold").ToString();
    }

    public void LeaderboardPanel()
    {
        isLeaderboardPanel = !isLeaderboardPanel;

        leaderboardPanel.SetActive(isLeaderboardPanel);
    }
    public void squadLink()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.level23.SquadArena");
    }
    public void fisherLink()
    {
        Application.OpenURL("https://bit.ly/3ZwnQXB");
    }
    public void runnerLink()
    {
        Application.OpenURL("https://bit.ly/456exyw");
    }
}
