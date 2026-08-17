using System;
using System.Collections;
using UnityEngine;

public class ImmobileEnemeyManager : MonoBehaviour
{
    [SerializeField] private int maxhealth = 50;
    [SerializeField] private int currenthealth;
    [SerializeField] private float attackcooldown = 4f;
    [SerializeField] private GameObject groundslam;
    [SerializeField] private float activeduration = 0.5f;

    public GameObject crystalPrefab;

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
        Debug.Log(damage);
        currenthealth -= damage;

        if (currenthealth <= 0)
        {
            StopAllCoroutines();
            if (groundslam != null)
            {
                groundslam.SetActive(false);
            }
            Instantiate(crystalPrefab, new Vector3(transform.position.x,transform.position.y -0.6f,transform.position.z), Quaternion.identity);
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerManager player = collision.GetComponentInParent<PlayerManager>();
            if (player != null)
            {
                Debug.Log("work");
                PlayerManager.current.TakeDamage(5,transform.position);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
