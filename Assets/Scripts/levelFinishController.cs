using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelFinishController : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("mech"))
        {
            Debug.Log("Level complete");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
