using System.Collections;
using UnityEngine;

public class FluoriteEnemyManager : MonoBehaviour
{

    [SerializeField] private int maxhealth = 15;
    [SerializeField] private int currenthealth;
    //[SerializeField] private float attackcooldown = 4f;
    [SerializeField] private bool attack;
    [SerializeField] private bool playerfound = true;
    [SerializeField] private float movespeed = 3f;
    [SerializeField] private Transform playertransform;

    [SerializeField] private float dashForce = 15f;
    [SerializeField] private float chargeDuration = 1.5f;
    [SerializeField] private float dashDuration = 0.3f;
    [SerializeField] private float postDashPause = 0.1f;
    [SerializeField] private float dashCooldown = 4f;    
    public GameObject crystalPrefab;
    private bool ismoving = true;
    private bool candash = true;

    private Rigidbody2D rb;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currenthealth = maxhealth;
        rb = GetComponent<Rigidbody2D>();


    }

    // Update is called once per frame
    void Update()
    {
        if (playerfound && candash)
        {
            StartCoroutine(Dash());
        }

    }

    void FixedUpdate()
    {
        if (ismoving && playerfound && playertransform != null)
        {
            follow();
        }
    }

        public void Takedamage(int damage)
    {
        Debug.Log("works");
        currenthealth -= damage;

        if (currenthealth <= 0)
        {
            StopAllCoroutines();

            Instantiate(crystalPrefab, new Vector3(transform.position.x,transform.position.y,transform.position.z), Quaternion.identity);
            Destroy(gameObject);
        }
    }

    IEnumerator Dash()
    {
        candash = false;
        ismoving = false;

        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        yield return new WaitForSeconds(chargeDuration);

        float dashdirectionX = 1f;

        if (playertransform != null)
        {
            dashdirectionX  = Mathf.Sign(playertransform.position.x - transform.position.x);
        }

        rb.linearVelocity = new Vector2(dashdirectionX * dashForce, rb.linearVelocity.y);
        yield return new WaitForSeconds(dashDuration);

        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        yield return new WaitForSeconds(postDashPause);

        ismoving = true;
        yield return new WaitForSeconds(dashCooldown);

        candash = true;


    }

    void follow()
    {
        if (ismoving && playerfound && playertransform != null)
        {
            float directionX = Mathf.Sign(playertransform.position.x - transform.position.x);
            rb.linearVelocity = new Vector2(directionX * movespeed, rb.linearVelocity.y);
            
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerfound = true;
            playertransform = collision.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerfound = false;
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y); 
        }
    }

}
