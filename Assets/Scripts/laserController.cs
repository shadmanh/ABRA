using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class laserController : MonoBehaviour
{
    [SerializeField]
    laserController laser;

    private Vector2 pos;
    private int posx;
    private int posy;
    private laserController next = null;
    
    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;
        posx = (int)pos.x/World.blockSize;
        posy = -1*(int)pos.y/World.blockSize;
        World.grid[posy][posx] = World.LASER; 
    }

    // Update is called once per frame
    public void UpdateLaser()
    {
        if (next == null)
        {
            int curPosY = posy + 1;
            laserController myLaser = null;
            laserController prevLaser = null;
            while (World.grid[curPosY][posx] == World.FREE || World.grid[curPosY][posx] == World.PLAYER)
            {
                if (World.grid[curPosY][posx] == World.PLAYER)
                {
                    SceneManager.LoadScene(2);
                }
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

    public void OnDestroy()
    {
        World.grid[posy][posx] = World.FREE;
        World.lasers.Remove(this);
        if (next != null)
        {
            Destroy(next.gameObject);
        }
    }

    public void setNext(laserController lc)
    {
        next = lc;
    }

    public int getGridX()
    {
        return posx;
    }

    public int getGridY()
    {
        return posy;
    }
}
