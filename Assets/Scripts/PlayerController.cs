 using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] float torqueAmount = 20f;
    [SerializeField] float jumpAmount = 8f;
    [SerializeField] float boostAmount = 1.2f;
    [SerializeField] double maxBoostTimer = 1.0;
    [SerializeField] int flipScoreIncr = 1000;
    [SerializeField] int snowflakeScoreIncr = 100;
    [SerializeField] UIController UIPanel;
    [SerializeField] GameObject zoomEffect;

    public bool touchingSnow = false;
    public int score = 0;
    public int timeBonus = 0;
    public double gameTimer = 0.0;
    public bool canControl = true;
    public Animator animator;

    private bool canJump = true;
    private bool canBoost = true;
    private bool crashPlayed;

    private Rigidbody2D rb2d;
    private double jumpTimer = 0.0;
    private double boostTimer = 0.0;

    // Start is called before the first frame update
    void Start()
    {
        zoomEffect.SetActive(false);
        Time.timeScale = 0;
        canControl = false;
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canControl) 
        {
            gameTimer += Time.deltaTime;
            RotatePlayer();
            TryJump();
            TryBoost();
        }

        else 
        {
            rb2d.velocity = Vector2.zero;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    void RotatePlayer()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb2d.AddTorque(torqueAmount);
        } 

        else if (Input.GetKey(KeyCode.RightArrow))
        {
            rb2d.AddTorque(-torqueAmount);
        }
        
        else 
        {
            rb2d.angularVelocity = 0;
        }

        if (rb2d.rotation >= 360) 
        {
            score += flipScoreIncr;
            rb2d.rotation -= 360;
        }

        if (rb2d.rotation <= -360) 
        {
            score += flipScoreIncr;
            rb2d.rotation += 360;
        }
    }

    void TryJump() 
    {
        bool jumpButtonPressed = Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Space);

        if (canJump && jumpButtonPressed && touchingSnow) 
        {
            jumpTimer += Time.deltaTime;
            rb2d.velocity += Vector2.up * jumpAmount;
            canJump = false;
        }  

        else if (!jumpButtonPressed) 
        {
            canJump = true;
        }
        
    }

    void TryBoost() 
    {
        
        if (boostTimer > 0 && boostTimer < maxBoostTimer && touchingSnow) 
        {
            boostTimer += Time.deltaTime;
            rb2d.velocity = rb2d.velocity.normalized * boostAmount;
        }

        if (touchingSnow && canBoost && Input.GetKey(KeyCode.Z)) 
        {
            animator.SetBool("isBoosting", true);
            boostTimer += Time.deltaTime;
            zoomEffect.SetActive(true);
            canBoost = false;
        }

        else if (!Input.GetKey(KeyCode.Z) && touchingSnow) 
        {
            canBoost = true;
            boostTimer = 0.0;
        }

        else if (!Input.GetKey(KeyCode.Z)) 
        {
            animator.SetBool("isBoosting", false);
        }

        if (boostTimer > maxBoostTimer || !touchingSnow || canBoost)
        {
            zoomEffect.SetActive(false);
        }
    }

    
    void OnTriggerEnter2D(Collider2D other) {
        
        switch(other.tag) 
        {
            case "Collectible":
                score += snowflakeScoreIncr;
                GetComponent<AudioSource>().Play();
                Destroy(other.gameObject);
            break;
            case "CrashPlane":
                UIPanel.GameOver();    
            break;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        switch(other.gameObject.tag)
        {
            case "Obstacle":
                rb2d.velocity = Vector2.zero;
                if (other.gameObject.GetComponent<AudioSource>() && !crashPlayed) 
                {
                    other.gameObject.GetComponent<AudioSource>().Play();
                    crashPlayed = true;
                }
                UIPanel.GameOver();
                break;
        }
    }
}
