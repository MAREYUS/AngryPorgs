using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;

    private GameObject goalParent;

    public enum GameState{
        Active,
        Won,
        Lost
    }

    // UI
    private GameObject endScreen;
    private TextMeshProUGUI goalTxt;

    private int goalAmount;
    private int goalHitAmount;

    private bool outOfAmmo;

    // Properties
    public int GoalAmount { get { return goalAmount; } }
    public int GoalHitAmount { get { return goalHitAmount; } }

    private void Awake()
    {
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
        
        
    }

    private void Update()
    {
        // game Won
        if(goalHitAmount == GoalAmount)
        {
            CalculateStars();
            GameWon();
        }

        // Game Lost
        if (outOfAmmo && Input.GetMouseButton(0)) 
            GameLost();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += ResetLevel;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= ResetLevel;
    }


    // restart the Game
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // load next Level
    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OnGoalHit()
    {
        goalHitAmount++;
        goalTxt.text = goalHitAmount + "/" + goalAmount;
    }

    private void ResetLevel(Scene scene, LoadSceneMode mode)
    {
        print("Reset Level");
        outOfAmmo = false;
        goalParent = GameObject.Find("Goals");
        goalAmount = goalParent.transform.childCount;
        goalHitAmount = 0;
        endScreen = GameObject.Find("EndScreen");
        goalTxt = GameObject.Find("GoalTxt").GetComponent<TextMeshProUGUI>();

        endScreen.SetActive(false);
        goalTxt.text = goalHitAmount + "/" + goalAmount;
    }

    public void OnOutOfAmmo()
    {
        outOfAmmo = true;
    }
    
    private void GameLost()
    {
        print("Game lost");
        endScreen.SetActive(true);
        GameObject.Find("ResultTxt").GetComponent<TextMeshProUGUI>().text = "YOU LOST";
        GameObject.Find("NextLevelBtn").SetActive(false);
    }

    private void GameWon()
    {
        print("Game won");
        endScreen.SetActive(true);
        GameObject.Find("ResultTxt").GetComponent<TextMeshProUGUI>().text = "YOU WIN";
        GameObject.Find("NextLevelBtn").SetActive(true);
    }

    void CalculateStars()
    {
    }
}
