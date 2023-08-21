using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] float torqueAmount = 1f;
    [SerializeField] float jumpAmount = 8f;
    [SerializeField] double maxOffSnowTime = 0.5;
    [SerializeField] float boostAmount = 1.2f;
    [SerializeField] double maxBoostTimer = 1.0;

    public bool touchingSnow = false;
    public bool canJump = true;
    public bool canBoost = true;

    private Rigidbody2D rb2d;
    private double offSnowTimer = 0.0;
    private double boostTimer = 0.0;
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
        SnowExitTimer();
        RotatePlayer();
        TryJump();
        TryBoost();
    }

    void RotatePlayer()
    {
        if (UnityEngine.Input.GetKey(KeyCode.LeftArrow))
        {
            rb2d.AddTorque(torqueAmount);
        }
        
        else {
            rb2d.angularVelocity = 0;
        }
    }

    void TryJump() 
    {
        bool jumpButtonPressed = Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Space);

        if (offSnowTimer < maxOffSnowTime && canJump && jumpButtonPressed) {
            rb2d.velocity += Vector2.up * jumpAmount;
            canJump = false;
        }  

        else if (!jumpButtonPressed) {
            canJump = true;
        }
        
    }

    void TryBoost() 
    {
        
        if (boostTimer > 0 && boostTimer < maxBoostTimer) {
            boostTimer += Time.deltaTime;
            rb2d.AddRelativeForce(Vector2.right * boostAmount);
        }

        if (touchingSnow && canBoost && Input.GetKey(KeyCode.RightArrow)) {
            boostTimer += Time.deltaTime;
            canBoost = false;
        }

        else if (!Input.GetKey(KeyCode.RightArrow)) {
            canBoost = true;
            boostTimer = 0.0;
        }
    }

    public void SnowExitTimer() 
    {
        if (!touchingSnow) {
            offSnowTimer += Time.deltaTime;
        } 

        else {
            offSnowTimer = 0.0;
        }
    }
}
