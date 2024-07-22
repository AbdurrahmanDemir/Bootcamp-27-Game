using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dataManager
{

    public void GoldCalculator(int gold)
    {
        if (PlayerPrefs.HasKey("gold"))
        {
            int oldGold = PlayerPrefs.GetInt("gold");
            PlayerPrefs.SetInt("gold", oldGold + gold);
        }
        else
            PlayerPrefs.SetInt("gold", gold);

        Debug.Log(PlayerPrefs.GetInt("gold"));
    }
    public void reduceGold(int gold)
    {
        int money = PlayerPrefs.GetInt("moneyy");
        PlayerPrefs.SetInt("moneyy", money - gold);
    }
    public void TrophyCalculator(int trophy)
    {
        if (PlayerPrefs.HasKey("trophy"))
        {
            int oldTrophy = PlayerPrefs.GetInt("trophy");
            PlayerPrefs.SetInt("trophy", oldTrophy + trophy);
        }
        else
            PlayerPrefs.SetInt("trophy", trophy);

        Debug.Log(PlayerPrefs.GetInt("trophy"));
    }
    public void EnergyCalculator(int energy)
    {
        if (PlayerPrefs.HasKey("energy"))
        {
            int oldTrophy = PlayerPrefs.GetInt("energy");
            PlayerPrefs.SetInt("energy", oldTrophy + energy);
        }
        else
            PlayerPrefs.SetInt("energy", energy);

        Debug.Log(PlayerPrefs.GetInt("energy"));
    }

    public void expCalculator(int exp)
    {
        if (PlayerPrefs.HasKey("exp"))
        {
            int oldTrophy = PlayerPrefs.GetInt("exp");
            PlayerPrefs.SetInt("exp", oldTrophy + exp);
        }
        else
            PlayerPrefs.SetInt("exp", exp);

        Debug.Log(PlayerPrefs.GetInt("exp"));
    }

}
