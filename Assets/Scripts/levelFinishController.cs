using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelFinishController : MonoBehaviour
{
    [SerializeField]
    private Transform transform;

    int posx;
    int posy;

    // Update is called once per frame
    void Update()
    {
        posx = (int)transform.position.x/World.blockSize;
        posy = -1*(int)transform.position.y/World.blockSize;
        World.grid[posy][posx] = 1;
    }
}
