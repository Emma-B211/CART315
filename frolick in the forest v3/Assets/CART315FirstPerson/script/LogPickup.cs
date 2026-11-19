
using UnityEngine;
using UnityEngine.InputSystem;

public class LogPickup : MonoBehaviour
{
    public float pickupRange = 3f;

    private Camera playerCamera;
    private WoodInventory inventory;

    void Start()
    {
        playerCamera = Camera.main;
        inventory = FindFirstObjectByType<WoodInventory>();
    }

    void Update()
    {
        if (Keyboard.current == null)
            return;

        if (Keyboard.current.fKey.wasPressedThisFrame)
        {
            CollectLog();
        }
    }

    void CollectLog()
    {
        if (playerCamera == null || inventory == null)
            return;

        Ray ray = new Ray(
            playerCamera.transform.position,
            playerCamera.transform.forward
        );

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, pickupRange))
        {
            LogPickup log = hit.collider.GetComponentInParent<LogPickup>();

            if (log != this)
                return;

            inventory.AddWood(1);
            Destroy(gameObject);

            Debug.Log("Collected one log!");
        }
    }
}
