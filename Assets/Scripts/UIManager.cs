using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI goalsTxt;
    public GameObject winScreen;
    public GameObject lostScreen;
    public GameObject[] stars;
    public ThingRuntimeSet amountOfGoals;
    public LevelSettings levelSettings;

    public FloatReference scoredGoals;
    public FloatReference leftAmmo;




    private void Start()
    {
        UpdateScore();
    }

    public void UpdateScore()
    {
        goalsTxt.text = scoredGoals.Value + "/" + amountOfGoals.Items.Count;
    }

    public void DisplayGameWonMenu()
    {
        winScreen.SetActive(true);
    }

    public void DisplayGameLostMenu()
    {
        lostScreen.SetActive(true);
    }

    public void CalculateStars()
    {
        print("Score: " + leftAmmo.Value * 100);
        int starActive = 0;

        // Calculate stars
        if (leftAmmo >= levelSettings.star3)
            starActive = 3;
        else if (leftAmmo >= levelSettings.star2)
            starActive = 2;
        else if (leftAmmo >= levelSettings.star1)
            starActive = 1;
        else
            starActive = 0;

        // Display Stars
        for(int i=0; i<starActive; i++)
        {
            stars[i].SetActive(true);
        }
    }

}
