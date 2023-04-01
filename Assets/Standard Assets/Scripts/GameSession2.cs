using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//using TMPro;


public class GameSession2 : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    static int score = 0;

    //CoinPickup coin;

    //[SerializeField] TextMeshProUGUI livesText;
   // [SerializeField] TextMeshProUGUI scoreText;
   [SerializeField] Text livesText;
   [SerializeField] Text scoreText;
    public int GetScore() {return score;}
    public void SetScore(int v) {score = v;}

    // Awake() is executed first in order of execution. Used for initialization
    void Awake()
    {
        int numGameSessions = FindObjectsOfType<GameSession2>().Length;
        if (numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);   
        }
        //coin = FindObjectOfType<CoinPickup>();
        //score = coin.GetSavedScore();
    }

    void Start()
    {
        scoreText.text = score.ToString();
        livesText.text = playerLives.ToString();
    }

    void Update() 
    {
        scoreText.text = score.ToString();
    }

    public void ProcessPlayerDeath()
    {
        if(playerLives > 1)
        {
            TakeLife();
        }
        else
        {
            ResetGameSession();
        }
    }

    void ResetGameSession()
    {
        FindObjectOfType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(0);
        // Important destroying this session to reset
        Destroy(gameObject);
    }

    void TakeLife()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        
        playerLives--;
        SceneManager.LoadScene(currentSceneIndex);
        livesText.text = playerLives.ToString();

    }

    public void IncreasingScore(int v)
    {
        score += v;
    }
}
