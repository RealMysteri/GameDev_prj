using UnityEngine;

public class GreenFlouriteDamage : MonoBehaviour
{
    public int damage = 15;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerManager player = collision.GetComponentInParent<PlayerManager>();
            if (player != null)
            {
                PlayerManager.current.TakeDamage(damage,transform.position);
            }
        }
    }
}
