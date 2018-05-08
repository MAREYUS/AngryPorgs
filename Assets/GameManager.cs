using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	
    public enum GameState{
        Active,
        Won,
        Lost
    }


    private int goalAmount;

    private void Start()
    {
        goalAmount = GameObject.Find("Goals").transform.childCount;
        print("Goal Amount: " + goalAmount);
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

    // subscribe to GoalHit
    private void OnEnable()
    {
        Goal.GoalHitEvent += GoalHit;
    }

    public void GoalHit()
    {
        print("Goal Hit! (GameManager)");
    }

}
