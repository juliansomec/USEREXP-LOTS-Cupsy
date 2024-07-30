using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDScript : MonoBehaviour
{
    public GameObject HUD;
    public Text compassPartsText;
    public int compassPartsCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        UpdateCompassPartsText();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.LogWarning(compassPartsCount);
    }

    public void AddCompassParts()
    {
        compassPartsCount++;
        UpdateCompassPartsText();
    }

    private void UpdateCompassPartsText()
    {
        compassPartsText.text = "x" + compassPartsCount;
    }

    public void OpenMessagePanel(string text)
    {
        HUD.SetActive(true);
    }//TODO: Be able to be used by other elements of the game

    public void CloseMessagePanel()
    {
        HUD.SetActive(false);
    }

    public int GetCompassParts()
    {
        return compassPartsCount;
    }
}
