using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    [SerializeField] ParticleSystem finishEffect;
    [SerializeField] PlayerController playerController;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            finishEffect.Play();
            GetComponent<AudioSource>().Play();
            playerController.score += 50000 / (int)playerController.gameTimer;
        }
    }

    
}
