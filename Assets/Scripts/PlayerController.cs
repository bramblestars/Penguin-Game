using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] float torqueAmount = 1f;
    [SerializeField] float jumpAmount = 8f;
    [SerializeField] double maxJumpTime = 1.0;

    public bool touchingSnow = false;
    public bool canJump = true;

    private Rigidbody2D rb2d;
    private double jumpTime = 0.0;
    SurfaceEffector2D surfaceEffector2D;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        surfaceEffector2D = FindObjectOfType<SurfaceEffector2D>();
        jumpTime = 0.0;
    }

    // Update is called once per frame
    void Update()
    {
        RotatePlayer();
        TryJump();
    }

    void TryJump() 
    {
        Debug.Log(touchingSnow);
        if (touchingSnow && canJump && (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Space))) {
            rb2d.velocity += Vector2.up * jumpAmount;
            jumpTime += Time.deltaTime;
            canJump = false;
        }  

        else if (!(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Space))) {
            canJump = true;
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

        else {
            rb2d.angularVelocity = 0;
        }
    }
}
