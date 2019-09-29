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

    private machineController machine;

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
        posx = (int)transform.position.x/World.blockSize;
        posy = -1*(int)transform.position.y/World.blockSize;
        World.grid[posy][posx] = 4;

        GameObject go = GameObject.Find("mech");
        machine = go.GetComponent<machineController>();
    }

    // Update is called once per frame
    private void Update()
    {
        moveLeft = Input.GetKey(KeyCode.LeftArrow);
        moveRight = Input.GetKey(KeyCode.RightArrow);
        moveUp = Input.GetKey(KeyCode.UpArrow);
        moveDown = Input.GetKey(KeyCode.DownArrow);
    }

    //0 is a free space
    //1 is the finish block
    //2 is the machine piece
    //3 is a boundary block
    //4 is the player
    private void FixedUpdate()
    {
        if (transform.position.x == posx*World.blockSize && transform.position.y == posy*(-World.blockSize))
        {
            int newPosX = moveLeft ? posx - 1 : posx;
            newPosX = moveRight ? posx + 1 : newPosX;
            int newPosY = moveDown ? posy + 1 : posy;
            newPosY = moveUp ? posy - 1 : newPosY;
            if (moveLeft || moveRight)
            {
                newPosY = posy;
            }
            //If the player is trying to move to an open space/finish space
            if (World.grid[newPosY][newPosX] <= 1)
            {
                World.grid[newPosY][newPosX] = 1;
                World.grid[posy][posx] = 0;
                pos += new Vector2((newPosX-posx)*World.blockSize, (posy-newPosY)*World.blockSize);
                posx = newPosX;
                posy = newPosY;
            }
            //If the player is trying to push the machine
            else if (World.grid[newPosY][newPosX] == 2)
            {
                //Pushing it left
                if (newPosX == posx - 1 && machine.MoveLeft() )
                {
                    World.grid[posy][posx - 1] = 4; 
                    World.grid[posy][posx] = 0;
                    pos += new Vector2((newPosX-posx)*World.blockSize, (posy-newPosY)*World.blockSize);
                    posx = newPosX;
                    posy = newPosY;
            PrintGrid();
                }
                
                //Pushing it right
                else if (newPosX == posx + 1 && machine.MoveRight() )
                {
                    World.grid[posy][posx + 1] = 4; 
                    World.grid[posy][posx] = 0;
                    pos += new Vector2((newPosX-posx)*World.blockSize, (posy-newPosY)*World.blockSize);
                    posx = newPosX;
                    posy = newPosY;
            PrintGrid();
                }
                
                //Pushing it down
                else if (newPosY == posy + 1 && machine.MoveDown() )
                {
                    World.grid[posy + 1][posx] = 4;
                    World.grid[posy][posx] = 0;
                    pos += new Vector2((newPosX-posx)*World.blockSize, (posy-newPosY)*World.blockSize);
                    posx = newPosX;
                    posy = newPosY;
            PrintGrid();
                }
                
                //Pushing it up
                else if (newPosY == posy - 1 && machine.MoveUp() )
                {
                    World.grid[posy - 1][posx] = 4;
                    World.grid[posy][posx] = 0;
                    pos += new Vector2((newPosX-posx)*World.blockSize, (posy-newPosY)*World.blockSize);
                    posx = newPosX;
                    posy = newPosY;
                }
            PrintGrid();
            }
        }

        transform.position = Vector2.MoveTowards(transform.position, pos, Time.deltaTime * speed);
    }

    void PrintGrid()
    {
        String line = "";
        for (int y=0; y<World.grid.Length; ++y)
        {
            for (int x=0; x<World.grid[y].Length; ++x)
            {
                line += World.grid[y][x] + " ";
            }
            line += "\n";
        }
        Debug.Log(line);
    }
}
