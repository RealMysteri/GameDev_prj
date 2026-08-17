using UnityEngine;

public class GroundAttackHit : MonoBehaviour
{
    [SerializeField] private int damage = 20;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerManager player = collision.GetComponentInParent<PlayerManager>();

            if (player != null)
            {
                Debug.Log("work");
                PlayerManager.current.TakeDamage(damage,transform.position);
            }
        }
    }
}
