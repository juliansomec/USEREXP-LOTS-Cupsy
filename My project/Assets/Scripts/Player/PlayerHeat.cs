using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeat : MonoBehaviour
{
    [SerializeField] FirstPersonController playerScript;
    public SphereCollider heatRange;

    [SerializeField] float normalHeatRange = 1.5f;
    [SerializeField] float crouchHeatRange = 0.2f;
    [SerializeField] float runHeatRange = 4f;
    float targetHeatRange;

    private bool isCrouching = false;
    private bool isRunning = false;

    [SerializeField] private bool isCooling = false;
    float coolingDuration = 20f;

    [SerializeField] private HUDScript hudScript;

    private float currentOpacity;
    private float targetOpacity;
    private float opacityTransitionSpeed = 2f;

    private Color originalColor;
    private Color coolingColor = Color.blue;

    private void Awake()
    {
        if (heatRange == null)
        {
            heatRange = GetComponent<SphereCollider>();
        }

        if (playerScript == null)
        {
            playerScript = GetComponent<FirstPersonController>();
        }

        if (playerScript == null)
        {
            Debug.LogWarning("PlayerMovement component not found!");
        }

        hudScript = FindObjectOfType<HUDScript>();
        if (hudScript == null)
        {
            Debug.LogWarning("HUDScript component not found!");
        }

        currentOpacity = 0.5f;
        targetOpacity = 0.5f;

        if (hudScript != null && hudScript.heatImage != null)
        {
            originalColor = hudScript.heatImage.color;
        }
    }

    void Update()
    {
        if (playerScript == null)
        {
            Debug.LogWarning("Set Player Movement Script first!");
            return;
        }

        // Check if the player is crouching
        isCrouching = playerScript.IsCrouched();
        // Check if the player is running
        isRunning = playerScript.IsSprinting();

        if (isCooling)
        {
            targetHeatRange = crouchHeatRange;
        }
        else
        {
            if (isCrouching)
            {
                targetHeatRange = crouchHeatRange;
                targetOpacity = 0.3f;
            }
            else if (isRunning)
            {
                targetHeatRange = runHeatRange;
                targetOpacity = 1.0f;
            }
            else
            {
                targetHeatRange = normalHeatRange;
                targetOpacity = 0.6f;
            }
        }

        heatRange.radius = Mathf.Lerp(heatRange.radius, targetHeatRange, 1 * Time.deltaTime);

        currentOpacity = Mathf.Lerp(currentOpacity, targetOpacity, opacityTransitionSpeed * Time.deltaTime);
        hudScript.SetHeatImageOpacity(currentOpacity);
    }

    public void ApplyCoolingEffect(float duration)
    {
        StartCoroutine(CoolingEffectCoroutine(duration));
    }

    IEnumerator CoolingEffectCoroutine(float duration)
    {
        isCooling = true;
        float elapsedTime = 0f;
        Color initialColor = hudScript.heatImage.color;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / duration);
            hudScript.SetHeatImageColor(coolingColor); // Set color to blue
            hudScript.SetHeatImageOpacity(alpha);
            yield return null;
        }

        isCooling = false;
        hudScript.SetHeatImageColor(originalColor); // Revert to original color
        hudScript.SetHeatImageOpacity(targetOpacity); // Ensure opacity matches the state (normal, running, crouching)
    }
}
