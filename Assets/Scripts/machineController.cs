using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class machineController : MonoBehaviour
{
    private int posx;
    private int posy;

    [SerializeField]
    private Transform transform;
    [SerializeField]
    private float speed = 1.0f;
    [SerializeField]
    private int nextScene;

    private playerController player;

    private Vector2 pos;

    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;
        posx = (int)transform.position.x/World.blockSize;
        posy = -1*(int)transform.position.y/World.blockSize;
        World.grid[posy][posx] = World.MACHINE;

        GameObject go = GameObject.Find("player");
        player = go.GetComponent<playerController>();
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
        if (World.grid[posy][posx - 1] <= World.FREE_OR_FINISH || World.grid[posy][posx - 1] == World.PLAYER && player.MoveLeft()
            || World.grid[posy][posx - 1] == World.LASER)
        {
            if (World.grid[posy][posx - 1] == World.FINISH)
            {
                SceneManager.LoadScene(nextScene);
            }
            else if (World.grid[posy][posx - 1] == World.LASER)
            {
                Vector3 position = new Vector3(3, 3, 3);
                foreach (laserController laser in World.lasers)
                {
                    if (laser.getGridX() == posx-1 && laser.getGridY() == posy)
                    {
                        Destroy(laser.gameObject);
                        break;
                    }
                }
            }

            World.grid[posy][posx - 1] = World.MACHINE;
            World.grid[posy][posx] = World.FREE;
            posx = posx - 1;
            pos += new Vector2(-World.blockSize, 0);
            player.PrintGrid();
            if (World.lasers.Count > 0)
            {
                laserController laser2 = World.lasers[World.lasers.Count-1];
                laser2.UpdateLaser();
            }

            return true;
        }
        return false;
    }

    public bool MoveRight()
    {
        if (World.grid[posy][posx + 1] <= World.FREE_OR_FINISH)
        {
            if (World.grid[posy][posx + 1] == World.FINISH)
            {
                SceneManager.LoadScene(nextScene);
            }
            World.grid[posy][posx + 1] = World.MACHINE;
            World.grid[posy][posx] = World.FREE;
            posx = posx + 1;
            pos += new Vector2(World.blockSize, 0);
            return true;
        }
        return false;
    }

    public bool MoveDown()
    {
        if (World.grid[posy + 1][posx] <= World.FREE_OR_FINISH)
        {
            if (World.grid[posy + 1][posx] == World.FINISH)
            {
                SceneManager.LoadScene(nextScene);
            }
            World.grid[posy + 1][posx] = World.MACHINE;
            World.grid[posy][posx] = World.FREE;
            posy = posy + 1;
            pos += new Vector2(0, -World.blockSize);
            return true;
        }
        return false;
    }

    public bool MoveUp()
    {
        if (World.grid[posy - 1][posx] <= World.FREE_OR_FINISH)
        {
            if (World.grid[posy - 1][posx] == World.FINISH)
            {
                SceneManager.LoadScene(nextScene);
            }
            World.grid[posy - 1][posx] = World.MACHINE;
            World.grid[posy][posx] = World.FREE;
            posy = posy - 1;
            pos += new Vector2(0, World.blockSize);
            return true;
        }
        return false;
    }

    public void doAction()
    {
        MoveLeft();
        player.resetTurnCounter();
    }
}
