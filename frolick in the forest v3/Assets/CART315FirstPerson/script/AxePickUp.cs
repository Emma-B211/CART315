
using UnityEngine;
using UnityEngine.InputSystem;

public class AxePickup : MonoBehaviour
{
    [Header("Pickup Settings")]
    public Camera playerCamera;
    public Transform axeHoldPoint;
    public float pickupRange = 3f;

    [Header("Visual Prompt")]
    public bool showPickupPrompt = true;

    private GameObject heldAxe;
    private bool isLookingAtAxe;

    // Allows other scripts to check if the player is holding the axe
    public bool HasAxe
    {
        get { return heldAxe != null; }
    }

    void Update()
    {
        CheckForAxe();

        if (Keyboard.current != null &&
            Keyboard.current.eKey.wasPressedThisFrame)
        {
            if (heldAxe == null)
            {
                TryPickupAxe();
            }
            else
            {
                DropAxe();
            }
        }
    }

    void CheckForAxe()
    {
        isLookingAtAxe = false;

        if (heldAxe != null || playerCamera == null)
            return;

        Ray ray = new Ray(
            playerCamera.transform.position,
            playerCamera.transform.forward
        );

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, pickupRange))
        {
            if (hit.collider.CompareTag("Pickup"))
            {
                isLookingAtAxe = true;
            }
        }
    }

    void TryPickupAxe()
    {
        if (playerCamera == null || axeHoldPoint == null)
            return;

        Ray ray = new Ray(
            playerCamera.transform.position,
            playerCamera.transform.forward
        );

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, pickupRange))
        {
            if (hit.collider.CompareTag("Pickup"))
            {
                heldAxe = hit.collider.gameObject;

                Rigidbody rb = heldAxe.GetComponent<Rigidbody>();

                if (rb != null)
                {
                    rb.isKinematic = true;
                    rb.detectCollisions = false;
                }

                Collider[] colliders =
                    heldAxe.GetComponentsInChildren<Collider>();

                foreach (Collider col in colliders)
                {
                    col.enabled = false;
                }

                heldAxe.transform.SetParent(axeHoldPoint);
                heldAxe.transform.localPosition = Vector3.zero;
                heldAxe.transform.localRotation = Quaternion.identity;

                Debug.Log("Axe picked up!");
            }
        }
    }

    void DropAxe()
    {
        heldAxe.transform.SetParent(null);

        heldAxe.transform.position =
            playerCamera.transform.position +
            playerCamera.transform.forward * 1.5f;

        Collider[] colliders =
            heldAxe.GetComponentsInChildren<Collider>();

        foreach (Collider col in colliders)
        {
            col.enabled = true;
        }

        Rigidbody rb = heldAxe.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.isKinematic = false;
            rb.detectCollisions = true;
        }

        heldAxe = null;

        Debug.Log("Axe dropped!");
    }

    void OnGUI()
    {
        if (!showPickupPrompt || !isLookingAtAxe)
            return;

        GUIStyle promptStyle = new GUIStyle();
        promptStyle.fontSize = 22;
        promptStyle.normal.textColor = Color.white;
        promptStyle.alignment = TextAnchor.MiddleCenter;
        promptStyle.fontStyle = FontStyle.Bold;

        GUI.Label(
            new Rect(
                Screen.width / 2 - 150,
                Screen.height / 2 + 40,
                300,
                40
            ),
            "Press E to Pick Up Axe",
            promptStyle
        );
    }
}
