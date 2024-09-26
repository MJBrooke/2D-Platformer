using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 _moveInput;
    private bool _jumpInput;
    
    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    private void OnMove(InputValue value)
    {
        _moveInput = value.Get<Vector2>();
        Debug.Log("Move: " + _moveInput);
    }

    private void OnJump(InputValue value)
    {
        _jumpInput = value.Get<float>() > 0f;
        Debug.Log("Jump: " + _jumpInput);
    }
}
