using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject youWinPanel;
    [SerializeField] TextMeshProUGUI baseScore;
    [SerializeField] TextMeshProUGUI timeBonus;
    [SerializeField] TextMeshProUGUI totalScore;
    [SerializeField] PlayerController penguin;

    public void GameOver() 
    {
        activatePanel(gameOverPanel);
    }

    public void YouWin() 
    {
        activatePanel(youWinPanel);
        baseScore.text = "score: " + penguin.score.ToString();
        timeBonus.text = "time bonus: " + penguin.timeBonus.ToString();
        totalScore.text = "total: " + (penguin.score + penguin.timeBonus).ToString();
    }

    public void Restart() 
    {
        SceneManager.LoadScene(0);
    }

    private void activatePanel(GameObject panel) 
    {
        penguin.canControl = false;
        panel.SetActive(true);
    }
}
