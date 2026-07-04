using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class playermovement : MonoBehaviour
{

    [SerializeField] private float movespeed = 5f;
    [SerializeField] private float acceleration = 35f;
    [SerializeField] private float deceleration = 35f;
    [SerializeField] private float airacceleration = 5f;

    [SerializeField] private float jumpspeed = 7f;
    [SerializeField] private float coyotejump = 0.15f;
    [SerializeField] private float jumpbuffertime = 0.15f;
    [SerializeField] private float jumpmultiplier = 0.5f;

    [SerializeField] private float friction = 0.9f;
    [SerializeField] public BoxCollider2D groundcheck;
    [SerializeField] public LayerMask groundmask;

    private Rigidbody2D rb;

    private bool jumppressed;
    private bool isgrounded;
    private float xinput;

    private float coyotecounter;
    private float jumpbuffercount;


    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        DirectionLook();
    }

    void FixedUpdate()
    {
        CheckGround();
        Move();
        jump();
        ApplyDrag();
    }

    void GetInput()
    {
        xinput = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumppressed = true;
        }
    }

    void Move()
    {
        rb.linearVelocity = new Vector2(xinput * movespeed, rb.linearVelocity.y);
    }

    void jump()
    {
        if (jumppressed && isgrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpspeed);
        }

        jumppressed = false;
    }

    void DirectionLook()
    {
        if (xinput == 0)
        {
            return;
        }

        transform.localScale = new Vector3(Math.Sign(xinput),1,1);
    }

    void ApplyDrag()
    {
        if (isgrounded && xinput == 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x * friction, rb.linearVelocity.y);
        }
    }

    void CheckGround()
    {
        isgrounded = Physics2D.OverlapArea(groundcheck.bounds.min, groundcheck.bounds.max,groundmask);
    }
}

