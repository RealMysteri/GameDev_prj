using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
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

    public float Lastforcey = 0f;

    private Rigidbody2D rb;

    [SerializeField] public bool isgrounded;
    [SerializeField] public bool ismoving;
    private float xinput;       

    private float coyotecounter;
    private float jumpbuffercount;

    public static playermovement current;


    // Start is called before the first frame update
    void Awake()
    {
        current = this;
        rb = GetComponent<Rigidbody2D>();
        defaultgravity = rb.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        DirectionLook();
        JumpBuffer();
        CheckGround();
        CheckMove();
    }

    void FixedUpdate()
    {

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
        float sizechange = 0f;
        if (xinput == 0)
        {
            return;
        }

        if (Math.Sign(xinput) > 0)
        {
            sizechange = 0.5f;
        }
        else
        {
            sizechange = -0.5f;
        }
        transform.localScale = new Vector3((Math.Sign(xinput) - sizechange),0.5f,0.5f);
    }

    void CheckGround()
    {
        if(math.abs(rb.linearVelocity.y) < 0.1f)
        {
            if(Lastforcey < 0f && rb.linearVelocity.y > Lastforcey)
            {
                isgrounded = true;                 
            }

        }
        else
        {
            isgrounded = false;
        }
        Lastforcey = rb.linearVelocity.y;

    }

    void CheckMove()
    {
        ismoving =  !(Mathf.Approximately(rb.linearVelocity.x, 0f));
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


}

