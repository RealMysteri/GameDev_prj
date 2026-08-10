using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using CardMaker;
using NUnit.Framework.Internal;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private int maxhealth = 100;
    [SerializeField] private int currenthealth;

    [SerializeField] private int ActionPoint;
    [SerializeField] private int MaxActionPoint;

    private Vector2 spawnpoint;
    private bool isdead = false;
    private Rigidbody2D rb;
    private playermovement movementscript;

    [SerializeField] private int damage = 20;
    [SerializeField] private float respawntimer = 3f;
    
    [SerializeField] private ImmobileEnemeyManager enemy;

    [SerializeField] private PlayerUi uiManager;

    [SerializeField] private CardLoader cardLoader; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currenthealth = maxhealth;
        spawnpoint = transform.position;
        rb = GetComponent<Rigidbody2D>();
        movementscript = GetComponent<playermovement>();
        ActionPoint = MaxActionPoint;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            int ActionPointcost = 1;
            if(checkAP(ActionPointcost))
            {
                if (enemy != null)
                {
                    enemy.Takedamage(damage);
                    ActionPoint -= ActionPointcost;
                }
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

        UpdatePlayerUI();
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
            currenthealth = 0;
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

    public void Heal(int amount)
    {
        if (isdead)
        {
            return;
        }

        currenthealth = Mathf.Min(currenthealth + amount,maxhealth);
    }

    private bool checkAP (int amt)
    {
        return (ActionPoint - amt) >= 0;
    }

    public void UseAP(int amount)
    {
        ActionPoint -= amount;
    }

    public int CurrentAP
    {
        get {return ActionPoint;}
    }

    public void GainAP(int amount)
    {
        ActionPoint = Mathf.Min(ActionPoint + amount,MaxActionPoint);
    }

    private void UpdatePlayerUI()
{
    if (uiManager != null)
    {
        uiManager.UpdateDisplay(currenthealth, maxhealth, ActionPoint, MaxActionPoint);
    }
}
}
