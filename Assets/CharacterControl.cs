using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterControl : MonoBehaviour
{
    [SerializeField] int speed = 50;
    [SerializeField] int jumpStrength = 5;

    Rigidbody rb;
    bool grounded = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        StaticPlayerInput.Input.Player.Enable();
        StaticPlayerInput.Input.Player.Jump.performed += Jump;
    }

    private void FixedUpdate()
    {
        rb.velocity = UpdateDirection(StaticPlayerInput.Input.Player.Move.ReadValue<Vector2>());
    }
    private Vector3 UpdateDirection(Vector2 input)
    { 
        Vector3 newDirection = CameraDirectionMovement(input, Camera.main);
        newDirection.Normalize();
        newDirection *= speed;

        newDirection.y = rb.velocity.y;

        return newDirection;
    }

    private Vector3 CameraDirectionMovement(Vector2 input, Camera cam)
    {
        Vector3 camRight = cam.transform.right;
        camRight.y = 0;
        camRight.Normalize();

        Vector3 camForward = cam.transform.forward;
        camForward.y = 0;
        camForward.Normalize();

        return input.x * camRight + camForward * input.y;
    }

    private void Jump(InputAction.CallbackContext ctx)
    {
        if (grounded)
        {
            Debug.Log("jump");
            rb.AddForce(Vector3.up * jumpStrength);
        }
    }
}
