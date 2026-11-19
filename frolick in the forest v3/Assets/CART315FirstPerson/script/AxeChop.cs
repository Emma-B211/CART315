
using UnityEngine;
using UnityEngine.InputSystem;

public class AxeChop : MonoBehaviour
{
    public Camera playerCamera;
    public AxePickup axePickup;

    public float chopRange = 4f;
    public int chopDamage = 1;

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
            currentTree.TakeDamage(chopDamage);
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
