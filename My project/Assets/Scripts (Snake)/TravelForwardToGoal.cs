using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelForwardToGoal : MonoBehaviour
{
    public Transform goal;
    public float speed = 3;
    public float rotSpeed = 3;
    public float distanceThreshold = 2.7f;

    public GameObject petPrefab;
    [SerializeField] List<Transform> bodies = new List<Transform>();

    void Start()
    {
        bodies.Add(transform);

        // Add the head to the bodies list and ensure it has a Body component
        if (bodies.Count > 1)
        {
            if (GetComponent<Body>() == null)
            {
                gameObject.AddComponent<Body>();
            }
        }
    }

    void Update()
    {
        Vector3 lookAtGoal = new Vector3(goal.position.x,
                                         transform.position.y,
                                         goal.position.z);

        Vector3 direction = lookAtGoal - transform.position;

        // Slerp helps with a more natural rotation (SLERP = Spherical Linear Interpolation)
        transform.rotation = Quaternion.Slerp(transform.rotation,
                                              Quaternion.LookRotation(direction),
                                              Time.deltaTime * rotSpeed);

        if (Vector3.Distance(lookAtGoal, transform.position) > distanceThreshold)
        {
            transform.Translate(0, 0, speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Player"))
        {
            Debug.Log("Spawned a body");
            AddBody();
        }
    }

    public void AddBody()
    {
        // Get the last body in the list
        Transform lastBody = bodies[bodies.Count - 1];

        // Calculate the position behind the last body
        Vector3 directionToLastBody = (lastBody.position - transform.position).normalized;
        Vector3 newBodyPosition = lastBody.position - directionToLastBody * lastBody.localScale.z;

        // Instantiate a new body and set it to follow the last body
        GameObject newBody = Instantiate(petPrefab, newBodyPosition, lastBody.rotation);
        Body newBodyScript = newBody.GetComponent<Body>();

        //Checks if spawned body has Body script, if not, adds a script to it
        if (newBodyScript != null)
        {
            newBodyScript.target = lastBody;
        }

        // Add the new body to the list
        bodies.Add(newBody.transform);
    }
}