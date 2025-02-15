using System.Collections;
using System.ComponentModel.Design.Serialization;
using UnityEngine;
using UnityEngine.Audio;

public class PausedState : IVillagerState
{
    public void HandleState(VillagerContext context)
    {
        if (context.agent != null)
        {
            context.agent.isStopped = true;
        }
        context.Animator.SetTrigger("Idle");
        if (context.audioSource.isPlaying)
        {
            context.audioSource.Stop();
        }
        Debug.Log("Character is now Idle.");

    }
    public void UpdateState(VillagerContext context)
    {
        if (!context.isPaused)
        {
            context.SetState(context.runningState);
        }
    }
}