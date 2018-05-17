using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;
    

    public enum GameState{
        Active,
        Inactive
    }

    public FloatVariable goalHitAmount;
    public ThingRuntimeSet goalAmount;

    public UnityEvent GameWonEvent;
    public UnityEvent GameLostEvent;

    private bool outOfAmmo;
    public static GameState currentGameState;

    private void Awake()
    {
        currentGameState = GameState.Active;
        goalHitAmount.Value = 0;
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        //DontDestroyOnLoad(gameObject);
        
        
    }

    private void Update()
    {
        // game Won
        if (goalAmount.Items.Count == goalHitAmount.Value)
        {
            GameWon();
            GameWonEvent.Invoke();
        }

        // Game Lost
        if (outOfAmmo && Input.GetMouseButton(0))
        {
            GameLost();
            GameLostEvent.Invoke();
        }
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
        if(SceneManager.GetActiveScene().buildIndex +1 < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    private void ResetLevel(Scene scene, LoadSceneMode mode)
    {
        currentGameState = GameState.Active;
        print("Reset Level");
        outOfAmmo = false;
        goalHitAmount.Value = 0;
    }

    public void OnOutOfAmmo()
    {
        outOfAmmo = true;
    }
    
    private void GameLost()
    {
        currentGameState = GameState.Inactive;
    }

    private void GameWon()
    {
        currentGameState = GameState.Inactive;
    }
    
    public void UpdateGoalHitAmount()
    {
        goalHitAmount.Value++;
    }


}
