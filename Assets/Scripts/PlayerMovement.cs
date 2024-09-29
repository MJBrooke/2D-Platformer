using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private static readonly int IsRunningParam = Animator.StringToHash("isRunning");

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float jumpSpeed = 5f;

    private Vector2 _moveInput;

    private void Start()
    {
    }

    private void Update()
    {
        Run();
        FlipSprite();
    }

    // This is invoked each time we input movement via the Input System.
    // We get a Vector2 type with the x and y value, between 0 and 1 inclusive.
    private void OnMove(InputValue value)
    {
        // We store this value into a field so that it can be used by other functions without passing this around as a parameter.
        _moveInput = value.Get<Vector2>();
    }
    
    // It is not recommended to make float comparisons against 0, even though the Input System gives a 0 when not moving.
    // This function creates a 'safe' comparison by taking the positive value of the movement and comparing it to Epsilon, which is a tiny value.
    // This overcomes floating-point inaccuracies.
    private bool IsRunning() => Mathf.Abs(_moveInput.x) > Mathf.Epsilon;
    
    private void Run()
    {
        // We set the x-value to our movement speed. If input is non-zero, this creates horizontal movement.
        // We set the y-value to the existing value of the RigidBody so that we don't override gravity.
        rb.velocity = new Vector2(_moveInput.x * movementSpeed, rb.velocity.y);
        
        // Set the animator's isRunning parameter so that the AnimationController knows to shift from PlayerIdling to PlayerRunning and back.
        animator.SetBool(IsRunningParam, IsRunning());
    }

    private void FlipSprite()
    {
        // If moving (i.e. input.x != 0), use the Scale.x value of the Transform component as either -1 or 1 to flip the direction
        if (IsRunning())
            transform.localScale = new Vector2(Mathf.Sign(_moveInput.x), transform.localScale.y);
    }

    private void OnJump(InputValue value)
    {
        // if (!value.isPressed) return;
        
        if (value.isPressed) rb.velocity += new Vector2(0f, jumpSpeed);
    }
}