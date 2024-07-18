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

    [SerializeField] private bool isCooling = false;
    float coolingDuration = 20f;

    private void Awake()
    {
        if (heatRange == null)
        {
            heatRange = GetComponent<SphereCollider>();
        }

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
        
        if (isCooling)
        {
            targetHeatRange = crouchHeatRange;
        }
        else
        {
            if (isCrouching)
            {
                targetHeatRange = crouchHeatRange;
            }
            else if (isRunning)
            {
                targetHeatRange = runHeatRange;
            }
            else
            {
                targetHeatRange = normalHeatRange;
            }
        }

        heatRange.radius = Mathf.Lerp(heatRange.radius, targetHeatRange, 1 * Time.deltaTime);

        Debug.Log(targetHeatRange);
    }

    public void ApplyCoolingEffect(float duration)
    {
        StartCoroutine(CoolingEffectCoroutine(duration));
    }

    IEnumerator CoolingEffectCoroutine(float duration)
    {
        isCooling = true;
        yield return new WaitForSeconds(duration);
        isCooling = false;
    }
}
