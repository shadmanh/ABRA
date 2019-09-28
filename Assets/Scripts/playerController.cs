using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private Transform transform;

    bool moveLeft = false;
    bool moveRight = false;
    bool moveUp = false;
    bool moveDown = false;

    private bool snapMovement;
    private bool isStill = true;
    private int prevX;
    private int prevY;
    private int velX = 0;
    private int velY = 0;

    // Start is called before the first frame update
    void Start()
    {
        prevX = (int)transform.position.x;
        prevY = (int)transform.position.y;
    }

    // Update is called once per frame
    private void Update()
    {
        
        moveLeft = Input.GetAxis("Horizontal") < 0;
        moveRight = Input.GetAxis("Horizontal") > 0;
        moveUp = Input.GetAxis("Vertical") > 0;
        moveDown = Input.GetAxis("Vertical") < 0;
    }

    private void FixedUpdate()
    {
        if (isStill)
        {
            isStill = false;
            prevX = (int)transform.position.x;
            prevY = (int)transform.position.y;
            if (moveLeft)
            {
                velX = -5;
            }
            else if (moveRight)
            {
                velX = 5;
            }
            else if (moveUp)
            {
                velY = 5;
            }
            else if (moveDown)
            {
                velY = -5;
            }
            else
            {
                isStill = true;
            }
    
        }
        else
        {
            rb.AddForce(new Vector2(velX, velY), ForceMode2D.Impulse);
            if(Math.Abs(((int)transform.position.x) - prevX) >= 10 || Math.Abs(((int)transform.position.y) - prevY) >= 10)
            {
                isStill = true;
                velX = 0;
                velY = 0;
                rb.velocity = Vector2.zero;
            }
        }
        
    }
}
