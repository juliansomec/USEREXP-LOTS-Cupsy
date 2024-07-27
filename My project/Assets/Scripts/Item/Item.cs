using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public HUDScript HUD;
    public Text HUDText;

    private bool isPlayerNear = false;

    private void Start()
    {
        HUD.CloseMessagePanel();
    }

    private void Update()
    {
        if (isPlayerNear && Keyboard.current.fKey.wasPressedThisFrame) 
        {
            PickUp();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision != null)
        {
            isPlayerNear = true;
            HUD.OpenMessagePanel("");
            HUDText.text = "Press 'F' to Obtain";
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        HUD.CloseMessagePanel();
    }

    void PickUp()
    {
        HUD.CloseMessagePanel();
        HUD.AddCompassParts();
        Destroy(gameObject);
    }
}
