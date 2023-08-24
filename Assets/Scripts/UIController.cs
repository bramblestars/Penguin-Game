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
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI baseScore;
    [SerializeField] TextMeshProUGUI timeBonus;
    [SerializeField] TextMeshProUGUI totalScore;
    [SerializeField] PlayerController penguin;

    private double timer = 0.0;

    void Update() 
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
    }

    public void GameOver() 
    {
        ActivatePanel(gameOverPanel);
    }

    public void YouWin() 
    {
        ActivatePanel(youWinPanel);
        baseScore.text = scoreText.text;
        timeBonus.text = "time bonus: " + penguin.timeBonus.ToString();
        totalScore.text = "total: " + (penguin.score + penguin.timeBonus).ToString();
    }

    public void Pause() 
    {
        if (penguin.canControl) {
            Time.timeScale = 0f;
            ActivatePanel(pauseMenuPanel);
        }
    }

    public void Resume() 
    {
        Time.timeScale = 1f;
        penguin.canControl = true;
        pauseMenuPanel.SetActive(false);
    }

    public void Restart() 
    {
        SceneManager.LoadScene(0);
    }


    private void ActivatePanel(GameObject panel) 
    {
        penguin.canControl = false;
        panel.SetActive(true);
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
