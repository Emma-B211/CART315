
using UnityEngine;

public class TreeHealth : MonoBehaviour
{
    [Header("Tree Settings")]
    public int treeHealth = 5;

    [Header("Log Drops")]
    public GameObject logPrefab;
    public int logsToDrop = 3;
    public float dropRadius = 1.5f;

    public void TakeDamage(int damage)
    {
        treeHealth -= damage;

        Debug.Log("Tree health: " + treeHealth);

        if (treeHealth <= 0)
        {
            DropLogs();
            Destroy(gameObject);
        }
    }

    void DropLogs()
    {
        if (logPrefab == null)
        {
            Debug.LogWarning("Log Prefab is missing!");
            return;
        }

        for (int i = 0; i < logsToDrop; i++)
        {
            Vector2 randomPosition = Random.insideUnitCircle * dropRadius;

            Vector3 spawnPosition = transform.position +
                new Vector3(randomPosition.x, 0.5f, randomPosition.y);

            Instantiate(logPrefab, spawnPosition, Random.rotation);
        }
    }
}
