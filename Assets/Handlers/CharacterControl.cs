using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterControl : MonoBehaviour
{
    [SerializeField] int speed = 50;
    [SerializeField] int jumpStrength = 5;
    [SerializeField] float sensitivity = 10;

    [SerializeField] Transform lookAt;
    [SerializeField] Animator characterAnim;
    [SerializeField] Transform character;

    Rigidbody rb;
    bool grounded;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        StaticPlayerInput.Input.Player.Enable();
        StaticPlayerInput.Input.Player.Jump.performed += Jump;
        StaticPlayerInput.Input.Player.Pause.performed += Pause;

        Cursor.lockState = CursorLockMode.Locked;
    }
    private void FixedUpdate()
    {
        rb.velocity = UpdateDirection(StaticPlayerInput.Input.Player.Move.ReadValue<Vector2>());
        /*float speed = Math.Abs(rb.velocity.x) + Math.Abs(rb.velocity.z);
        characterAnim.SetFloat("Speed", speed);*/

        /*rb.rotation = UpdateRotation(StaticPlayerInput.Input.Player.Look.ReadValue<Vector2>(), rb.rotation);*/
        lookAt.rotation = UpdateRotation(StaticPlayerInput.Input.Player.Look.ReadValue<Vector2>(), lookAt.rotation);
        /*if (characterAnim.GetFloat("Speed") > 0)
        {
            rb.rotation = lookAt.rotation;
            lookAt.localRotation = Quaternion.Euler(0,0,0);
        }*/
    }

    private Quaternion UpdateRotation(Vector2 input, Quaternion rotator)
    {
        input.Normalize();

        /*float newLook = Mathf.Clamp(lookAt.localPosition.y + input.y *.5f, 0f, 2f);
        lookAt.localPosition = new Vector3(0, newLook, .5f);*/

        return Quaternion.Lerp(rotator, Quaternion.Euler(0, rotator.eulerAngles.y + input.x*sensitivity, 0), .9f);

            
    }

    private Vector3 UpdateDirection(Vector2 input)
    { 
        Vector3 newDirection = CameraDirectionMovement(input, Camera.main);
        newDirection.Normalize();
        newDirection *= speed;

        characterAnim.SetFloat("Speed", newDirection.magnitude);
        character.rotation = Quaternion.LookRotation(newDirection);

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
        if (grounded || Physics.Raycast(rb.transform.position, Vector3.down, 1))
        {
            characterAnim.SetTrigger("Jump");
            grounded = false;
            characterAnim.SetBool("Grounded", grounded);
            rb.AddForce(Vector3.up * jumpStrength);
        }
    }

    private void Pause(InputAction.CallbackContext ctx)
    {
        if (Cursor.lockState == CursorLockMode.Locked)
            Cursor.lockState = CursorLockMode.None;
        else
            Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 6)
        {
            grounded = true;
            characterAnim.SetBool("Grounded", grounded);
        }

    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 6)
        {
            grounded = false;
            characterAnim.SetBool("Grounded", grounded);
        }
    }

}
