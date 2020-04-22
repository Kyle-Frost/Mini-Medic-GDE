﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameVariables : MonoBehaviour
{
    public bool inTutorial;
    private bool alliesSpawned = false;

    public float playerSpeed = 125f;
    public float riskMeterSpeed = 5f;
    public float countdownStartingTime = 3f;
    public float bleedoutSpeed = 3f;

    public int lives = 3;
    public Image lifeOne;
    public Image lifeTwo;
    public Image lifeThree;

    public GameObject lossScreen;
    public GameObject gameUI;
    public Button exitButton;
    public Button restartButton;

    public ClockUI clockScript;
    public MouseInput inputScript;

    private void Start()
    {
        exitButton.onClick.AddListener(ExitGame);
        restartButton.onClick.AddListener(RestartGame);
        lives = 3;

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Game"))
        {
            inTutorial = true;
        }
        else
        {
            inTutorial = false;
        }
    }
    private void Update()
    {
        if (lives == 3)
        {
            lifeOne.enabled = true;
            lifeTwo.enabled = true;
            lifeThree.enabled = true;
        }
        if (lives == 2)
        {
            lifeOne.enabled = true;
            lifeTwo.enabled = true;
            lifeThree.enabled = false;
        }
        if (lives == 1)
        {
            lifeOne.enabled = true;
            lifeTwo.enabled = false;
            lifeThree.enabled = false;
        }
        if (lives == 0)
        {
            lifeOne.enabled = false;
            lifeTwo.enabled = false;
            lifeThree.enabled = false;

            lossScreen.SetActive(true);
            gameUI.SetActive(false);
            Time.timeScale = 1;
        }

        if (clockScript.wholeTime % 12 == 0 && !inTutorial && !alliesSpawned)
        {
            inputScript.spawnAllies();
            alliesSpawned = true;
        }
        if (clockScript.wholeTime != 0 && (clockScript.wholeTime -1) % 12 == 0 && !inTutorial && alliesSpawned)
        {
            alliesSpawned = false;
        }
    }

    void RestartGame() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        lossScreen.SetActive(false);
    }

    void ExitGame()
    {
        SceneManager.LoadScene(1);
    }
}