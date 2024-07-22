using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "tower", menuName = "tower/towerData")]
public class tower : ScriptableObject
{
    public GameObject _tower;
    public int damage /*= PlayerPrefs.GetInt("towerDamage")*/;
    public int health/*= PlayerPrefs.GetInt("Health")*/;
    public float arrowSpeed/*= PlayerPrefs.GetFloat("Speed")*/;
    public float attackSpeed;
    public float range;
    public float healthRecovery;
}
