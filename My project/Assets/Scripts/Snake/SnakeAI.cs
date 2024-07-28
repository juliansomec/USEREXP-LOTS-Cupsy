using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class SnakeAI : MonoBehaviour
{
    private float snakeBodyCooldown = 1f; // Cooldown in milliseconds
    public float snakeKills = 0f; //JUST FOR TESTING, PWEDENG TANGGALIN TO AH
    public float maxSnakeKills = 5f; //PWEDE RIN TO TANGGALIN

    public GameObject petPrefab;

    [SerializeField] List<Transform> bodies = new List<Transform>();
    [SerializeField] public Transform goalTransform;
    [SerializeField] private Transform originalGoalTransform;
    [SerializeField] private List<Transform> newGoalTransforms;
    [SerializeField] private Transform playerRespawnTransform;
    [SerializeField] public Transform lastGoalTransform; // Store the last goal transform

    [SerializeField] private SphereCollider playerHeat;
    [SerializeField] private Transform playerTransform;

    private SnakeLineOfSight snakeLineOfSight;

    private NavMeshAgent nav;

    private bool isCooldownActive = false;
    private bool isRoaming = true;

    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        snakeLineOfSight = GetComponent<SnakeLineOfSight>();
        bodies.Add(transform);

        goalTransform = GetRandomGoalTransform();

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
        if (isRoaming == true && snakeLineOfSight.playerInSight == false)
        {
            nav.destination = goalTransform.position;
        }
        else if (isRoaming == false || snakeLineOfSight.playerInSight == true)
        {
            nav.destination = playerTransform.position;
            Debug.Log("Following Player");
        }

        if (snakeKills == maxSnakeKills)
        {
            SceneManager.LoadScene(0);
        }

        if (goalTransform == lastGoalTransform)
        {
            Debug.Log("The Error Happened so finding another goalTransform i guess.");
            goalTransform = GetRandomGoalTransform();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !isCooldownActive)
        {
            snakeKills += 1f;
            isRoaming = true;
            goalTransform = GetRandomGoalTransform(); // Set goal to a random new goal transform
            lastGoalTransform = goalTransform; // Update last goal transform
            /*StartCoroutine(SnakeRoamTimer());*/
            AddBody(); // Add body upon collision with player
            StartCoroutine(CooldownTimer());
            other.transform.position = playerRespawnTransform.position;
        }

        else if (other.gameObject.CompareTag("NewGoalTransform"))
        {
            lastGoalTransform = goalTransform; // Update last goal transform
            goalTransform = GetRandomGoalTransform(); // Set goal to another random new goal transform
            Debug.Log(goalTransform);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other == playerHeat && !isCooldownActive)
        {
            isRoaming = false;
            Debug.Log("player detected");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other == playerHeat && !isCooldownActive)
        {
            isRoaming = true;
            Debug.Log("snake lost heat of player");
            goalTransform = GetRandomGoalTransform();
        }
    }

    public Transform GetRandomGoalTransform()
    {
        Debug.Log("GetRandomGoalTransform called.");

        // Create a list of available goal transforms
        List<Transform> availableGoalTransforms = new List<Transform>(newGoalTransforms);

        // Ensure the list is not empty before proceeding
        if (availableGoalTransforms.Count == 0)
        {
            Debug.LogError("No goal transforms available.");
            return null; // Or handle this case as needed
        }

        // Remove the last goal transform from the list if it's not null
        if (lastGoalTransform != null && availableGoalTransforms.Contains(lastGoalTransform))
        {
            availableGoalTransforms.Remove(lastGoalTransform);
        }

        Debug.Log("Available Goal Transforms:");
        foreach (Transform t in availableGoalTransforms)
        {
            Debug.Log(t.name); // Log the name of each transform for easy identification
        }

        // If the list becomes empty after removal, you may need to handle this case
        if (availableGoalTransforms.Count == 0)
        {
            Debug.LogWarning("No available goal transforms left after removal.");
            return null; // Or handle this case as needed
        }

        // Randomize the selection of a new goal transform
        int randomIndex = Random.Range(0, availableGoalTransforms.Count);

        // Return the chosen transform
        return availableGoalTransforms[randomIndex];
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

/*    private IEnumerator SnakeRoamTimer()
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
    }*/

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