using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading;
using System;

public class UIController : MonoBehaviour
{
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject youWinPanel;
    [SerializeField] GameObject pauseMenuPanel;
    [SerializeField] GameObject instructionsPanel;
    [SerializeField] GameObject scoreBoardPanel;
    [SerializeField] GameObject optionsPanel;
    [SerializeField] GameObject creditsPanel;


    [SerializeField] TextMeshProUGUI leaderboardScore;
    [SerializeField] GameObject namesAndScores;
    [SerializeField] GameObject submissionSection;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI baseScore;
    [SerializeField] TextMeshProUGUI timeBonus;
    [SerializeField] TextMeshProUGUI totalScore;
    [SerializeField] PlayerController penguin;
    [SerializeField] private TextMeshProUGUI inputScoreText;
    private AudioSource crashSound;

    private double timer = 0.0;
    private GameObject currentActivePanel;
    private GameObject previousActivePanel;

    private void Start() 
    {
        currentActivePanel = instructionsPanel;
    }

    private void Update() 
    {
        if (penguin.canControl) 
        {
            scoreText.text = "score: " + penguin.score.ToString();
            timerText.text = GetTime();
            timer += Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.Escape)) 
        {
            Pause();
        }

        if (instructionsPanel.activeSelf && Input.GetKey(KeyCode.Return))
        {
            DismissInstructions();
        }
    }

    public void GameOver() 
    {
        ActivatePanel(gameOverPanel);
        inputScoreText.text = penguin.score.ToString();
        previousActivePanel = gameOverPanel;
    }

    public void YouWin() 
    {
        ActivatePanel(youWinPanel);
        baseScore.text = scoreText.text;
        timeBonus.text = "time bonus: " + penguin.timeBonus.ToString();
        totalScore.text = "total: " + (penguin.score + penguin.timeBonus).ToString();
        leaderboardScore.text = totalScore.text.Substring(7);
        previousActivePanel = youWinPanel;
    }

    public void Pause() 
    {
        if (penguin.canControl) {
            Time.timeScale = 0f;
            ActivatePanel(pauseMenuPanel);
            previousActivePanel = pauseMenuPanel;
        }
    }

    public void Resume() 
    {
        Time.timeScale = 1f;
        penguin.canControl = true;
        currentActivePanel.SetActive(false);
    }

    public void Restart() 
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void DismissInstructions()
    {
        currentActivePanel = instructionsPanel;
        Resume();
    }

    public void ShowSubmissionLeaderboard() 
    {
        currentActivePanel.SetActive(false);
        ActivatePanel(scoreBoardPanel);
        submissionSection.SetActive(true);
        namesAndScores.transform.localPosition = Vector3.zero;
    }

    public void ShowLeaderboard() 
    {
        currentActivePanel.SetActive(false);
        ActivatePanel(scoreBoardPanel);
        submissionSection.SetActive(false);
        namesAndScores.transform.localPosition = new Vector3(-205, 20, 0);
    }

    public void ShowOptions() 
    {
        currentActivePanel.SetActive(false);
        ActivatePanel(optionsPanel);
    }

    public void ShowCredits()
    {
        currentActivePanel.SetActive(false);
        ActivatePanel(creditsPanel);
    }

    public void ReturnToPreviousPanel() 
    {
        currentActivePanel.SetActive(false);
        previousActivePanel.SetActive(true);
        currentActivePanel = previousActivePanel;
    }


    private void ActivatePanel(GameObject panel) 
    {
        if (currentActivePanel)
        {
            // turn off current active panel first
            currentActivePanel.SetActive(false);
        }
        
        // turn on new panel
        panel.SetActive(true);

        currentActivePanel = panel;

        penguin.canControl = false;
    }

    private string GetTime() 
    {
        int minutes = (int) Math.Floor(timer / 60);
        int seconds = (int) Math.Floor(timer % 60);
        string mins = minutes < 10 ? "0" + minutes.ToString() : minutes.ToString();
        string secs = seconds < 10 ? "0" + seconds.ToString() : seconds.ToString();
        return mins + ":" + secs;
    }
}
