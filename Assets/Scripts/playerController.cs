using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;

    bool moveLeft = false;
    bool moveRight = false;
    bool moveUp = false;
    bool moveDown = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        moveLeft = Input.GetAxis("Horizontal") < 0;
        moveRight = Input.GetAxis("Horizontal") > 0;
        moveUp = Input.GetAxis("Vertical") > 0;
        moveDown = Input.GetAxis("Vertical") < 0;
    }

    private void FixedUpdate()
    {
        if (moveLeft)
        {
            rb.AddForce(new Vector2(-5, 0), ForceMode2D.Impulse);
        }
        if (moveRight)
        {
            rb.AddForce(new Vector2(5, 0), ForceMode2D.Impulse);
        }
        if (moveUp)
        {
            rb.AddForce(new Vector2(0, 5), ForceMode2D.Impulse);
        }
        if (moveDown)
        {
            rb.AddForce(new Vector2(0, -5), ForceMode2D.Impulse);
        }
    }
}
