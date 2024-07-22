using System;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class gameManager : MonoBehaviour
{

    [Header("----Enemy Stage Settings----")]
    [SerializeField] public GameObject[] enemyStage1;
    [SerializeField] public GameObject[] enemyStage2;
    [SerializeField] public GameObject[] enemyStage3;
    [Header("---Enemy List---")]
    public List<GameObject> enemyList;
    public GameObject[] enemyCreatPos;
    [Header("---Enemy Stage Count---")]
    public int enemyCreatCountStage1;
    public int enemyCreatCountStage2;
    public int enemyCreatCountStage3;
    [Header("---Enemy Settings---")]
    public int enemyCount;
    public TextMeshProUGUI enemyCountText;
    public int activeEnemy;
    public int towerCollisionEnemy;
    [Header("Enemy Stage Timer")]
    public float stage1Timer;
    public float stage2Timer;
    public float stage3Timer;

    [Header("---Elements---")]
    public Slider bossSlider;
    public Slider panelbossSlider;
    public TextMeshProUGUI totalKillLosePText;
    public TextMeshProUGUI totalKillWinPText;
    public GameObject winPanel;
    public GameObject _losePanel;
    public GameObject _popUpPrefabs;
    public TextMeshProUGUI _popUpText;
    public GameObject _newWordPanel;
    particleManager _particleManager;
    public GameObject coinPrefabs;
    ObjectFlow _objectFlowGold;
    ObjectFlow _objectFlowEnergy;
    ObjectFlow _objectFlowTrophy;
    public TextMeshProUGUI damageUpgradeText;
    public TextMeshProUGUI healthUpgradeText;
    public TextMeshProUGUI speedUpgradeText;
    public TextMeshProUGUI attackspeedUpgradeText;
    public TextMeshProUGUI rangeUpgradeText;
    public TextMeshProUGUI enemyGoldUpgradeText;
    public TextMeshProUGUI healthRecoveryUpgradeText;
    [SerializeField] private GameObject[] upgradePanels;
    dataManager _dataManager= new dataManager();
  
    public GameObject sceneLoadingPanel;
    public Slider loadingSlider;
    public AudioSource battleMusic;

    [Header("----UI DOTWEEN----")]
    [SerializeField] private RectTransform winBackground, loseBackground;
    [SerializeField] private RectTransform winLabelPanel, loseLabelPanel;
    [SerializeField] private RectTransform winRewards, loseRewards;
    [SerializeField] private RectTransform winButton1, winButton2, loseButton1, loseButton2;

    [Header("----Level Settings----")]
    private string arenaName;
    private int arenaNameINT;
    private string _currentComplatedArena;
    private int currentComplatedArenaINT;
    [SerializeField] private int levelWinGold;
    [SerializeField] private int levelLoseGold;
    [SerializeField] public int levelEnemyExp;
    [SerializeField] private TextMeshProUGUI levelWinGoldText;
    [SerializeField] private TextMeshProUGUI levelLoseGoldText;
    [SerializeField] private int levelWinTrophy;
    [SerializeField] private int levelLoseTrophy;
    [SerializeField] private TextMeshProUGUI levelWinTrophyText;
    [SerializeField] private TextMeshProUGUI levelLoseTrophyText;
    [SerializeField] private int levelWinEnergy;
    [SerializeField] private TextMeshProUGUI levelWinEnergyText;
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI energyText;
    public TextMeshProUGUI expText;
    public int exp;

    [Header("Action")]
    public static Action onCompetedArena;
    private void Awake()
    {
        Screen.SetResolution(450, 750, true);


        Time.timeScale = 1;

        PlayerPrefs.SetInt("tutorialscene", 1);

        enemyCount = enemyCreatCountStage1+enemyCreatCountStage2+enemyCreatCountStage3;


        bossSlider.maxValue = enemyCount;
        bossSlider.value = activeEnemy;
        activeEnemy = 0;
        towerCollisionEnemy = 0;
        _particleManager = GameObject.FindWithTag("particleManager").GetComponent<particleManager>();
        _objectFlowGold = GameObject.FindGameObjectWithTag("goldFlow").GetComponentInChildren<ObjectFlow>();
        _objectFlowEnergy = GameObject.FindGameObjectWithTag("energyFlow").GetComponentInChildren<ObjectFlow>();
        _objectFlowTrophy = GameObject.FindGameObjectWithTag("trophyFlow").GetComponentInChildren<ObjectFlow>();
    }
    private void Start()
    {
        battleMusic.Play();
     
        arenaName= SceneManager.GetActiveScene().name;


        exp = 50;
        expText.text = exp.ToString();

        goldText.text = PlayerPrefs.GetInt("gold").ToString();
        energyText.text = PlayerPrefs.GetInt("energy").ToString();
        
        enemyCountText.text = enemyCount.ToString();

        StartCoroutine(enemyCreat());
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
            winFinishPanel();
    }


    public IEnumerator popUpCreat(string massage)
    {

        _popUpPrefabs.SetActive(true);
        _popUpText.text = massage;

        yield return new WaitForSeconds(1f);
        _popUpPrefabs.SetActive(false);

    }


    public void winFinishPanel()
    {
        _dataManager.GoldCalculator(levelWinGold);
        _dataManager.TrophyCalculator(levelWinTrophy);
        _dataManager.EnergyCalculator(levelWinEnergy);
        levelWinGoldText.text = levelWinGold.ToString();
        levelWinTrophyText.text = levelWinTrophy.ToString();
        levelWinEnergyText.text= levelWinEnergy.ToString();

        PlayerPrefs.SetString("complatedArena", arenaName);

        totalKillWinPText.text = activeEnemy.ToString();

        int totalKill = PlayerPrefs.GetInt("totalKill", 1);
        PlayerPrefs.SetInt("totalKill", totalKill + activeEnemy);


        //_adManager.ShowInterstitialAd();

        int completedArenaQuest = PlayerPrefs.GetInt("compeletedArenaQuest", 1);
        PlayerPrefs.SetInt("compeletedArenaQuest", completedArenaQuest + 1);
        
        onCompetedArena?.Invoke();

        Time.timeScale = 0;
        winPanel.SetActive(true);

    }
    public void heroLosePanel()
    {

        _dataManager.GoldCalculator(levelLoseGold);
        _dataManager.TrophyCalculator(levelLoseTrophy);
        levelLoseGoldText.text = levelLoseGold.ToString();
        levelLoseTrophyText.text = levelLoseTrophy.ToString();

        panelbossSlider.maxValue = enemyCount;
        panelbossSlider.value = activeEnemy;



        totalKillLosePText.text = activeEnemy.ToString();

        _losePanel.SetActive(true);
        Time.timeScale = 0;
        
    }
   
    public void LOSERewarded2xButton()
    {
        _dataManager.GoldCalculator(levelLoseGold);
        EventSystem.current.currentSelectedGameObject.SetActive(false);

        //add
    }
    public void WINRewarded2xButton()
    {
        _dataManager.GoldCalculator(levelWinGold);
        _dataManager.EnergyCalculator(levelWinEnergy);
        EventSystem.current.currentSelectedGameObject.SetActive(false);


        //add
    }

    public void enemyDead()
    {
        activeEnemy++;

        exp+=levelEnemyExp;
        expText.text = exp.ToString();

        goldText.text = PlayerPrefs.GetInt("gold").ToString();

        int lastEnemy = enemyCount - activeEnemy;

        bossSlider.value = activeEnemy;
        enemyCountText.text = lastEnemy.ToString();

        if (activeEnemy+towerCollisionEnemy >= enemyCount)
        {
            winFinishPanel();
        }


    }
    public void upgradePanel(int menuNumber)
    {
        for (int i = 0; i < upgradePanels.Length; i++)
        {
            upgradePanels[i].SetActive(i == menuNumber);
        }

    }



    IEnumerator enemyCreat()
    {
        while (true)
        {
            yield return new WaitForSeconds(stage1Timer);

            if (enemyCreatCountStage1 != 0)
            {

                int enemyRandom = Random.Range(0, enemyStage1.Length);
                int creatRandom = Random.Range(0, enemyCreatPos.Length);

                GameObject objem = Instantiate(enemyStage1[enemyRandom], enemyCreatPos[creatRandom].transform.position, Quaternion.identity);

                enemyList.Add(objem);

                bossSlider.value = activeEnemy;
                enemyCreatCountStage1--;
            }

            if (enemyCreatCountStage1 == 0 && enemyCreatCountStage2!=0)
            {
                int enemyRandom = Random.Range(0, enemyStage2.Length);
                int creatRandom=Random.Range(0, enemyCreatPos.Length);  

                GameObject objem= Instantiate(enemyStage2[enemyRandom], enemyCreatPos[creatRandom].transform.position, Quaternion.identity);

                enemyList.Add(objem);

                bossSlider.value = activeEnemy;
                enemyCreatCountStage2--;
            }
            if (enemyCreatCountStage1 == 0 &&enemyCreatCountStage2==0&& enemyCreatCountStage3 != 0)
            {
                int enemyRandom = Random.Range(0, enemyStage3.Length);
                int creatRandom = Random.Range(0, enemyCreatPos.Length);

                GameObject objem = Instantiate(enemyStage3[enemyRandom], enemyCreatPos[creatRandom].transform.position, Quaternion.identity);

                enemyList.Add(objem);

                bossSlider.value = activeEnemy;
                enemyCreatCountStage3--;
            }



        }

    }

    public void onClickTween(RectTransform Rtransform)
    {
        Rtransform.DOScale(Rtransform.localScale * .015f, .015f).SetLoops(2, LoopType.Yoyo);
    }
    public void onClickTween2(RectTransform Rtransform)
    {
        Rtransform.DOScale(Rtransform.localScale * .015f, .015f).SetLoops(2, LoopType.Yoyo);
    }


    public void menuButton()
    {
        Time.timeScale = 1;
        StartCoroutine(menu());
    }

    IEnumerator menu()
    {
        _objectFlowGold.Flow();
        _objectFlowTrophy.Flow();
        _objectFlowEnergy.Flow();
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(loadingScene(0));

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

    public void goldpack()
    {
        if (PlayerPrefs.GetInt("gold") >= 500)
        {
            exp += 50;
            int money = PlayerPrefs.GetInt("gold");
            PlayerPrefs.SetInt("gold", money - 500);
            expText.text = exp.ToString();
        }
        else
        {
            StartCoroutine(popUpCreat("NOT ENOUGH GOLD"));
        }

    }

    public void rewardedButton()
    {
        exp += 100;
        expText.text=exp.ToString();
        _dataManager.EnergyCalculator(50);
        energyText.text = PlayerPrefs.GetInt("energy").ToString();
        EventSystem.current.currentSelectedGameObject.SetActive(false);


        //add
    }

    public void GameSpeed()
    {
        if(Time.timeScale==1)
            Time.timeScale = 3;
        else if(Time.timeScale==3)
            Time.timeScale = 1;

        Debug.Log("GameSpeed"+ Time.timeScale);
    }

    
}
