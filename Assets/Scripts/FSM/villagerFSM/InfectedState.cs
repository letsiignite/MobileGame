using System.Collections;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

public class InfectedState : IVillagerState
{
    public void HandleState(VillagerContext context)
    {
        if (context.agent != null)
        {
            context.agent.isStopped = true;
            //Debug.Log("Agent isStopped set to: " + context.agent.isStopped);
        }
        context.Animator.SetTrigger("Idle");
        //Debug.Log("Character is now Infected.");
        context.StartCoroutine(ResumeAfterDelay(5f,context));
    }
    private IEnumerator ResumeAfterDelay(float delay, VillagerContext context)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Resume moving
        context.isNotInfected = true;
    }
    public void UpdateState(VillagerContext context)
    {
        //Debug.Log("Character is idle...");
        if(context.isPaused)
        {
            context.SetState(context.pausedState);
        }
        if(context.isNotInfected)
        {
            context.SetState(context.runningState);
        }
    }
}
