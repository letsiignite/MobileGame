using UnityEngine;
using UnityEngine.AI;

public class RunningState : IVillagerState
{
    public void HandleState(VillagerContext context)
    {
        //Debug.Log("Character is now Running.");
        if (context.agent != null)
        {
            context.agent.isStopped = false;
        }
        MoveToRandomPoint(context);
    }
    void MoveToRandomPoint(VillagerContext context)
    {
        Debug.Log("in here");
        if (context.points.Count == 0) return;

        // Randomize the path by selecting a target and adding slight deviation
        context.currentTargetIndex = Random.Range(0, context.points.Count);
        Transform targetPoint = context.points[context.currentTargetIndex];

        Vector3 randomDeviation = new Vector3(
            Random.Range(-context.pathDeviation, context.pathDeviation),
            0, // Assuming movement is on a flat plane (use Y for vertical deviation if needed)
            Random.Range(-context.pathDeviation, context.pathDeviation)
        );

        Vector3 targetWithDeviation = targetPoint.position + randomDeviation;

        // Calculate the path to the destination
        NavMeshPath path = new NavMeshPath();
        if (context.agent.CalculatePath(targetWithDeviation, path) && path.status == NavMeshPathStatus.PathComplete)
        {
            // Set the destination to the calculated path
            context.agent.SetPath(path);

            // Trigger the run animation
            context.Animator.SetTrigger("Run");

        }
        else
        {
            Debug.LogWarning($"Path to {targetPoint.name} could not be found or is incomplete.");
        }
    }
    public void UpdateState(VillagerContext context)
    {
        Debug.Log("Character is running...");
        if (context.isMoving && context.agent.remainingDistance <= context.agent.stoppingDistance && !context.agent.pathPending)
        {
            // Move to the next random point
            MoveToRandomPoint(context);
        }
        if (!context.isMoving)
        {
            context.SetState(context.idleState);
        }
    }
}
