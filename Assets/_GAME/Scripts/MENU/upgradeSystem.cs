using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class upgradeSystem : MonoBehaviour
{
     public tower _tower;

    public TMP_Text playerUpgradeLevelName;
    public TMP_Text playerUpgradeMoney;
    public GameObject moneyBox;

    [SerializeField] private TextMeshProUGUI towerDamageText;
    [SerializeField] private TextMeshProUGUI towerHealtText;
    [SerializeField] private TextMeshProUGUI towerArrowSpeedText;
    [SerializeField] private TextMeshProUGUI towerAttackSpeedText;
    [SerializeField] private TextMeshProUGUI towerRangeText;
    [SerializeField] private TextMeshProUGUI towerRecoveryText;

    [SerializeField] private TextMeshProUGUI towerPanelGoldText;

    [Header("Action")]
    public static Action onTowerUpgrade;

    public int[] PlayerUpgradeMoneyArray = { 100, 300, 500, 1000, 10000 };

    public Tweener ShakeTweener;
    private string _upgradeLevelName = "Upgrade Lvl. ";
    private int _playerUpgradeLevel = 1;


    private void Awake()
    {
        if (!PlayerPrefs.HasKey("towerDamage"))
            PlayerPrefs.SetInt("towerDamage", _tower.damage = 100);
        else
            _tower.damage = PlayerPrefs.GetInt("towerDamage");


        if (!PlayerPrefs.HasKey("towerHealth"))
            PlayerPrefs.SetInt("towerHealth", _tower.health=150);
        else
            _tower.health = PlayerPrefs.GetInt("towerHealth");


        if (!PlayerPrefs.HasKey("towerArrowSpeed"))
            PlayerPrefs.SetFloat("towerArrowSpeed", _tower.arrowSpeed= 5f);
        else
            _tower.arrowSpeed = PlayerPrefs.GetFloat("towerArrowSpeed");


        if (!PlayerPrefs.HasKey("towerAttackSpeed"))
            PlayerPrefs.SetFloat("towerAttackSpeed", _tower.attackSpeed= 3f);
        else
            _tower.attackSpeed = PlayerPrefs.GetFloat("towerAttackSpeed");


        if (!PlayerPrefs.HasKey("towerRange"))
            PlayerPrefs.SetFloat("towerRange", _tower.range= 2f);
        else
            _tower.range = PlayerPrefs.GetFloat("towerRange");


        if (!PlayerPrefs.HasKey("towerRecovery"))
            PlayerPrefs.SetFloat("towerRecovery", _tower.healthRecovery= 4f);
        else
            _tower.healthRecovery = PlayerPrefs.GetFloat("towerRecovery");

    }

    private void Start()
    {
        towerPanelGoldText.text = PlayerPrefs.GetInt("gold").ToString();

        towerDamageText.text = PlayerPrefs.GetInt("towerDamage").ToString();
        towerHealtText.text = PlayerPrefs.GetInt("towerHealth").ToString();
        towerArrowSpeedText.text = PlayerPrefs.GetFloat("towerArrowSpeed").ToString("F2");
        towerAttackSpeedText.text = PlayerPrefs.GetFloat("towerAttackSpeed").ToString("F2");
        towerRangeText.text = PlayerPrefs.GetFloat("towerRange").ToString("F2");
        towerRecoveryText.text = PlayerPrefs.GetFloat("towerRecovery").ToString("F2");

        if (!PlayerPrefs.HasKey("towerUpgradeLevel"))
        {
            _playerUpgradeLevel = 1;
        }
        else
        {
            _playerUpgradeLevel = PlayerPrefs.GetInt("towerUpgradeLevel");
        }

        playerUpgradeLevelName.text = _upgradeLevelName + _playerUpgradeLevel;
        playerUpgradeMoney.text = PlayerUpgradeMoneyArray[_playerUpgradeLevel - 1].ToString();
    }
    public int UpgradeButton(int[] UpgradeMoneyArray, int upgradeLevel, TMP_Text upgradeLevelName,
        TMP_Text upgradeMoney)
    {
        int money = UpgradeMoneyArray[upgradeLevel - 1];
        if (PlayerPrefs.GetInt("gold") >= money) // paramiz varsa upgrade islemlerini yapicaz
        {
            if (upgradeLevel >= UpgradeMoneyArray.Length)
            {
                int gold = PlayerPrefs.GetInt("gold");
                PlayerPrefs.SetInt("gold", gold - money);
                upgradeMoney.GetComponentInParent(typeof(Button)).gameObject
                    .SetActive(false); //eger tum upgradeler yapilirsa butonu gorunmez yapiyoruz
                upgradeLevelName.text = "Upgrade Lvl. Max";
            }
            else
            {
                int gold = PlayerPrefs.GetInt("gold");
                PlayerPrefs.SetInt("gold", gold - money); //odeme yaptik
                upgradeLevelName.text = "Upgrade Lvl. " + (upgradeLevel + 1); //level ismimizi guncelledik
                upgradeMoney.text = UpgradeMoneyArray[upgradeLevel].ToString(); //paramizi guncelledik

                _tower.damage += 25;
                _tower.health += 20;
                _tower.arrowSpeed += 0.2f;
                if(_tower.attackSpeed>0.8f)
                    _tower.attackSpeed -= 0.2f;
                else
                    towerAttackSpeedText.text = "MAX";

                _tower.range += 0.2f;
                if (_tower.attackSpeed > 0.6f)
                    _tower.healthRecovery -= 0.3f;
                else
                    towerRecoveryText.text = "MAX";

                PlayerPrefs.SetInt("towerDamage",_tower.damage);
                PlayerPrefs.SetInt("towerHealth", _tower.health);
                PlayerPrefs.SetFloat("towerArrowSpeed", _tower.arrowSpeed);
                PlayerPrefs.SetFloat("towerAttackSpeed", _tower.attackSpeed);
                PlayerPrefs.SetFloat("towerRecovery", _tower.healthRecovery);
                PlayerPrefs.GetFloat("towerRange", _tower.range);

                towerDamageText.text = _tower.damage.ToString();
                towerHealtText.text = _tower.health.ToString();
                towerArrowSpeedText.text = _tower.arrowSpeed.ToString("F2");
                towerAttackSpeedText.text = _tower.attackSpeed.ToString("F2");
                towerRangeText.text = _tower.range.ToString("F2");
                towerRecoveryText.text = _tower.healthRecovery.ToString("F2");


                towerPanelGoldText.text = PlayerPrefs.GetInt("gold").ToString();

                Debug.Log(upgradeLevel);
                upgradeLevel += 1; //bu kismmi kaydetmem lazim***.
                PlayerPrefs.SetInt("towerUpgradeLevel",upgradeLevel);

                onTowerUpgrade?.Invoke();
            }
        }
        else
        {
            MoneyShake(); //paramiz yoksa para kutucugunu titrestiriyoruz
        }

        return upgradeLevel;
    }

    public void MoneyShake()
    {
        if (ShakeTweener != null)
        {
            if (ShakeTweener.IsActive())
            {
                return;
            }
        }
        ShakeTweener = moneyBox.transform.DOShakeScale(0.5f, 0.2f, 10, 90f, false);
    }

    public void PlayerUpgrade()
    {
        _playerUpgradeLevel = UpgradeButton(PlayerUpgradeMoneyArray, _playerUpgradeLevel, playerUpgradeLevelName,
            playerUpgradeMoney);
    }
}