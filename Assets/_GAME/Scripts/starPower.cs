using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.UI;

public class starPower : MonoBehaviour
{
    private towerController _towerController;
    private enemyController _enemyController;
    private gameManager _gameManager;

    private bool isAttackSpeed;
    private bool isArrowSpeed;
    private bool isRecoverySpeed;
    private bool isExtraArrow;
    private bool isEnemyFreeze;
    private bool isWallsPower;

    [Header("SKILL SPEED")]
    private float oldAttackSpeed;
    private float oldArrowSpeed;
    private float oldRecoverySpeed;
    private float oldEnemySpeed;

    [Header("---SKILL TIMER---")]
    [SerializeField] private float attackSpeedTimer;
    [SerializeField] private float arrowSpeedTimer;
    [SerializeField] private float recoverySpeedTimer;
    [SerializeField] private float extraArrowTimer;
    [SerializeField] private float enemyFreezeTimer;
    [SerializeField] private float wallsTimer;

    [Header("---SKILL SLIDER---")]
    [SerializeField] private Slider attackSpeedSlider;
    [SerializeField] private Slider arrowSpeedSlider;
    [SerializeField] private Slider recoverySpeedSlider;
    [SerializeField] private Slider extraArrowSlider;
    [SerializeField] private Slider enemyFreezeSlider;
    [SerializeField] private Slider wallsSlider;

    [Header("---ELEMENTS----")]
    [SerializeField] private GameObject superArrowPrefabs;
    [SerializeField] private GameObject wallsPrefabs;
    [SerializeField] private GameObject attackPos1;
    [SerializeField] private GameObject[] enemy;

    private void Start()
    {
        _towerController = GameObject.FindWithTag("tower").GetComponent<towerController>();
        _gameManager = GameObject.FindGameObjectWithTag("gameManager").GetComponent<gameManager>();
      

        oldAttackSpeed = _towerController.towerGameInAttackSpeed;
        oldArrowSpeed = _towerController.towerGameInSpeed;
        oldRecoverySpeed = _towerController.towerGameInRecovery;
    }
    private void Update()
    {
        GameObject[] enemyList = GameObject.FindGameObjectsWithTag("enemy1");
        enemy = new GameObject[enemyList.Length];

        for (int i = 0; i < enemyList.Length; i++)
        {
            enemy[i] = enemyList[i];
            _enemyController = enemy[i].GetComponent<enemyController>();

            oldEnemySpeed = _enemyController.inGameSpeed;
        }

        if (isAttackSpeed)
        {
            attackSpeedTimer-=Time.deltaTime;
            attackSpeedSlider.value = attackSpeedTimer;
            if (attackSpeedTimer <= 0)
            {
                attackSpeedTimer = 0;
                
                _towerController.towerGameInAttackSpeed = oldAttackSpeed;
                isAttackSpeed = false;
            }
        }

        if (isArrowSpeed)
        {
            arrowSpeedTimer-=Time.deltaTime;
            arrowSpeedSlider.value = arrowSpeedTimer;
            if (arrowSpeedTimer <= 0)
            {
                arrowSpeedTimer = 0;
                _towerController.towerGameInSpeed = oldArrowSpeed;
                isArrowSpeed = false;
            }
        }
        if (isRecoverySpeed)
        {
            recoverySpeedTimer-=Time.deltaTime;
            recoverySpeedSlider.value = recoverySpeedTimer;
            if(recoverySpeedTimer <= 0)
            {
                recoverySpeedTimer = 0;
                _towerController.towerGameInRecovery = oldRecoverySpeed;
                isRecoverySpeed = false;
            }

        }
        if (isExtraArrow)
        {
            extraArrowTimer-=Time.deltaTime;
            extraArrowSlider.value = extraArrowTimer;
            if(extraArrowTimer <= 0)
            {
                extraArrowTimer = 0;
                isExtraArrow = false;

                CancelInvoke("extraArrowInstantiante");
            }
        }
        if (isEnemyFreeze)
        {
            enemyFreezeTimer-=Time.deltaTime;
            enemyFreezeSlider.value = enemyFreezeTimer;
            if(enemyFreezeTimer <= 0)
            {
                enemyFreezeTimer = 0;

                //for (int i = 0; i < enemy.Length; i++)
                //{
                //    _enemyController.inGameSpeed = oldEnemySpeed;
                //}

                _enemyController.inGameSpeed = oldEnemySpeed;

                isEnemyFreeze = false;
            }
        }

        if (isWallsPower)
        {
            wallsTimer -= Time.deltaTime;
            wallsSlider.value = wallsTimer;
            if (wallsTimer <= 0)
            {
                wallsTimer = 0;
                GameObject walls = GameObject.FindGameObjectWithTag("walls");
                walls.SetActive(false);
                isWallsPower = false;

            }
        }

    }


    public void attackSpeedPower()
    {
        if (!isAttackSpeed && PlayerPrefs.GetInt("energy") >= 20)
        {
            int money = PlayerPrefs.GetInt("energy");
            PlayerPrefs.SetInt("energy", money - 20);

            _gameManager.energyText.text = PlayerPrefs.GetInt("energy").ToString();


            isAttackSpeed = true;
            attackSpeedTimer = 5f;
            float newSpeed = 1f;
            _towerController.towerGameInAttackSpeed = newSpeed;
            attackSpeedSlider.maxValue = 5f;

        }
        else
        {
            StartCoroutine(_gameManager.popUpCreat("YETERLI ENERJI YOK"));
        }
    }
    public void arrowSpeedPower()
    {
        if (!isArrowSpeed && PlayerPrefs.GetInt("energy") >= 20)
        {
            int money = PlayerPrefs.GetInt("energy");
            PlayerPrefs.SetInt("energy", money - 20);
            _gameManager.energyText.text = PlayerPrefs.GetInt("energy").ToString();


            isArrowSpeed = true;
            arrowSpeedTimer = 5f;
            float newSpeed = 10f;
            _towerController.towerGameInSpeed = newSpeed;
            arrowSpeedSlider.maxValue = 5f;
        }
        else
        {
            StartCoroutine(_gameManager.popUpCreat("YETERLI ENERJI YOK"));
        }
    }
    public void recoverySpeedPower()
    {
        if (!isRecoverySpeed && PlayerPrefs.GetInt("energy") >= 20)
        {
            int money = PlayerPrefs.GetInt("energy");
            PlayerPrefs.SetInt("energy", money - 20);
            _gameManager.energyText.text = PlayerPrefs.GetInt("energy").ToString();


            isRecoverySpeed = true;
            recoverySpeedTimer = 5f;
            float newSpeed = 1f;
            _towerController.towerGameInRecovery=newSpeed;
            recoverySpeedSlider.maxValue = 5f;
        }
        else
        {
            StartCoroutine(_gameManager.popUpCreat("YETERLI ENERJI YOK"));
        }
    }

    public void extraArrowPower()
    {
        if (!isExtraArrow && PlayerPrefs.GetInt("energy") >= 70)
        {
            int money = PlayerPrefs.GetInt("energy");
            PlayerPrefs.SetInt("energy", money - 70);
            _gameManager.energyText.text = PlayerPrefs.GetInt("energy").ToString();


            isExtraArrow = true;
            InvokeRepeating("extraArrowInstantiante", 0.5f, 0.5f);
            extraArrowTimer = 5f;
            extraArrowSlider.maxValue = 5f;
            
        }
        else
        {
            StartCoroutine(_gameManager.popUpCreat("YETERLI ENERJI YOK"));
        }

    }

    private void extraArrowInstantiante()
    {
        Instantiate(superArrowPrefabs, attackPos1.transform.position, Quaternion.identity);

    }

    public void enemyFreezePower()
    {
        if (!isEnemyFreeze && PlayerPrefs.GetInt("energy") >= 20)
        {
            int money = PlayerPrefs.GetInt("energy");
            PlayerPrefs.SetInt("energy", money - 20);
            _gameManager.energyText.text = PlayerPrefs.GetInt("energy").ToString();


            isEnemyFreeze = true;
            enemyFreezeTimer = 2f;
            float newSpeed = 0f;

            for (int i = 0; i < enemy.Length; i++)
            {
                _enemyController.inGameSpeed = newSpeed;
            }

            enemyFreezeSlider.maxValue = 2f;
        }
        else
        {
            StartCoroutine(_gameManager.popUpCreat("YETERLI ENERJI YOK"));
        }
    }

    public void wallsPower()
    {
        if (!isWallsPower && PlayerPrefs.GetInt("energy") >= 50)
        {
            int money = PlayerPrefs.GetInt("energy");
            PlayerPrefs.SetInt("energy", money - 50);
            _gameManager.energyText.text = PlayerPrefs.GetInt("energy").ToString();

            isWallsPower = true;
            wallsTimer = 5f;
            Instantiate(wallsPrefabs, wallsPrefabs.transform.position, Quaternion.identity);
            wallsSlider.maxValue = 5f;

        }
        else
        {
            StartCoroutine(_gameManager.popUpCreat("YETERLI ENERJI YOK"));
        }
    }



}
