using UnityEngine;

public class CrystalRock : MonoBehaviour
{
    public GameObject collectiblePrefab; 
    [SerializeField]private bool playerIsNear = false;

    void Update()
    {

        if (playerIsNear && Input.GetKeyDown(KeyCode.F))
        {
            BreakObject();
        }
    }

    private void BreakObject()
    {
        if (collectiblePrefab != null)
        {
            Instantiate(collectiblePrefab, transform.position, Quaternion.identity);
        }

        // Play particle effect or screen shake here if you want later!
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
           playerIsNear = true; 
        } 
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            playerIsNear = false;
        }
    }
}
