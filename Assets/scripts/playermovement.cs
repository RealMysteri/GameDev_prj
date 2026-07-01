using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playermovement : MonoBehaviour
{

    public float speed = 5;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");

        rb.linearVelocity = new Vector2(x * speed,rb.linearVelocity.y);

        if (Input.GetKey(KeyCode.Space))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, speed);
        }
    
    }
}
