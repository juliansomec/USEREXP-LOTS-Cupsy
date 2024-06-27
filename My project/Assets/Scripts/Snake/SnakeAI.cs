using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class SnakeAI : MonoBehaviour
{
    public float snakeBodyCooldown = 1200f;
    public float currentSnakeBodyCooldown;

    public GameObject petPrefab;

    [SerializeField] List<Transform> bodies = new List<Transform>();
    [SerializeField] private Transform goalTransform;

    private NavMeshAgent nav;

    // Start is called before the first frame update
    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();

        currentSnakeBodyCooldown = snakeBodyCooldown;

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

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Snake Body Spawn Cooldown: " + currentSnakeBodyCooldown);

        if (currentSnakeBodyCooldown > 0)
        {
            currentSnakeBodyCooldown--;
        }

        nav.destination = goalTransform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Spawn Cooldown: " + currentSnakeBodyCooldown);
        if (other.gameObject.CompareTag("Player") && currentSnakeBodyCooldown <= 0)
        {
            Debug.Log("Spawned a body");
            AddBody();
            currentSnakeBodyCooldown = snakeBodyCooldown;
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
