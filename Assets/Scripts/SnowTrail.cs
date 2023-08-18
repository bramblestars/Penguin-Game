using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowTrail : MonoBehaviour
{
    [SerializeField] ParticleSystem snowTrail;

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Ground")
        {
            Debug.Log("hi");
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
