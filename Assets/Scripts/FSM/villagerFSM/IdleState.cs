using System.Collections;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

public class IdleState : IVillagerState
{
    public void HandleState(VillagerContext context)
    {
        Debug.Log("Character is now Idle.");
        if (context.agent != null)
        {
            context.agent.isStopped = true;
        }
        context.Animator.SetTrigger("Idle");
        context.StartCoroutine(ResumeAfterDelay(5f,context));
    }
    private IEnumerator ResumeAfterDelay(float delay, VillagerContext context)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Resume moving
        context.isMoving = true;
    }
    public void UpdateState(VillagerContext context)
    {
        Debug.Log("Character is idle...");
        if(context.isMoving)
        {
            context.SetState(context.runningState);
        }
    }
}
