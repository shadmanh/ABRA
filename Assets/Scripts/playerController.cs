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
    [SerializeField]
    private int[,] world;
    //0 is a free space
    //1 is the player
    //2 is the machine piece
    //3 is a boundary block

    bool moveLeft = false;
    bool moveRight = false;
    bool moveUp = false;
    bool moveDown = false;

    private Vector2 pos;
    private int posx;
    private int posy;


    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;
        posx = 9;
        posy = 4;
        world = new int[,] { 
            { 3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3 },
            { 3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3 },
            { 3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3 },
            { 3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3 },
            { 3,0,0,0,0,0,2,0,0,1,0,0,0,0,0,0,0,3 },
            { 3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3 },
            { 3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3 },
            { 3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3 },
            { 3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3 },
            { 3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3 }
        };
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
            int newPosX = moveLeft ? posx - 1 : posx;
            newPosX = moveRight ? posx + 1 : newPosX;
            int newPosY = moveDown ? posy + 1 : posy;
            newPosY = moveUp ? posy - 1 : newPosY;
            if (moveLeft || moveRight)
            {
                newPosY = posy;
            }
            if (world[newPosY, newPosX] == 0)
            {
                world[newPosY, newPosX] = 1;
                world[posy, posx] = 0;
                pos += new Vector2((newPosX-posx)*10, (posy-newPosY)*10);
                posx = newPosX;
                posy = newPosY;
            }
        }

        transform.position = Vector2.MoveTowards(transform.position, pos, Time.deltaTime * speed);
    }
}
