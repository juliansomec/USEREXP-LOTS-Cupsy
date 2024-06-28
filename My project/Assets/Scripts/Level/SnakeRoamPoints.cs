using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeRoamPoints : MonoBehaviour
{
    void FixedUpdate()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Snake"))
        {
            Debug.Log("Snake has reached Roaming Point");
        }
    }
}
