
using UnityEngine;
using UnityEngine.InputSystem;

public class LogPickup : MonoBehaviour
{
    public float pickupRange = 3f;
    public Camera playerCamera;
    public int woodAmount = 1;

    private WoodInventory inventory;

    void Start()
    {
        if (playerCamera == null)
            playerCamera = Camera.main;

        inventory = FindFirstObjectByType<WoodInventory>();
    }

    void Update()
    {
        if (playerCamera == null || inventory == null)
            return;

        Ray ray = new Ray(
            playerCamera.transform.position,
            playerCamera.transform.forward
        );

        if (Physics.Raycast(ray, out RaycastHit hit, pickupRange))
        {
            LogPickup log = hit.collider.GetComponentInParent<LogPickup>();

            if (log == this && Keyboard.current != null &&
                Keyboard.current.fKey.wasPressedThisFrame)
            {
                CollectLog();
            }
        }
    }

    void CollectLog()
    {
        inventory.AddWood(woodAmount);
        Debug.Log("Collected " + woodAmount + " wood!");

        Destroy(gameObject);
    }
}
