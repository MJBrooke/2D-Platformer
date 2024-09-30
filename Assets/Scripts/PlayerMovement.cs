using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private static readonly int IsRunningParam = Animator.StringToHash("isRunning");
    private static readonly int IsClimbingParam = Animator.StringToHash("isClimbing");

    private const float DefaultGravity = 7f;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator myAnimator;
    [SerializeField] private CapsuleCollider2D myCollider;

    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float jumpSpeed = 18f;
    [SerializeField] private float climbSpeed = 5f;

    private Vector2 _moveInput;
    private int groundLayerMask;

    private void Start()
    {
        groundLayerMask = LayerMask.GetMask("Ground");
    }

    private void Update()
    {
        Run();
        FlipSprite();
        ClimbLadder();
    }

    // This is invoked each time we input movement via the Input System.
    // We get a Vector2 type with the x and y value, between 0 and 1 inclusive.
    private void OnMove(InputValue value)
    {
        // We store this value into a field so that it can be used by other functions without passing this around as a parameter.
        _moveInput = value.Get<Vector2>();
    }

    private void Run()
    {
        // We set the x-value to our movement speed. If input is non-zero, this creates horizontal movement.
        // We set the y-value to the existing value of the RigidBody so that we don't override gravity.
        rb.velocity = new Vector2(_moveInput.x * movementSpeed, rb.velocity.y);

        // Set the animator's isRunning parameter so that the AnimationController knows to shift from PlayerIdling to PlayerRunning and back.
        myAnimator.SetBool(IsRunningParam, IsMovingOnAxis(_moveInput.x));
    }

    private void FlipSprite()
    {
        // If moving (i.e. input.x != 0), use the Scale.x value of the Transform component as either -1 or 1 to flip the direction
        if (IsMovingOnAxis(_moveInput.x))
            transform.localScale = new Vector2(Mathf.Sign(_moveInput.x), transform.localScale.y);
    }

    private void OnJump(InputValue value)
    {
        // We check if the Player's collider is touching anything designated with the 'Ground' layer.
        // This means we can only jump off of the ground, rather than in midair.
        if (myCollider.IsTouchingLayers(groundLayerMask) && IsGrounded())
            rb.velocity += new Vector2(0f, jumpSpeed);
    }

    // We shoot a line from the bottom of the Player's position (the origin) downwards for 1 Unity unit of length, interacting only with the Ground layer.
    // If any collision is found, an implicit conversion to a bool will return true, else false
    private bool IsGrounded() => Physics2D.Raycast(
        origin: transform.position + Vector3.down * 0.5f,
        direction: Vector2.down,
        distance: 1f,
        groundLayerMask);

    private void ClimbLadder()
    {
        if (myCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            rb.gravityScale = 0f; // Prevent player from 'sinking' on the ladder if not moving
            rb.velocity = new Vector2(rb.velocity.x, _moveInput.y * climbSpeed);
            myAnimator.SetBool(IsClimbingParam,
                IsMovingOnAxis(_moveInput.y)); // Only play animation if we're on the ladder and moving
        }
        else
        {
            rb.gravityScale = DefaultGravity;
            myAnimator.SetBool(IsClimbingParam, false);
        }
    }

    // It is not recommended to make float comparisons against 0, even though the Input System gives a 0 when not moving.
    // This function creates a 'safe' comparison by taking the positive value of the movement and comparing it to Epsilon, which is a tiny value.
    // This overcomes floating-point inaccuracies.
    private static bool IsMovingOnAxis(float value) => Mathf.Abs(value) > Mathf.Epsilon;
}