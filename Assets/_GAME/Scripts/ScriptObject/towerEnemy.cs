using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "towerEnemy", menuName = "towerEnemy/enemyData")]

public class towerEnemy : ScriptableObject
{
    public GameObject _enemy;
    public int health;
    public float speed;
    public int damage;
}
