
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
        {
            SelectBuilding(benchPrefab, benchCost, "Bench");
        }

        if (Keyboard.current.digit2Key.wasPressedThisFrame)
        {
            SelectBuilding(campfirePrefab, campfireCost, "Campfire");
        }

        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            CancelBuilding();
        }

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
            return;

        Ray ray = new Ray(
            playerCamera.transform.position,
            playerCamera.transform.forward
        );

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, buildRange, groundLayer))
        {
            if (!inventory.UseWood(selectedCost))
            {
                Debug.Log("Not enough wood!");
                return;
            }

            // Align the building with the ground surface
           Quaternion rotation = Quaternion.FromToRotation(
            Vector3.up,
            hit.normal
);

            // Slightly lift the building above the ground
            Vector3 placementPosition = hit.point +
                hit.normal * placementOffset;

            // Place the building
            Instantiate(
                selectedPrefab,
                placementPosition,
                rotation
            );

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
