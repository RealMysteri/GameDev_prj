using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playermovement : MonoBehaviour
{

    [SerializeField] private float movespeed = 5f;
    [SerializeField] private float acceleration = 35f;
    [SerializeField] private float deceleration = 35f;
    [SerializeField] private float airacceleration = 5f;

    [SerializeField] private float jumpspeed = 7f;
    [SerializeField] private float coyotetime = 0.15f;
    [SerializeField] private float jumpbuffertime = 0.15f;
    [SerializeField] private float jumpmultiplier = 0.5f;

    [SerializeField] private float GravityMulti;

    [SerializeField] private float maxfallspeed ;
    [SerializeField] private float defaultgravity;
    [SerializeField] public PlayerManager player;

    private Rigidbody2D rb;

    [SerializeField] private bool isgrounded;
    private bool ismoving;
    private float xinput;       

    private float coyotecounter;
    private float jumpbuffercount;


    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        defaultgravity = rb.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        DirectionLook();
        JumpBuffer();

    }

    void FixedUpdate()
    {
        CheckGround();
        CheckMove();
        UdpateCoyoteTime();
        Move();
        Jump();
        Gravity();
    }

    void GetInput()
    {
        xinput = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpbuffercount = jumpbuffertime;
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            if (ismoving && isgrounded)
            {
                RegenAP();
            }
        
        }
    }

    void Move()
    {
        float targetspeed = xinput * movespeed;

        float accelrate;

        if (isgrounded)
        {
            accelrate = (MathF.Abs(targetspeed) > 0.01f) ? acceleration : deceleration;
        }
        else
        {
            accelrate = airacceleration;
        }

        float newSpeed = Mathf.MoveTowards(rb.linearVelocity.x, targetspeed, accelrate * Time.deltaTime);
        rb.linearVelocity = new Vector2(newSpeed, rb.linearVelocity.y);
    }

    void Jump()
    {
        if (jumpbuffercount > 0 && coyotecounter > 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpspeed);

            jumpbuffercount = 0;
            coyotecounter = 0;
        }

        if (Input.GetKeyUp(KeyCode.Space) && rb.linearVelocity.y > 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * jumpmultiplier);
        }
    }

    void JumpBuffer()
    {
        if (jumpbuffercount > 0)
        {
            jumpbuffercount -= Time.deltaTime;
        }
    }

    void UdpateCoyoteTime()
    {
        if (isgrounded)
        {
            coyotecounter = coyotetime;
        }
        else
        {
            coyotecounter -= Time.fixedDeltaTime;
        }
    }

    void DirectionLook()
    {
        if (xinput == 0)
        {
            return;
        }

        transform.localScale = new Vector3(Math.Sign(xinput),1,1);
    }

    void CheckGround()
    {
        isgrounded =  Mathf.Approximately(rb.linearVelocity.y, 0f);
    }

    void CheckMove()
    {
        ismoving =  Mathf.Approximately(rb.linearVelocity.x, 0f);
    }

    void Gravity()
    {
        if(rb.linearVelocity.y < -0.01f)
        {
            rb.gravityScale = defaultgravity * GravityMulti;

            float clampedY = MathF.Max(rb.linearVelocity.y, -maxfallspeed);
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, clampedY);
        }
        else
        {
            rb.gravityScale = defaultgravity;
        }
    }

    void RegenAP()
    {
        
        player.GainAP(10);
    }
}

