using System.Collections;
using UnityEngine;

public class FluoriteEnemyManager : MonoBehaviour
{

    [SerializeField] private int maxhealth = 50;
    [SerializeField] private int currenthealth;
    //[SerializeField] private float attackcooldown = 4f;
    [SerializeField] private bool attack;
    [SerializeField] private bool move = true;
    [SerializeField] private bool playerfound = true;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currenthealth = maxhealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {   
            Debug.Log("worked");
            StartCoroutine(ChargeAttack());
        }

        if (move == true && playerfound == true)
        {
            Follow();
        }

    }

    IEnumerator ChargeAttack()
    {
        move = false;
        Debug.Log("test");
        yield return new WaitForSeconds(2f);
        Debug.Log("attack done");
        move = true;
    }

    void Follow()
    {
        Debug.Log("Following");
    }


}
