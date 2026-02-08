using UnityEngine;

public class RandomCollectableSpawner : MonoBehaviour
{
    [SerializeField] GameObject collectablePrefab;
    [SerializeField] int amount = 10;
    [SerializeField] private Vector2 minPos = new Vector2(-8f, -4f);
    [SerializeField] private Vector2 maxPos = new Vector2(8f, 4f);

    void Start()
    {
        for (int i = 0; i < amount; i++)
        {
            float x = Random.Range(minPos.x, maxPos.x);
            float y = Random.Range(minPos.y, maxPos.y);

            Vector3 spawnPos = new Vector3(x, y, 0f);
            // Instantiate(collectablePrefab, spawnPos, Quaternion.identity);
            // This version instantly spawns the prefab AND makes it a child of this object.
            Instantiate(collectablePrefab, spawnPos, Quaternion.identity, transform);
        }
    }
}
