
using UnityEngine;

public class EnemyAppearManager : MonoBehaviour
{
    public Transform player;
    public GameObject enemy;
    public float movementThreshold = 0.05f;

    private Vector3 lastPlayerPosition;
    private bool enemyAppeared = false;

    void Start()
    {
        if (player == null)
        {
            GameObject playerObject =
                GameObject.FindGameObjectWithTag("Player");

            if (playerObject != null)
                player = playerObject.transform;
        }

        if (player != null)
            lastPlayerPosition = player.position;
        else
            Debug.LogError("Assign the Player and tag it Player.");

        if (enemy != null)
            enemy.SetActive(false);
        else
            Debug.LogError("Assign the InfectedEnemy in the Inspector.");
    }

    void Update()
    {
        if (enemyAppeared || player == null || enemy == null)
            return;

        float distanceMoved = Vector3.Distance(
            player.position,
            lastPlayerPosition
        );

        if (distanceMoved > movementThreshold)
        {
            enemyAppeared = true;
            enemy.SetActive(true);

            Debug.Log("The infected enemy has appeared!");
        }

        lastPlayerPosition = player.position;
    }
}
