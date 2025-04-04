using UnityEngine;

public class InkDropSpawner : MonoBehaviour
{
    public GameObject inkDropPrefab;
    public float spawnInterval = 2f;
    public int dropsPerInterval = 30;
    public Vector3 areaSize = new Vector3(10, 0, 8);

    void Start()
    {
        InvokeRepeating(nameof(SpawnInkDrops), 0f, spawnInterval);
    }

    void SpawnInkDrops()
    {
        for (int i = 0; i < dropsPerInterval; i++)
        {
            Vector3 randomPos = new Vector3(
                Random.Range(-areaSize.x / 2, areaSize.x / 2),
                0,
                Random.Range(-areaSize.z / 2, areaSize.z / 2)
            );

            Vector3 spawnPosition = transform.position + randomPos;
            Instantiate(inkDropPrefab, spawnPosition, Quaternion.identity);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, areaSize);
    }
}
