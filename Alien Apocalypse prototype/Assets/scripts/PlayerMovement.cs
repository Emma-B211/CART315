
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float jumpForce = 11f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private float horizontalInput;
    private bool isGrounded;
    private int facingDirection = 1;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        // Prevent the character from tipping over.
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        // Stop unwanted movement when the game begins.
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
    }

    void Update()
    {
        // Move only while A or D is held.
        horizontalInput = 0f;

        if (Input.GetKey(KeyCode.A))
            horizontalInput = -1f;

        if (Input.GetKey(KeyCode.D))
            horizontalInput = 1f;

        // Check whether the player is touching the ground.
        if (groundCheck != null)
        {
            isGrounded = Physics2D.OverlapCircle(
                groundCheck.position,
                groundCheckRadius,
                groundLayer
            );
        }

        // Jump only when Space is pressed.
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(
                rb.linearVelocity.x,
                jumpForce
            );
        }

        // Face the direction of movement.
        if (horizontalInput != 0f)
        {
            facingDirection = horizontalInput > 0f ? 1 : -1;

            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x) * facingDirection;
            transform.localScale = scale;
        }
    }

    void FixedUpdate()
    {
        // Set horizontal speed from current keyboard input.
        rb.linearVelocity = new Vector2(
            horizontalInput * moveSpeed,
            rb.linearVelocity.y
        );
    }
}
