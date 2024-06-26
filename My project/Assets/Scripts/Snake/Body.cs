using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour
{
    public GameObject petPrefab; // Prefab for the body

    public Transform target; // The target to follow
    public float speed = 3f; // Movement speed
    public float rotSpeed = 3f; // Rotation speed
    public float distanceThreshold = 2.7f; // Distance threshold to start moving

    void Update()
    {
            Vector3 lookAtTarget = new Vector3(target.position.x, transform.position.y, target.position.z);
            Vector3 direction = lookAtTarget - transform.position;

            // Slerp helps with a more natural rotation (SLERP = Spherical Linear Interpolation)
            transform.rotation = Quaternion.Slerp(transform.rotation,
                                                    Quaternion.LookRotation(direction),
                                                    Time.deltaTime * rotSpeed);

            if (Vector3.Distance(lookAtTarget, transform.position) > distanceThreshold)
            {
                transform.Translate(0, 0, speed * Time.deltaTime); // Move towards the target
            }
    }
}