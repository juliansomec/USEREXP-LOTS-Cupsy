using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeat : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement;
    public SphereCollider heatRange;

    [SerializeField] float normalHeatRange = 1.5f;
    [SerializeField] float crouchHeatRange = 0.2f;
    [SerializeField] float runHeatRange = 4f;
    float targetHeatRange;

    private bool isCrouching = false;
    private bool isRunning = false;

    private void Awake()
    {
        if (heatRange == null)
        {
            heatRange = GetComponent<SphereCollider>();
        }

        // Ensure playerMovement reference is set
        if (playerMovement == null)
        {
            playerMovement = GetComponent<PlayerMovement>();
        }

        if (playerMovement == null)
        {
            Debug.LogWarning("PlayerMovement component not found!");
        }
    }

    void Update()
    {
        if (playerMovement == null)
        {
            Debug.LogWarning("Set Player Movement Script first!");
            return;
        }

        // Check if the player is crouching
        isCrouching = playerMovement.inputManager.inputMaster.Movement.Crouch.ReadValue<float>() != 0;
        // Check if the player is running
        isRunning = playerMovement.inputManager.inputMaster.Movement.Run.ReadValue<float>() != 0;

        if (heatRange != null)
        {
            if (isCrouching)
            {
                targetHeatRange = crouchHeatRange;
                Debug.Log($"Heat Range (Crouching): {heatRange.radius}");
            }
            else if (isRunning)
            {
                targetHeatRange = runHeatRange;
                Debug.Log($"Heat Range (Running): {heatRange.radius}");
            }
            else
            {
                targetHeatRange = normalHeatRange;
                Debug.Log($"Heat Range (Normal): {heatRange.radius}");
            }
        }

        heatRange.radius = Mathf.Lerp(heatRange.radius, targetHeatRange, 1 * Time.deltaTime);

        Debug.Log($"Crouching: {isCrouching}, Running: {isRunning}");
    }
}
