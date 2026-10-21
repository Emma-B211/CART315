
using UnityEngine;

public class EnemyAppearOnMove : MonoBehaviour
{
    public Transform player;
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
            {
                player = playerObject.transform;
            }
        }

        if (player != null)
        {
            lastPlayerPosition = player.position;
        }
    }

    void Update()
    {
        if (enemyAppeared || player == null)
        {
            return;
        }

        float distanceMoved = Vector3.Distance(
            player.position,
            lastPlayerPosition
        );

        if (distanceMoved > movementThreshold)
        {
            enemyAppeared = true;

            Debug.Log("Player moved!");
        }

        lastPlayerPosition = player.position;
    }
}
