using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScare : MonoBehaviour
{
    public Canvas UICanvas;
    private void Awake()
    {
        UICanvas.enabled = false;
    }
    private void OnCollisionEnter(Collision other)
    {
        UICanvas.enabled = true;
    }
}
