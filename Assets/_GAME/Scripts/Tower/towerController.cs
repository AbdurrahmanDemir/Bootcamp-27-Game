using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class _newTower
{
    public GameObject tower;
    public int health;
    public int damage;
    public float arrowSpeed;
    public float attackSpeed;
    public float range;
    public float healthRecovery;

    public _newTower(GameObject tower,int health, int damage, float arrowSpeed,float attackSpeed,float range,float healthRecovery)
    {
        this.tower = tower; 
        this.health = health;
        this.damage = damage;
        this.arrowSpeed = arrowSpeed;
        this.attackSpeed = attackSpeed;
        this.range = range;
        this.healthRecovery = healthRecovery;
    }
}
public class towerController : MonoBehaviour
{
    [Header("Tower Scriptible")]
    public tower _tower;
    public _newTower _newTower;

    [Header("Elements")]
     gameManager _gameManager;
    private int towerMaxHealth;
    public int towerGameInHealth;
    private int towerMaxDamage;
    public int towerGameInDamage;
    private float towerMaxSpeed;
    public float towerGameInSpeed;
    private float towerMaxAttackSpeed;
    public float towerGameInAttackSpeed;
    private float towerMaxRange;
    public float towerGameInRange;
    private float towerMaxHealthRecovery;
    public float towerGameInRecovery;
    [Header("Settings")]
    public TextMeshProUGUI healthText;
    public Slider towerHealthSlider;
    private int upgradeHealthExp = 20;
    private int upgradeHealthRecoveryExp = 20;
    private int upgradeDamageExp = 20;
    private int upgradeSpeedExp = 20;
    private int upgradeAttackSpeedExp = 20;
    private int upgradeRangeExp = 5;
    private int upgradeEnemyExp = 20;
    [Header("Game Settings")]
    [SerializeField] private GameObject arrowPrefabs;
    [SerializeField] private GameObject attackPos1;
    public bool isAttack;

    private void Awake()
    {

        Config();

        towerGameInHealth = towerMaxHealth;
        towerGameInDamage = towerMaxDamage;
        towerGameInSpeed = towerMaxSpeed;
        towerGameInAttackSpeed = towerMaxAttackSpeed;
        towerGameInRange = towerMaxRange;
        towerGameInRecovery = towerMaxHealthRecovery;
    }
    private void Start()
    {
        _gameManager = GameObject.FindGameObjectWithTag("gameManager").GetComponent<gameManager>();


        healthText.text = towerGameInHealth.ToString();

        towerHealthSlider.maxValue = towerMaxHealth;
        towerHealthSlider.value = towerGameInHealth;

        _gameManager.damageUpgradeText.text = upgradeDamageExp.ToString();
        _gameManager.healthUpgradeText.text = upgradeHealthExp.ToString();
        _gameManager.attackspeedUpgradeText.text=upgradeAttackSpeedExp.ToString();
        _gameManager.speedUpgradeText.text = upgradeSpeedExp.ToString();
        _gameManager.rangeUpgradeText.text = upgradeRangeExp.ToString();
        _gameManager.enemyGoldUpgradeText.text=upgradeEnemyExp.ToString();
        _gameManager.healthRecoveryUpgradeText.text = upgradeHealthRecoveryExp.ToString();

        StartCoroutine(attack());
        StartCoroutine(towerHealth());
    }
    void Config()
    {
        _newTower = new _newTower(_tower._tower, 
            PlayerPrefs.GetInt("towerHealth"), 
            PlayerPrefs.GetInt("towerDamage"), 
            PlayerPrefs.GetFloat("towerArrowSpeed"),
            PlayerPrefs.GetFloat("towerAttackSpeed"),
            PlayerPrefs.GetFloat("towerRange"),
            PlayerPrefs.GetFloat("towerRecovery"));
        towerMaxHealth=_newTower.health;
        towerMaxDamage=_newTower.damage;
        towerMaxSpeed = _newTower.arrowSpeed;
        towerMaxAttackSpeed= _newTower.attackSpeed;
        towerMaxRange= _newTower.range;
        towerMaxHealthRecovery= _newTower.healthRecovery;


    }

    IEnumerator attack()
    {
        

        while (true)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy1");
          
            yield return new WaitForSeconds(towerGameInAttackSpeed);

            if (isAttack&&enemies.Length>0)
            {
                Instantiate(arrowPrefabs, attackPos1.transform.position, Quaternion.identity);
            }



        }
    }
    public void towerHealtDamage(int heroDamage)
    {
        towerGameInHealth -= heroDamage;

        towerHealthSlider.value = towerGameInHealth;
        healthText.text = towerGameInHealth.ToString();


        if (towerGameInHealth <= 0)
        {

            _gameManager.heroLosePanel();

            gameObject.SetActive(false);

            

        }
    }
    IEnumerator towerHealth()
    {
        while (true)
        {

            yield return new WaitForSeconds(towerGameInRecovery);

            if (towerGameInHealth < towerMaxHealth)
            {
                towerGameInHealth += 5;
                healthText.text = towerGameInHealth.ToString();

                towerHealthSlider.maxValue = towerMaxHealth;
                towerHealthSlider.value = towerGameInHealth;

            }
        }
    }

    public void towerHealthUpgrade()
    {
        if (_gameManager.exp >= upgradeHealthExp)
        {
            towerGameInHealth += 10;
            towerMaxHealth += 10;
            healthText.text = towerGameInHealth.ToString();

            towerHealthSlider.maxValue = towerMaxHealth;
            towerHealthSlider.value = towerGameInHealth;

            //int money = PlayerPrefs.GetInt("gold");
            //PlayerPrefs.SetInt("gold", money - upgradeHealthExp);

            _gameManager.exp-=upgradeHealthExp;

            upgradeHealthExp += 50;

            _gameManager.goldText.text = PlayerPrefs.GetInt("gold").ToString();
            _gameManager.expText.text=_gameManager.exp.ToString();

            _gameManager.healthUpgradeText.text = upgradeHealthExp.ToString();

            
        }
    }
    public void towerDamageUpgrade()
    {
        if (_gameManager.exp >= upgradeDamageExp)
        {

            towerGameInDamage += 10;

            //int money = PlayerPrefs.GetInt("gold");
            //PlayerPrefs.SetInt("gold", money - upgradeDamageExp);

            _gameManager.exp-=upgradeDamageExp;

            upgradeDamageExp += 20;


            _gameManager.damageUpgradeText.text = upgradeDamageExp.ToString();
            _gameManager.goldText.text = PlayerPrefs.GetInt("gold").ToString();
            _gameManager.expText.text = _gameManager.exp.ToString();


        }
    }
    public void towerSpeedUpgrade()
    {
        if (_gameManager.exp >= upgradeSpeedExp)
        {

            towerGameInSpeed += 0.2f;

            //int money = PlayerPrefs.GetInt("gold");
            //PlayerPrefs.SetInt("gold", money - upgradeSpeedExp);

            _gameManager.exp-=upgradeSpeedExp;


            upgradeSpeedExp += 10;


            _gameManager.speedUpgradeText.text = upgradeSpeedExp.ToString();
            _gameManager.goldText.text = PlayerPrefs.GetInt("gold").ToString();
            _gameManager.expText.text = _gameManager.exp.ToString();


        }
    }
    public void towerAttackSpeedUpgrade()
    {
        if (_gameManager.exp >= upgradeAttackSpeedExp)
        {

            towerGameInAttackSpeed -= 0.09f;

            if (towerGameInAttackSpeed <= 0.5f)
            {
                towerGameInAttackSpeed = 0.5f;
                _gameManager.attackspeedUpgradeText.text = "MAX SPEED";
                return;
            }

            //int money = PlayerPrefs.GetInt("gold");
            //PlayerPrefs.SetInt("gold", money - upgradeAttackSpeedExp);

            _gameManager.exp-= upgradeAttackSpeedExp;


            upgradeAttackSpeedExp += 10;


            _gameManager.attackspeedUpgradeText.text = upgradeAttackSpeedExp.ToString();
            _gameManager.goldText.text = PlayerPrefs.GetInt("gold").ToString();
            _gameManager.expText.text = _gameManager.exp.ToString();


        }
    }
    public void towerRangeUpgrade()
    {
        if (_gameManager.exp >= upgradeRangeExp)
        {

            towerGameInRange += 0.2f;



            //int money = PlayerPrefs.GetInt("gold");
            //PlayerPrefs.SetInt("gold", money - upgradeRangeExp);

            _gameManager.exp-=upgradeRangeExp;

            if (towerGameInRange >= 4f)
            {
                towerGameInRange = 4f;
                _gameManager.rangeUpgradeText.text = "MAX LIFETIME";
                return;
            }


            upgradeRangeExp += 2;


            _gameManager.rangeUpgradeText.text = upgradeRangeExp.ToString();
            _gameManager.goldText.text = PlayerPrefs.GetInt("gold").ToString();
            _gameManager.expText.text = _gameManager.exp.ToString();


        }
    }

    public void towerHealthRecoveryUpgrade()
    {
        if (_gameManager.exp >= upgradeHealthRecoveryExp)
        {

            towerGameInRecovery -= 0.2f;

            if (towerGameInRecovery <= 0.5f)
            {
                towerGameInRecovery = 0.5f;
                _gameManager.healthRecoveryUpgradeText.text = "MAX RECOVERY";
                return;
            }

            //int money = PlayerPrefs.GetInt("gold");
            //PlayerPrefs.SetInt("gold", money - upgradeHealthRecoveryExp);

            _gameManager.exp-= upgradeHealthRecoveryExp;

            upgradeHealthRecoveryExp += 50;


            _gameManager.healthRecoveryUpgradeText.text = upgradeHealthRecoveryExp.ToString();
            _gameManager.goldText.text = PlayerPrefs.GetInt("gold").ToString();
            _gameManager.expText.text = _gameManager.exp.ToString();


        }
    }

    public void enemyGoldUpgrade()
    {
        if (_gameManager.exp >= upgradeEnemyExp)
        {

            _gameManager.levelEnemyExp += 5;



            //int money = PlayerPrefs.GetInt("gold");
            //PlayerPrefs.SetInt("gold", money - upgradeEnemyExp);

            _gameManager.exp-=upgradeEnemyExp;


            upgradeEnemyExp += 50;


            _gameManager.enemyGoldUpgradeText.text = upgradeEnemyExp.ToString();
            _gameManager.goldText.text = PlayerPrefs.GetInt("gold").ToString();
            _gameManager.expText.text = _gameManager.exp.ToString();


        }
    }

}
