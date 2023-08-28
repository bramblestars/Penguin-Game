using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareCollider : MonoBehaviour
{
    [SerializeField] PlayerController penguin;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        switch(other.tag)
        {
            case "Ground":
                penguin.touchingSnow = true;
                break;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        switch(other.tag)
        {
            case "Ground":
                penguin.touchingSnow = false;
                break;
        }
    }
}
