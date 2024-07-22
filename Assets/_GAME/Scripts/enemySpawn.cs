using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class enemySpawn : MonoBehaviour
{

    [Header("Enemy & Hero")]
    [SerializeField] public GameObject[] enemy;
    public List<GameObject> enemyList;
    public GameObject[] enemyCreatPos;
    public int enemyCreatCount;
    public int _enemyCreat;
    public int enemyCount;
    public TextMeshProUGUI enemyCountText;
    public int activeEnemy;
    public int towerCollisionEnemy;

    private void Awake()
    {

        enemyCount = enemyCreatCount;
    }
}
