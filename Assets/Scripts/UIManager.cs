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
        int earnedStars;

        // Calculate stars based on LevelSettings
        if (leftAmmo >= levelSettings.star3)
            earnedStars = 3;
        else if (leftAmmo >= levelSettings.star2)
            earnedStars = 2;
        else if (leftAmmo >= levelSettings.star1)
            earnedStars = 1;
        else
            earnedStars = 0;

        // Display Stars
        for(int i=0; i<earnedStars; i++)
        {
            stars[i].SetActive(true);
        }
    }

}
