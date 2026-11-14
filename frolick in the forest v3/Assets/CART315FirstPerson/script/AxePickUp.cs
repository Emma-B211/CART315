using UnityEngine;

public class AxePickUp : MonoBehaviour
{

    [Header("Pickup Setting")]
    public Camera playerCamera;
    public Transform axeHoldPoint;
    public float PickupRange;
    public KeyCode pickupKey= KeyCode.E;

    private gameObject heldAxe;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
