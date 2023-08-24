using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] float torqueAmount = 20f;
    [SerializeField] float jumpAmount = 8f;
    [SerializeField] double maxOffSnowTime = 0.5;
    [SerializeField] float boostAmount = 1.2f;
    [SerializeField] double maxBoostTimer = 1.0;
    [SerializeField] int flipScoreIncr = 1000;
    [SerializeField] int snowflakeScoreIncr = 100;
    [SerializeField] UIController UIPanel;

    public bool touchingSnow = false;
    public int score = 0;
    public int timeBonus = 0;
    public double gameTimer = 0.0;
    public bool canControl = true;
    public Animator animator;

    private bool canJump = true;
    private bool canBoost = true;

    private Rigidbody2D rb2d;
    private double offSnowTimer = 0.0;
    private double boostTimer = 0.0;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canControl) 
        {
            gameTimer += Time.deltaTime;
            SnowExitTimer();
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
    /// Whenever the penguin leaves the platform, start a timer so that the
    /// player is allowed a tiny bit of leeway for jumping.
    /// </summary>
    public void SnowExitTimer() 
    {
        if (!touchingSnow) {
            offSnowTimer += Time.deltaTime;
        } 

        else {
            offSnowTimer = 0.0;
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
            score -= flipScoreIncr;
            rb2d.rotation += 360;
        }
    }

    void TryJump() 
    {
        bool jumpButtonPressed = Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Space);

        if (offSnowTimer < maxOffSnowTime && canJump && jumpButtonPressed) 
        {
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
        
        if (boostTimer > 0 && boostTimer < maxBoostTimer) 
        {
            boostTimer += Time.deltaTime;
            rb2d.velocity = rb2d.velocity.normalized * boostAmount;
        }

        if (touchingSnow && canBoost && Input.GetKey(KeyCode.Z)) 
        {
            animator.SetBool("isBoosting", true);
            boostTimer += Time.deltaTime;
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
    }

    
    void OnTriggerEnter2D(Collider2D other) {
        
        switch(other.tag) 
        {
            case "Obstacle":
                rb2d.velocity = Vector2.zero;
                if (other.GetComponent<AudioSource>()) 
                {
                    other.GetComponent<AudioSource>().Play();
                }
                UIPanel.GameOver();
            break;
            case "Collectible":
                score += snowflakeScoreIncr;
                GetComponent<AudioSource>().Play();
                Destroy(other.gameObject);
            break;
        }
    }
}
