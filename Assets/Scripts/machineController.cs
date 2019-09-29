using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class machineController : MonoBehaviour
{
    private int posx;
    private int posy;

    [SerializeField]
    private Transform transform;
    [SerializeField]
    private float speed = 1.0f;

    private Vector2 pos;

    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;
        posx = (int)transform.position.x/World.blockSize;
        posy = -1*(int)transform.position.y/World.blockSize;
        World.grid[posy][posx] = 2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        //pos += new Vector2(posx*World.blockSize, posy*World.blockSize);
        transform.position = Vector2.MoveTowards(transform.position, pos, Time.deltaTime * speed);
    }

    public bool MoveLeft()
    {
        if (World.grid[posy][posx - 1] <= 1)
        {
            World.grid[posy][posx - 1] = 2;
            World.grid[posy][posx] = 0;
            posx = posx - 1;
            pos += new Vector2(-World.blockSize, 0);
            return true;
        }
        return false;
    }

    public bool MoveRight()
    {
        if (World.grid[posy][posx + 1] <= 1)
        {
            World.grid[posy][posx + 1] = 2;
            World.grid[posy][posx] = 0;
            posx = posx + 1;
            pos += new Vector2(World.blockSize, 0);
            return true;
        }
        return false;
    }

    public bool MoveDown()
    {
        if (World.grid[posy + 1][posx] <= 1)
        {
            World.grid[posy + 1][posx] = 2;
            World.grid[posy][posx] = 0;
            posy = posy + 1;
            pos += new Vector2(0, -World.blockSize);
            return true;
        }
        return false;
    }

    public bool MoveUp()
    {
        if (World.grid[posy - 1][posx] <= 1)
        {
            World.grid[posy - 1][posx] = 2;
            World.grid[posy][posx] = 0;
            posy = posy - 1;
            pos += new Vector2(0, World.blockSize);
            return true;
        }
        return false;
    }
}
