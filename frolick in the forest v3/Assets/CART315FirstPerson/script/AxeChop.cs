
using UnityEngine;
using UnityEngine.InputSystem;

public class AxeChop : MonoBehaviour
{
    [Header("Chopping Settings")]
    public Camera playerCamera;
    public float chopRange = 3f;
    public float chopCooldown = 0.8f;
    public int chopDamage = 1;

    [Header("Prompt Settings")]
    public bool showChopPrompt = true;

    private AxePickup axePickup;
    private TreeHealth currentTree;
    private float nextChopTime;

    void Start()
    {
        axePickup = GetComponent<AxePickup>();

        if (playerCamera == null)
            playerCamera = Camera.main;
    }

    void Update()
    {
        CheckForTree();

        if (Mouse.current != null &&
            Mouse.current.leftButton.wasPressedThisFrame)
        {
            Chop();
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

        RaycastHit hit;

       if (Physics.Raycast(ray, out hit, chopRange))
{
    currentTree = hit.collider.GetComponentInParent<TreeHealth>();
}
    }

    void Chop()
    {
        if (axePickup == null || !axePickup.HasAxe)
        {
            Debug.Log("You need to pick up the axe first!");
            return;
        }

        if (currentTree == null)
            return;

        if (Time.time < nextChopTime)
            return;

        nextChopTime = Time.time + chopCooldown;

        currentTree.TakeDamage(chopDamage);

        Debug.Log("Chopped tree!");
    }

    void OnGUI()
    {
        if (!showChopPrompt || currentTree == null)
            return;

        GUIStyle style = new GUIStyle();
        style.fontSize = 22;
        style.normal.textColor = Color.white;
        style.alignment = TextAnchor.MiddleCenter;
        style.fontStyle = FontStyle.Bold;

        string prompt;

        if (axePickup != null && axePickup.HasAxe)
        {
            prompt = "Left Click to Chop Tree";
        }
        else
        {
            prompt = "Pick Up an Axe to Chop";
        }

        GUI.Label(
            new Rect(
                Screen.width / 2 - 180,
                Screen.height / 2 + 40,
                360,
                40
            ),
            prompt,
            style
        );
    }
}
