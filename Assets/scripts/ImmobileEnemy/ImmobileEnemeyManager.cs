using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ImmobileEnemeyManager : MonoBehaviour
{
    [SerializeField] private int maxhealth = 50;
    [SerializeField] private int currenthealth;
    [SerializeField] private float attackcooldown = 4f;
    [SerializeField] private GameObject groundslam;
    [SerializeField] private float activeduration = 0.5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currenthealth = maxhealth;

        if (groundslam != null)
        {
            groundslam.SetActive(false);
        }
        StartCoroutine(Attack());
    }

    private IEnumerator Attack()
    {
        while (currenthealth > 0)
        {
            yield return new WaitForSeconds(attackcooldown);
            StartCoroutine(DoAttack());
        }

    }

    private IEnumerator DoAttack()
    {
        if (groundslam != null)
        {
            groundslam.SetActive(true);
            yield return new WaitForSeconds(activeduration);
            groundslam.SetActive(false);
        }
    }

    public void Takedamage(int damage)
    {
        Debug.Log("works");
        currenthealth -= damage;

        if (currenthealth <= 0)
        {
            StopAllCoroutines();
            if (groundslam != null)
            {
                groundslam.SetActive(false);
            }
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
