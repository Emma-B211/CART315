
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float jumpForce = 11f;

    [Header("Ground Detection")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private float horizontalInput;
    private bool isGrounded;
    private int facingDirection = 1;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // A moves left; D moves right.
        horizontalInput = 0f;

        if (Input.GetKey(KeyCode.A))
        {
            horizontalInput = -1f;
        }

        if (Input.GetKey(KeyCode.D))
        {
            horizontalInput = 1f;
        }

        // Check whether the player is touching the ground.
        if (groundCheck != null)
        {
            isGrounded = Physics2D.OverlapCircle(
                groundCheck.position,
                groundCheckRadius,
                groundLayer
            );
        }
        else
        {
            isGrounded = false;
        }

        // Jump using Space.
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(
                rb.linearVelocity.x,
                jumpForce
            );
        }

        // Face the direction of movement.
        if (horizontalInput > 0)
        {
            facingDirection = 1;
        }
        else if (horizontalInput < 0)
        {
            facingDirection = -1;
        }

        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * facingDirection;
        transform.localScale = scale;
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(
            horizontalInput * moveSpeed,
            rb.linearVelocity.y
        );
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null)
            return;

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(
            groundCheck.position,
            groundCheckRadius
        );
    }
}
