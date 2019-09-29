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
    [SerializeField]
    private float speed = 1.0f;

    bool moveLeft = false;
    bool moveRight = false;
    bool moveUp = false;
    bool moveDown = false;

    private bool snapMovement;
    public bool isStill = true;
    private Vector2 pos;
    private int velX = 0;
    private int velY = 0;

    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
            moveLeft = Input.GetKey(KeyCode.LeftArrow);
            moveRight = Input.GetKey(KeyCode.RightArrow);
            moveUp = Input.GetKey(KeyCode.UpArrow);
            moveDown = Input.GetKey(KeyCode.DownArrow);
    }

    private void FixedUpdate()
    {
        if (transform.position.x == pos.x && transform.position.y == pos.y)
        {
            if (moveLeft)
            {
                pos += new Vector2(-10,0);
            }
            else if (moveRight)
            {
                pos += new Vector2(10,0);
            }
            else if (moveUp)
            {
                pos += new Vector2(0,10);
            }
            else if (moveDown)
            {
                pos += new Vector2(0,-10);
            }
        }

        transform.position = Vector2.MoveTowards(transform.position, pos, Time.deltaTime * speed);
        
    }
}
