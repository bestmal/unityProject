using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public delegate void GameDelegate();
    public static event GameDelegate OnGameStarted;
    public static event GameDelegate OnGameOverConfirmed;
    
    public static GameManager Instance;

    public GameObject startPage;
    public GameObject gameOverPage;
    public GameObject countdownPage;
    public Text scoreText;
  

    enum PageState
    {
        None, 
        Start,
        GameOver,
        Countdown   
    }
    int wynik = 0;
    bool gameOver = true;

    public bool GameOver { get { return gameOver;} }
    public int Score { get { return wynik; } }
    void Awake()
    {
        Instance = this;
    }
    void OnEnable()
    {
        Text.OnCountdownFinished += OnCountdownFinished;
        TapController.OnPlayerDied += OnPlayerDied;
        TapController.OnPlayerDied += OnPlayerScored;
    }
    void OnDisable()
    {
        Text.OnCountdownFinished -= OnCountdownFinished;
        TapController.OnPlayerDied -= OnPlayerDied;
        TapController.OnPlayerDied -= OnPlayerScored;
    }

    void OnCountdownFinished()
    {
 
        SetPageState(PageState.None);
        OnGameStarted();
        wynik = 0;
        gameOver = false;
    }
    void OnPlayerDied()
    {
        gameOver = true;
        int savedScore = PlayerPrefs.GetInt("Wynik");
        if (wynik > savedScore)
        {
            PlayerPrefs.SetInt("Twój Wynik", wynik);
        }
        SetPageState(PageState.GameOver);
    }
    void OnPlayerScored()
    {
        wynik++;
        scoreText.text = wynik.ToString();

    }

    void SetPageState(PageState state)
    {
        switch (state)
        {
            case PageState.None:
                startPage.SetActive(false);
                gameOverPage.SetActive(false);
                countdownPage.SetActive(false);
                break;
                
            case PageState.Start:
                startPage.SetActive(true);
                gameOverPage.SetActive(false);
                countdownPage.SetActive(false);
                break;

            case PageState.GameOver:
                startPage.SetActive(false);
                gameOverPage.SetActive(true);
                countdownPage.SetActive(false);
                break;

            case PageState.Countdown:
                startPage.SetActive(false);
                gameOverPage.SetActive(false);
                countdownPage.SetActive(true);
                break;
        }
    }
    public void ConfirmGameOver()
    {
        OnGameOverConfirmed();
        scoreText.text = "0";
        SetPageState(PageState.Start);
    }
    public void StartGame()
    {
        SetPageState(PageState.Countdown);
    }
}

