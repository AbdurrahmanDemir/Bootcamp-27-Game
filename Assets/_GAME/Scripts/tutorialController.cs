using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialController : MonoBehaviour
{
    public GameObject tutorialPanel;
   
    private void Start()
    {

        PlayerPrefs.SetInt("tutorialscene", 1);


    }
    public void tutorialEnd()
    {
        tutorialPanel.SetActive(false);
    }
}
