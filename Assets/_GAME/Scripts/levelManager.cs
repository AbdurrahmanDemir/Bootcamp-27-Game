using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelManager : MonoBehaviour
{
    dataManager _dataManager= new dataManager();
    void Start()
    {
        

        if (!PlayerPrefs.HasKey("tutorialscene"))
        {
            StartCoroutine(tutorialLoading());
            _dataManager.GoldCalculator(1000);
            _dataManager.TrophyCalculator(0);
            _dataManager.EnergyCalculator(100);
        }
        else
        {
            SceneManager.LoadScene("menu");
        }
    }

    IEnumerator tutorialLoading()
    {
        SceneManager.LoadScene("menu");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("tutorial");

    }

  
}
