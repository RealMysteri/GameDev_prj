using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private int maxhealth = 100;
    [SerializeField] private int currenthealth;

    private Vector2 spawnpoint;
    private bool isdead = false;
    private Rigidbody2D rb;
    private playermovement movementscript;

    [SerializeField] private int damage = 20;
    [SerializeField] private float respawntimer = 3f;
    
    [SerializeField] private ImmobileEnemeyManager enemy;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currenthealth = maxhealth;
        spawnpoint = transform.position;
        rb = GetComponent<Rigidbody2D>();
        movementscript = GetComponent<playermovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            if (enemy != null)
            {
                enemy.Takedamage(damage);
            }
        }

        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            Heal(10);
        }

        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            TakeDamage(15); 
        }
    }

    public void TakeDamage(int damage)
    {
        if (isdead)
        {
            return;
        }

        currenthealth -= damage;

        if (currenthealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isdead = true;

        if (movementscript != null)
        {
            movementscript.enabled = false;
            rb.linearVelocity = Vector2.zero;
            rb.simulated = false;

            StartCoroutine(Respawn());
        }
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(respawntimer);

        transform.position = spawnpoint;
        currenthealth = maxhealth;
        isdead = false;
        rb.simulated = true;

        if (movementscript != null)
        {
            movementscript.enabled = true;
        }
    }

    private void Heal(int amount)
    {
        if (isdead)
        {
            return;
        }

        currenthealth = Mathf.Min(currenthealth + amount,maxhealth);
    }
}
