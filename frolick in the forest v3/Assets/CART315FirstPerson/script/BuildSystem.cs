
using UnityEngine;
using UnityEngine.InputSystem;

public class BuildSystem : MonoBehaviour
{
    [Header("References")]
    public Camera playerCamera;
    public WoodInventory inventory;

    [Header("Building Prefabs")]
    public GameObject benchPrefab;
    public GameObject campfirePrefab;

    [Header("Building Costs")]
    public int benchCost = 4;
    public int campfireCost = 5;

    [Header("Placement")]
    public float buildRange = 5f;
    public LayerMask groundLayer;
    public float placementOffset = 0.02f;

    private GameObject selectedPrefab;
    private int selectedCost;
    private string selectedName = "";

    void Update()
    {
        if (Keyboard.current == null || Mouse.current == null)
            return;

        if (Keyboard.current.digit1Key.wasPressedThisFrame)
            SelectBuilding(benchPrefab, benchCost, "Bench");

        if (Keyboard.current.digit2Key.wasPressedThisFrame)
            SelectBuilding(campfirePrefab, campfireCost, "Campfire");

        if (Keyboard.current.escapeKey.wasPressedThisFrame)
            CancelBuilding();

        if (selectedPrefab != null &&
            Mouse.current.leftButton.wasPressedThisFrame)
        {
            PlaceBuilding();
        }
    }

    void SelectBuilding(GameObject prefab, int cost, string buildingName)
    {
        if (prefab == null)
        {
            Debug.LogWarning(buildingName + " prefab is missing!");
            return;
        }

        selectedPrefab = prefab;
        selectedCost = cost;
        selectedName = buildingName;

        Debug.Log("Selected: " + buildingName);
    }

    void PlaceBuilding()
    {
        if (playerCamera == null || inventory == null)
        {
            Debug.LogWarning("Player Camera or Inventory is missing!");
            return;
        }

        Ray ray = new Ray(
            playerCamera.transform.position,
            playerCamera.transform.forward
        );

        if (Physics.Raycast(ray, out RaycastHit hit, buildRange, groundLayer))
        {
            if (inventory.CurrentWood < selectedCost)
            {
                Debug.Log("Not enough wood!");
                return;
            }

            Vector3 placementPosition =
                hit.point + Vector3.up * placementOffset;

           GameObject builtObject;

if (selectedPrefab == campfirePrefab)
{
    // Correct campfire rotation.
    Quaternion campfireRotation = Quaternion.Euler(
        -39.454f,
        -1.084f,
        1.002f
    );

    builtObject = Instantiate(
        selectedPrefab,
        placementPosition,
        campfireRotation
    );
}
else
{
    // Preserve the bench's original rotation.
    builtObject = Instantiate(
        selectedPrefab,
        placementPosition,
        selectedPrefab.transform.rotation
    );
}

            if (!inventory.UseWood(selectedCost))
            {
                Destroy(builtObject);
                return;
            }

            Debug.Log(selectedName + " placed!");

            CancelBuilding();
        }
        else
        {
            Debug.Log("No ground detected. Aim at the ground.");
        }
    }

    void CancelBuilding()
    {
        selectedPrefab = null;
        selectedName = "";
        selectedCost = 0;
    }

    void OnGUI()
    {
        if (selectedPrefab == null)
            return;

        GUI.Label(
            new Rect(20, 100, 300, 40),
            "Building: " + selectedName +
            " | Cost: " + selectedCost + " wood"
        );

        GUI.Label(
            new Rect(20, 130, 350, 40),
            "Left Click: Place | Escape: Cancel"
        );
    }
}
