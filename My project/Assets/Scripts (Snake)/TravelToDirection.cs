using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelToDirection : MonoBehaviour
{
    public Vector3 direction = new Vector3(-16, 0, -17);
    float movementSpeed = 5;

    // Start is called before the first frame update
    void Start()
    {
        //direction *= 0.001f;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.Translate(direction.normalized * movementSpeed * Time.deltaTime);
    }
}
