
using UnityEngine;
using UnityEngine.InputSystem;

public class FindTreeCollider : MonoBehaviour
{
    public float detectionRadius = 5f;

    void Update()
    {
        if (Keyboard.current != null &&
            Keyboard.current.fKey.wasPressedThisFrame)
        {
            CheckColliders();
        }
    }

    void CheckColliders()
    {
        Collider[] colliders =
            Physics.OverlapSphere(
                transform.position,
                detectionRadius,
                ~0,
                QueryTriggerInteraction.Collide
            );

        Debug.Log("Colliders detected: " + colliders.Length);

        foreach (Collider col in colliders)
        {
            Debug.Log(
                "Object: " + col.gameObject.name +
                " | Enabled: " + col.enabled +
                " | Active: " + col.gameObject.activeInHierarchy +
                " | Is Trigger: " + col.isTrigger
            );
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
