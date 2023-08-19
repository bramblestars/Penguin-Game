using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script to control snow particles behind penguin
public class SnowTrail : MonoBehaviour
{
    [SerializeField] ParticleSystem snowTrail;
    public PlayerController playerController;

    private void Start() {
        playerController = GetComponent<PlayerController>();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Ground")
        {
            playerController.canJump = true;
            snowTrail.Play();
        }    
    }

    private void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.tag == "Ground")
        {
            playerController.canJump = false;
            snowTrail.Stop();
        }
    }
}
