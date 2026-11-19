
using UnityEngine;
using UnityEngine.InputSystem;

public class AxeChop : MonoBehaviour
{
    [Header("References")]
    public Camera playerCamera;
    public AxePickup axePickup;

    [Header("Chopping Settings")]
    public float chopRange = 4f;
    public int chopDamage = 1;

    [Header("Chopping Audio")]
    public AudioSource audioSource;
    public AudioClip chopSound;
    [Range(0f, 1f)]
    public float chopVolume = 1f;

    private TreeHealth currentTree;

    void Update()
    {
        CheckForTree();

        if (currentTree != null &&
            axePickup != null &&
            axePickup.HasAxe &&
            Mouse.current != null &&
            Mouse.current.leftButton.wasPressedThisFrame)
        {
            ChopTree();
        }
    }

    void ChopTree()
    {
        // Apply damage to the tree
        currentTree.TakeDamage(chopDamage);

        // Play chopping sound
        if (audioSource != null && chopSound != null)
        {
            audioSource.PlayOneShot(chopSound, chopVolume);
        }
    }

    void CheckForTree()
    {
        currentTree = null;

        if (playerCamera == null)
            return;

        Ray ray = new Ray(
            playerCamera.transform.position,
            playerCamera.transform.forward
        );

        if (Physics.Raycast(ray, out RaycastHit hit, chopRange))
        {
            currentTree = hit.collider.GetComponentInParent<TreeHealth>();
        }
    }

    void OnGUI()
    {
        if (currentTree == null)
            return;

        GUIStyle style = new GUIStyle();
        style.fontSize = 20;
        style.fontStyle = FontStyle.Bold;
        style.normal.textColor = Color.white;
        style.alignment = TextAnchor.MiddleCenter;

        string message = "Pick Up an Axe to Chop";

        if (axePickup != null && axePickup.HasAxe)
            message = "Left Click to Chop Tree";

        GUI.Label(
            new Rect(Screen.width / 2 - 150, Screen.height / 2 + 40, 300, 40),
            message,
            style
        );
    }
}
