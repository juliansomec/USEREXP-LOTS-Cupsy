using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDScript : MonoBehaviour
{
    public GameObject HUD;
    public int relicCount = 0;

    //Parts
    public RawImage[] Parts;
    public RawImage cooldownIcon;
    public RawImage heatImage;

    public bool canWin = false;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < Parts.Length; i++)
        {
            Parts[i].gameObject.SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (relicCount == 6)
        {
            canWin = true;
        }
    }

    public void SetCooldownOpacity(float alpha)
    {
        cooldownIcon.color = new Color(cooldownIcon.color.r, cooldownIcon.color.g, cooldownIcon.color.b, alpha);
    }

    public void OpenMessagePanel(string text)
    {
        HUD.SetActive(true);
    }//TODO: Be able to be used by other elements of the game

    public void CloseMessagePanel()
    {
        HUD.SetActive(false);
    }

    public int GetCompassParts() //change to GetRelicCount later
    {
        return relicCount;
    }

    public void AddRelicCount()
    {
        relicCount++;
    }

    public void SetHeatImageOpacity(float opacity)
    {
        if (heatImage != null)
        {
            Color color = heatImage.color;
            color.a = opacity;
            heatImage.color = color;
        }
    }

    public void SetHeatImageColor(Color color)
    {
        if (heatImage != null)
        {
            heatImage.color = color;
        }
    }

    public void ObtainPart(int part)
    {
        Parts[part].gameObject.SetActive(true);
    }
}
