using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    public GameObject exitLight;
    public HUDScript hudScript;

    void Start()
    {
        exitLight.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (hudScript.GetCompassParts() == 6)
        {
            exitLight.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hudScript.canWin == true && hudScript.GetCompassParts() == 6)
        {
            Debug.Log("You win");
        }
    }
}
