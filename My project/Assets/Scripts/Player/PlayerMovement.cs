using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public InputManager inputManager;

    public Rigidbody rb;

    public float speed = 10f;
    public float crouchSpeed = 5f;
    public float runSpeed = 16f;

    public bool isCrouching;
    public bool isRunning;

    public HUDScript Hud;

    private void Update()
    {
        // Read input values
        float forward = inputManager.inputMaster.Movement.Forward.ReadValue<float>();
        float right = inputManager.inputMaster.Movement.Right.ReadValue<float>();

        Vector3 move = transform.right * right + transform.forward * forward;

        // Check if the player is crouching
        bool isCrouching = inputManager.inputMaster.Movement.Crouch.ReadValue<float>() != 0;

        if (isCrouching)
        {
            move *= crouchSpeed;
        }
        else
        {
            move *= inputManager.inputMaster.Movement.Run.ReadValue<float>() == 0 ? speed : runSpeed;
        }

        // Adjust the player's height based on crouching state
        transform.localScale = new Vector3(1, isCrouching ? 0.4618f : 1f, 1);

        rb.velocity = new Vector3(move.x, rb.velocity.y, move.z);
    }

    public bool IsCrouching()
    {
        return isCrouching;
    }

    public bool IsRunning()
    {
        return isRunning;
    }
}
