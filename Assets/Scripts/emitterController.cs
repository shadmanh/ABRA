using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class emitterController : MonoBehaviour
{
    [SerializeField]
    private laserController laser;

    private Vector2 pos;
    private int posx;
    private int posy;
    
    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;
        posx = (int)pos.x/World.blockSize;
        posy = -1*(int)pos.y/World.blockSize;
        World.grid[posy][posx] = World.BOUNDARY;
    }

    // Update is called once per frame
    void Update()
    {
        World.grid[posy][posx] = World.BOUNDARY;
        int curPosY = posy+1;
        laserController myLaser = null;
        laserController prevLaser = null;
        while (World.grid[curPosY][posx] == World.FREE)
        {
            myLaser = Instantiate(laser, new Vector3(pos.x, -1*curPosY*World.blockSize, 0), Quaternion.identity);
            if (prevLaser != null)
            {
                prevLaser.setNext(myLaser);
            }
            prevLaser = myLaser;
            curPosY++;
            World.lasers.Add(myLaser);
        }
    }
}
