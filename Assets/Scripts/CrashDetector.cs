using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrashDetector : MonoBehaviour
{
    [SerializeField] UIController UIPanel;

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Ground") {
            UIPanel.GameOver();
        }
    }
    
}
