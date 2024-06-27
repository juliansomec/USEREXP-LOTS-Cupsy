using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public HUDScript HUD;

    private void OnCollisionEnter(Collision collision)
    {
        HUD.OpenMessagePanel("Press F to Obtain");
    }

    private void OnCollisionExit(Collision collision)
    {
        HUD.CloseMessagePanel();
    }
}
