using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class VillagerMovement : MonoBehaviour
{
    private List<Transform> points = new List<Transform>();
    private NavMeshAgent agent;
    private Animator animator;
    private bool isMoving = true;
    private int currentTargetIndex = 0;
    private float pathDeviation = 10.0f; // Amount of random deviation from target
    //private float minDistanceToTarget = 0.5f; // How close the villager has to be to consider the target reached

    void Start()
    {
        // Get the NavMeshAgent and Animator components
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        // Start moving if points are set
        if (points.Count > 0)
        {
            MoveToRandomPoint();
        }
    }

    public void SetPoints(List<Transform> spawnPoints)
    {
        points = spawnPoints;
    }

    void Update()
    {
        // Check if the villager has reached the destination and is moving
        if (isMoving && agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
        {
            // Move to the next random point
            MoveToRandomPoint();
        }
    }

    void MoveToRandomPoint()
    {
        if (points.Count == 0) return;

        // Randomize the path by selecting a target and adding slight deviation
        currentTargetIndex = Random.Range(0, points.Count); 
        Transform targetPoint = points[currentTargetIndex];

        Vector3 randomDeviation = new Vector3(
            Random.Range(-pathDeviation, pathDeviation),
            0, // Assuming movement is on a flat plane (use Y for vertical deviation if needed)
            Random.Range(-pathDeviation, pathDeviation)
        );

        Vector3 targetWithDeviation = targetPoint.position + randomDeviation;

        // Calculate the path to the destination
        NavMeshPath path = new NavMeshPath();
        if (agent.CalculatePath(targetWithDeviation, path) && path.status == NavMeshPathStatus.PathComplete)
        {
            // Set the destination to the calculated path
            agent.SetPath(path);

            // Trigger the run animation
            if (animator != null)
            {
                animator.SetTrigger("Run");
            }

            // Move to the next point in the list, looping if necessary
            //currentTargetIndex = (currentTargetIndex + 1) % points.Count;
        }
        else
        {
            Debug.LogWarning($"Path to {targetPoint.name} could not be found or is incomplete.");
        }
    }

    public void StopMovement()
    {
        // Stop the villager from moving
        isMoving = false;
        if (agent != null)
        {
            agent.isStopped = true;
        }

        // Trigger an idle or stop animation
        if (animator != null)
        {
            animator.SetTrigger("Idle");
        }
    }
}


