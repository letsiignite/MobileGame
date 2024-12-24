using UnityEngine;

public class ChaseState : GEState
{
    private float timer = 0f;
    private float waitTime = 2f;

    public override void EnterState(GEntityAI geAI)
    {
        // change speed and all that
        name = "Chase";
    }

    public override void UpdateState(GEntityAI geAI)
    {
        timer += Time.deltaTime;
        if (geAI.los.visibleEnemy.Contains(geAI.playerRef) && timer <= waitTime)
        {
            geAI.agent.SetDestination(geAI.playerRef.transform.position);
        }
        else if(!geAI.los.visibleEnemy.Contains(geAI.playerRef))
        {
            timer = 0f;
            geAI.alertState.reAlert = true;
            geAI.SwitchState(geAI.alertState);
        }
    }
}
