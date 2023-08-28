using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    [SerializeField] UIController uiPanel;
    private void Awake() 
    {
        GameObject[] music = GameObject.FindGameObjectsWithTag("Music");
        if (music.Length > 1) 
        {
            Destroy(this.gameObject);
        }
        else 
        {
            DontDestroyOnLoad(this);
        }
    }
}
