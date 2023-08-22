using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject youWinPanel;
    [SerializeField] GameObject penguin;
    [SerializeField] float timeStopDelay = 1f;

    public void GameOver() 
    {
        activatePanel(gameOverPanel);
    }

    public void YouWin() 
    {
        activatePanel(youWinPanel);
    }

    public void Restart() 
    {
        SceneManager.LoadScene(0);
    }

    private void activatePanel(GameObject panel) 
    {
        panel.SetActive(true);
        penguin.GetComponent<PlayerController>().canControl = false;
    }
}
