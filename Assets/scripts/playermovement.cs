 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playermovement : MonoBehaviour
{

    public float speed = 5;

    public float drag = 0.9f;

    private Rigidbody2D rb;

    public BoxCollider2D groundcheck;

    public LayerMask groundmask;

    public bool isgrounded;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");

        if (Mathf.Abs(x) > 0)
        {
            rb.linearVelocity = new Vector2(x * speed,rb.linearVelocity.y);
        }


        if (Input.GetKey(KeyCode.Space))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, speed);
        }

    
    }

    void FixedUpdate()
    {
        CheckGround();
        rb.linearVelocity *= drag;
    }

    void CheckGround()
    {
        Debug.Log(isgrounded);
        Debug.Log(Physics2D.OverlapAreaAll(groundcheck.bounds.min, groundcheck.bounds.max,groundmask).Length);
        isgrounded = Physics2D.OverlapAreaAll(groundcheck.bounds.min, groundcheck.bounds.max,groundmask).Length > 0;
        
    }
}

