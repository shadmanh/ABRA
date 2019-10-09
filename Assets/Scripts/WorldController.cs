using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldController : MonoBehaviour
{
    private int px;
    private int py;
    private int mx;
    private int my;

    private bool playerMoved = false;
    private bool mechMoved = false;

    // Start is called before the first frame update
    void Start()
    {
        GameObject go = GameObject.Find("player");
        Vector2 pos = go.GetComponent<playerController>().transform.position;
        px = (int)pos.x/World.blockSize;
        py = -1*(int)pos.y/World.blockSize;
        
        go = GameObject.Find("mech");
        pos = go.GetComponent<machineController>().transform.position;
        mx = (int)pos.x/World.blockSize;
        my = -1*(int)pos.y/World.blockSize;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerMoved)
        {
            GameObject go = GameObject.Find("player");
            Vector2 pos = go.GetComponent<playerController>().transform.position;
            int oldpx = (int)pos.x/World.blockSize;
            int oldpy = -1*(int)pos.y/World.blockSize;
            if (World.grid[py][px] == World.FREE)
            {
                World.grid[oldpy][oldpx] == World.FREE;
        }

        playerMoved = false;
        mechMoved = false;
    }

    void setPlayerPos(int posx, int posy) {
        px = posx;
        py = posy;
        playerMoved = true;
    }

    void setMachinePos(int posx, int posy) {
        mx = posx;
        my = posy;
        mechMoved = true;
    }
}
