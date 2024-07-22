using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class newenemys
{
    public GameObject _enemy;
    public int _healh;
    public float _speed;
    public int _damage;

    public newenemys(GameObject enemy, int health, float speed, int damage)
    {
        this._enemy = enemy;
        this._healh = health;
        this._speed = speed;
        this._damage = damage;
    }
}

public class enemyController : MonoBehaviour
{
    [Header("Enemy Scriptable")]
    public towerEnemy _towerEnemy;
    private newenemys myEnemy;

    [Header("Enemy Stats")]

    GameObject inGameEnemy;
    int maxHealh;
    public int inGameHealh;
    float maxSpeed;
    public float inGameSpeed;
    int maxDamage;
    public int inGameDamage;

    [Header("Enemy Elements")]
    gameManager _gameManager;
    towerController _towerController;
    public Slider enemyHealthSlider;
    public GameObject target;

    [Header("Action")]
    public static Action onDead;

    private void Awake()
    {
        _gameManager = GameObject.FindGameObjectWithTag("gameManager").GetComponent<gameManager>();
        _towerController = GameObject.FindGameObjectWithTag("tower").GetComponent<towerController>();
        target = GameObject.FindGameObjectWithTag("tower");
        Config();
    }

    private void Update()
    {
        //if (target != null)
        //{
        //    Vector3 direction = target.transform.position - transform.position;
        //    transform.Translate(direction.normalized * inGameSpeed * Time.deltaTime);

        //Vector2 direction = target.transform.position - transform.position;
        //transform.Translate(direction.normalized * inGameSpeed * Time.deltaTime);

        //transform.position = Vector2.Lerp(transform.position, target.transform.position, inGameSpeed * Time.deltaTime);

    }
    private void FixedUpdate()
    {
        //transform.Translate(Vector2.down * inGameSpeed * Time.deltaTime);
        Vector2 direction = target.transform.position - transform.position;
        transform.Translate(direction.normalized * inGameSpeed * Time.deltaTime);
    }


    private void Start()
    {
        transform.rotation= new Quaternion(0,0,0,0);
        maxHealh = inGameHealh;
        maxSpeed = inGameSpeed;
        maxDamage = inGameDamage;
        enemyHealthSlider.maxValue = maxHealh;
        enemyHealthSlider.value = inGameHealh;
    }

    void Config()
    {
        myEnemy = new newenemys(_towerEnemy._enemy, _towerEnemy.health,_towerEnemy.speed,_towerEnemy.damage);
        inGameEnemy = myEnemy._enemy;
        inGameHealh = myEnemy._healh;
        inGameSpeed = myEnemy._speed;
        inGameDamage = myEnemy._damage;
    }




    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            inGameHealh--;
        }
        

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("tower"))
        {
            Debug.Log("towera vurdu");
            _towerController.towerHealtDamage(inGameDamage);
            _gameManager.towerCollisionEnemy++;
            if(_gameManager.towerCollisionEnemy+_gameManager.activeEnemy >= _gameManager.enemyCount)
            {
                _gameManager.winFinishPanel();
            }
            gameObject.SetActive(false);
        }
    }

    public void enemyHealtDamage(int heroDamage)
    {
        inGameHealh -= heroDamage;

        enemyHealthSlider.value = inGameHealh;


        if (inGameHealh <= 0)
        {
            _gameManager.enemyDead();
            gameObject.SetActive(false);

            onDead?.Invoke();

        }
    }

    //IEnumerator coinAnim()
    //{
    //    GameObject coin = Instantiate(_gameManager.coinPrefabs, transform.position, Quaternion.identity);
    //    yield return new WaitForSeconds(2f);
    //    Destroy(coin);
    //}
}