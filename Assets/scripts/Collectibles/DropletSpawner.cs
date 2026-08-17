using UnityEngine;

public class DropletSpawner : MonoBehaviour
{
    [SerializeField] private GameObject dropletPrefab;
    [SerializeField] private float spawnInterval = 2.0f;
    [SerializeField] private float startDelay = 1.0f;

    void Start()
    {

        InvokeRepeating(nameof(SpawnDroplet), startDelay, spawnInterval);
    }

    void SpawnDroplet()
    {
        if (dropletPrefab != null)
        {
            
            Instantiate(dropletPrefab, transform.position, Quaternion.identity);
        }
    }
}
