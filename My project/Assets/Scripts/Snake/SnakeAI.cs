using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class SnakeAI : MonoBehaviour
{
    private float snakeBodyCooldown = 12f; // Cooldown in milliseconds
    private float snakeRoamTime = 30f;

    public GameObject petPrefab;

    [SerializeField] List<Transform> bodies = new List<Transform>();
    [SerializeField] private Transform goalTransform;
    [SerializeField] private Transform originalGoalTransform;
    [SerializeField] private Transform newGoalTransform;

    private NavMeshAgent nav;

    private bool isCooldownActive = false;
    private bool isRoaming = false;

    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        bodies.Add(transform);

        // Save the original goal transform
        originalGoalTransform = goalTransform;

        // Add the head to the bodies list and ensure it has a Body component
        if (bodies.Count > 1)
        {
            if (GetComponent<Body>() == null)
            {
                gameObject.AddComponent<Body>();
            }
        }
    }

    private void Update()
    {
        nav.destination = goalTransform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !isCooldownActive)
        {
            isRoaming = true;
            goalTransform = newGoalTransform; // Set goal to newGoalTransform
            StartCoroutine(SnakeRoamTimer());
            AddBody(); // Add body upon collision with player
            StartCoroutine(CooldownTimer());
        }
    }

    private IEnumerator CooldownTimer()
    {
        isCooldownActive = true;

        float elapsedTime = 0f;

        while (elapsedTime < snakeBodyCooldown)
        {
            yield return new WaitForSeconds(1f);
            elapsedTime += 1f;
            Debug.Log($"Cooldown time passed: {elapsedTime} seconds");
        }

        isCooldownActive = false;
    }

    private IEnumerator SnakeRoamTimer()
    {
        float elapsedTime = 0f;

        while (elapsedTime < snakeRoamTime)
        {
            yield return new WaitForSeconds(1f);
            elapsedTime += 1f;
            Debug.Log($"Snake Roaming: {elapsedTime} seconds");
        }

        // After snakeRoamTime seconds, switch back to originalGoalTransform
        goalTransform = originalGoalTransform;
        isRoaming = false;
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

        // Checks if spawned body has Body script, if not, adds a script to it
        if (newBodyScript != null)
        {
            newBodyScript.target = lastBody;
        }

        // Add the new body to the list
        bodies.Add(newBody.transform);
    }
}