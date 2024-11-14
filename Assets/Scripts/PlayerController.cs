using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public PlayerController playerController;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpPower;

    private bool isJumping = false;
    private bool isGrounded = false;

    [SerializeField] private LayerMask groundLayerMask;

    private Vector2 curMovementInput;
    private Vector3 direction;

    private CharacterController characterController;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsGrounded())
        {
            Move();
            direction.x = curMovementInput.x * moveSpeed;

            if (isJumping)
            {
                direction.y = jumpPower;
                isJumping = false;
            }
            else
            {
                direction.y += Physics.gravity.y * Time.fixedDeltaTime;
            }
        }
        else
        {
            direction.y += Physics.gravity.y * Time.fixedDeltaTime;
        }
    }

    void Move()
    {
        Vector3 direction = transform.forward * curMovementInput.x + transform.right * curMovementInput.y;
        direction *= moveSpeed;
        direction.y = _rigidbody.velocity.y;
        _rigidbody.velocity = direction;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            curMovementInput = context.ReadValue<Vector2>();
        }
        else if(context.phase == InputActionPhase.Canceled)
        {
            curMovementInput = Vector3.zero;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started)
        {
            isJumping = true;
        }
    }

    private bool IsGrounded()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down)
        };

        for(int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 1f, groundLayerMask))
            {
                return true;
            }
        }
        return false;
    }
}
