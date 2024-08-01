using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Fountain : MonoBehaviour
{
    public HUDScript HUD;
    public Text HUDText;
    [SerializeField] GameObject waterObject;

    private bool isPlayerNear = false;
    [SerializeField] private PlayerHeat playerHeat;

    private void Start()
    {
        HUD.CloseMessagePanel();
    } 

    private void Update()
    {
        if (isPlayerNear && Keyboard.current.fKey.wasPressedThisFrame && waterObject != null) 
        {
            Drink();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision != null && waterObject != null)
        {
            isPlayerNear = true;
            HUD.OpenMessagePanel("");
            HUDText.text = "Pres 'F' to Drink from Foutain";
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        HUD.CloseMessagePanel();
    }

    void Drink()
    {
        if (playerHeat != null)
        {
            playerHeat.ApplyCoolingEffect(20f);
        }

        HUD.CloseMessagePanel();
        Destroy(waterObject);
    }
}
