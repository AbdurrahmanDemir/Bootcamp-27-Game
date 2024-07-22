using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Transform skillScroll;
    starPower _starPower;

    [Header("Skill Button")]
    [SerializeField] private GameObject[] buttons;

    //[Header("Skill Prefabs")]
    //[SerializeField] private GameObject arrowSpeedPrefabs;
    //[SerializeField] private GameObject attackSpeedPrefabs;

    private void Awake()
    {
        _starPower= GetComponent<starPower>();
    }
    private void Start()
    {
        switch (PlayerPrefs.GetInt("starPower1"))
        {
            case 0:
                Instantiate(buttons[0], skillScroll);
                buttons[0].SetActive(true);
                break;
            case 1:
                Instantiate(buttons[1], skillScroll);
                buttons[1].SetActive(true);
                break;
            case 2:
                Instantiate(buttons[2], skillScroll);
                buttons[2].SetActive(true);
                break;
            case 3:
                Instantiate(buttons[3], skillScroll);
                buttons[3].SetActive(true);
                break;
            case 4:
                Instantiate(buttons[4], skillScroll);
                buttons[4].SetActive(true);
                break;
            case 5:
                Instantiate(buttons[5], skillScroll);
                buttons[5].SetActive(true);
                break;
        }

        switch (PlayerPrefs.GetInt("starPower2"))
        {
            case 0:
                Instantiate(buttons[0], skillScroll);
                buttons[0].SetActive(true);
                break;
            case 1:
                Instantiate(buttons[1], skillScroll);
                buttons[1].SetActive(true);
                break;
            case 2:
                Instantiate(buttons[2], skillScroll);
                buttons[2].SetActive(true);
                break;
            case 3:
                Instantiate(buttons[3], skillScroll);
                buttons[3].SetActive(true);
                break;
            case 4:
                Instantiate(buttons[4], skillScroll);
                buttons[4].SetActive(true);
                break;
            case 5:
                Instantiate(buttons[5], skillScroll);
                buttons[5].SetActive(true);
                break;



        }
    }

    //0. eleman her zaman attackSpeed 
    //    1.eleman her zaman arrowSpeed
    //    2. Eleman her zaman health Recovery speed

    //arrowSpeedPrefabs.GetComponent<Button>().onClick.AddListener(() => _starPower.arrowSpeedPower());

}
