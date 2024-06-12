using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public InputManager inputManager;

    public Rigidbody rb;

    public float speed = 5f;
    public float runSpeed = 15f;

 
    private void Awake()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        
    }

    private void Update()
    {
        float forward = inputManager.inputMaster.Movement.Forward.ReadValue<float>();
        float right = inputManager.inputMaster.Movement.Right.ReadValue<float>();
        Vector3 move = transform.right * right + transform.forward * forward;

        move *= inputManager.inputMaster.Movement.Run.ReadValue<float>() == 0 ? speed : runSpeed;
        transform.localScale = new Vector3(1, inputManager.inputMaster.Movement.Crouch.ReadValue<float>() == 0 ? 1f : 0.4618f, 1);

        rb.velocity = new Vector3(move.x, rb.velocity.y, move.z);
    }
}
