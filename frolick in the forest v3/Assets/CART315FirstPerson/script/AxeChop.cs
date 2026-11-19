
using UnityEngine;
using UnityEngine.InputSystem;

public class AxeChop : MonoBehaviour
{
    public Camera playerCamera;
    public float chopRange = 3f;
    public float chopCooldown = 0.8f;
    public int chopDamage = 1;

    private AxePickup axePickup;
    private float nextChopTime;

    void Start()
    {
        axePickup = GetComponent<AxePickup>();

        if (playerCamera == null)
        {
            playerCamera = Camera.main;
        }
    }

    void Update()
    {
        if (Mouse.current == null)
            return;

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Chop();
        }
    }

    void Chop()
    {
        // Make sure the player is holding the axe
        if (axePickup == null || !axePickup.HasAxe)
            return;

        if (Time.time < nextChopTime)
            return;

        nextChopTime = Time.time + chopCooldown;

        Ray ray = new Ray(
            playerCamera.transform.position,
            playerCamera.transform.forward
        );

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, chopRange))
        {
            TreeHealth tree = hit.collider.GetComponentInParent<TreeHealth>();

            if (tree != null)
            {
                tree.TakeDamage(chopDamage);
                Debug.Log("Chopped tree!");
            }
        }
    }
}
