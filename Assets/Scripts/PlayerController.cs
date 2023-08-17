using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerController : MonoBehaviour
{

    [SerializeField] float torqueAmount = 1f;
    [SerializeField] float boostSpeed = 30f;
    [SerializeField] float baseSpeed = 20f;

    private Rigidbody2D rb2d;
    SurfaceEffector2D surfaceEffector2D;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        surfaceEffector2D = FindObjectOfType<SurfaceEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {
        RotatePlayer();
        RespondToBoost();
    }

    void RespondToBoost() 
    {
        // if we push up, then speed up, otherwise stay normal speed
        if (UnityEngine.Input.GetKey(KeyCode.UpArrow)) 
        {
            surfaceEffector2D.speed = boostSpeed;
        }
    }

    void RotatePlayer()
    {
        if (UnityEngine.Input.GetKey(KeyCode.LeftArrow))
        {
            rb2d.AddTorque(torqueAmount);
        }

        else if (UnityEngine.Input.GetKey(KeyCode.RightArrow))
        {
            rb2d.AddTorque(-torqueAmount);
        }
    }
}
