using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 20f; //for mouse sens

    public Transform playerBody; //for coders to specify what gameObject the player's body will be in unity editor

    float xRotation = 0f; //for rotating around x axis

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //to hide the cursor when player is looking around
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime; //gets the x axis of mouse, multiply with mouseSensitivity variable to adjust sens speed, multiply with Time.deltaTime to have a smoother rotation and be independent from the frame rate
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime; //gets the y axis of mouse, multiply with mouseSensitivity variable to adjust sens speed, multiply with Time.deltaTime to have a smoother rotation and be independent from the frame rate

        xRotation -= mouseY; //every frame will decrease x Rotation based on mouse y, making it plus will flip rotation
        xRotation = Mathf.Clamp(xRotation, -80f, 70f); //ensures that the player won't overrotate and look behind themselves, ensuring that they could only look between -80 and 70 vertically

        /*Debug.Log("mouseX: " + mouseX + ", mouseY: " + mouseY);*/

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); //applies the mouseY rotation, Quaternion is responsible for rotations
        playerBody.Rotate(Vector3.up * mouseX); //for Mouse X movement, multiplying Vector3.up, which is the y axis, with mouseX makes the player rotate around the y axis, which allows the player to look left or right
    }
}
