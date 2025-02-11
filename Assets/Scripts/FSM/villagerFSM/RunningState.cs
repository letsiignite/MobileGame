using System.Collections.Generic;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Audio;

public class RunningState : IVillagerState
{
    [SerializeField]
    private float nextPlayTime = 0f; // Timer to control playback
    public void HandleState(VillagerContext context)
    {
        //Debug.Log("Character is now Running.");
        if (context.agent != null)
        {
            context.agent.isStopped = false;
        }

        MoveToRandomPoint(context);

        if (context.audioSource.isPlaying)
        {
            context.audioSource.Stop();
        }

        PlayRandomClip(context);

        context.Animator.SetTrigger("Run");
        Debug.Log("setting run trigger");
    }
    void PlayRandomClip(VillagerContext context)
    {
        // Pick a random clip
        AudioClip randomClip = context.runningAudioClips[Random.Range(0, context.runningAudioClips.Count)];

        // Assign and play it
        context.PlaySound(randomClip);

        // Set the next play time to avoid instant replay
        nextPlayTime = Time.time + randomClip.length + Random.Range(0.5f, 2f); // Add random delay for variation
    }
    void MoveToRandomPoint(VillagerContext context)
    {
        //Debug.Log("in here");
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
        }
    }
    public void UpdateState(VillagerContext context)
    {
        if (context.runningAudioClips.Count == 0) return;

        // Check if the audio has finished playing
        if (!context.audioSource.isPlaying && Time.time >= nextPlayTime)
        {
            PlayRandomClip(context);
        }
        //Debug.Log("Character is running...");
        if (context.isNotInfected && context.agent.remainingDistance <= context.agent.stoppingDistance && !context.agent.pathPending)
        {
            // Move to the next random point
            MoveToRandomPoint(context);
        }
        if(context.isPaused)
        {
            context.SetState(context.pausedState);
        }
        if (!context.isNotInfected)
        {
            context.SetState(context.infectedState);
        }
    }
}
