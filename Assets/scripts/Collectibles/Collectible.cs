using UnityEngine;

public class Collectible : MonoBehaviour
{
    public enum CollectibleType { Star, PurpleFlourite, Howlite }
    public CollectibleType type;
    public int amount = 1;

    [SerializeField]private bool playerIsNear = false;

    void Update()
    {

        if (playerIsNear && Input.GetKeyDown(KeyCode.F))
        {
            gainstat();
        }
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

    public void gainstat()
    {
            if (type == CollectibleType.Star)
            {
                PlayerManager.current.maxhealth += amount;
                PlayerManager.current.currenthealth += amount; 
            }
            else if (type == CollectibleType.PurpleFlourite)
            {
                PlayerManager.current.PurpleFlourite += amount;
            }
            else if (type == CollectibleType.Howlite)
            {
                PlayerManager.current.howlite += amount;
            }

            Destroy(gameObject);         
    }
}
