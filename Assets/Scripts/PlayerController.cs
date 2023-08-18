using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] float torqueAmount = 1f;
    [SerializeField] float jumpAmount = 8f;
    [SerializeField] double maxJumpTime = 1.0;
    [SerializeField] float airResistance = 2f;
    [SerializeField] float baseSpeed = 20f;

    private Rigidbody2D rb2d;
    private double jumpTime = 0.0;
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
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Space)) {
            jumpTime += Time.deltaTime;
        }

        // if we push up or jump, jump once
        if (jumpTime > 0.0 && jumpTime < maxJumpTime)
        {
            jumpTime += Time.deltaTime;
            rb2d.AddForce(Vector2.left * airResistance);
            rb2d.AddForce(Vector2.up * jumpAmount);
        } 
        else if (!Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.Space))
        {
            jumpTime = 0.0;
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
