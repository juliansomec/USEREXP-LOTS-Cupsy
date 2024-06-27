using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDScript : MonoBehaviour
{
    public GameObject HUD;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenMessagePanel(string text)
    {
        HUD.SetActive(true);
    }//TODO: Be able to be used by other elements of the game

    public void CloseMessagePanel()
    {
        HUD.SetActive(false);
    }
}
