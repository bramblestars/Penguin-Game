using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script to control snow particles behind penguin
public class SnowTrail : MonoBehaviour
{
    [SerializeField] ParticleSystem snowTrail;

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Ground")
        {
            snowTrail.Play();
        }    
    }

    private void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.tag == "Ground")
        {
            snowTrail.Stop();
        }
    }
}
