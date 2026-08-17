using UnityEngine;

public class DropletDelete : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // If it hits the Environment/Ground layer, vanish
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
