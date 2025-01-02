using UnityEngine;

public class InfectState : GEState
{
    private GameObject targetVillager;

    public override void EnterState(GEntityAI geAI)
    {
        name = "Infect";
    }

    public override void UpdateState(GEntityAI geAI)
    {
        if(geAI.los.visibleEnemy.Contains(geAI.playerRef))
        {
            //get the infected people component and call "that" function.
            geAI.SwitchState(geAI.alertState);
        }
        else if(geAI.los.visibleEnemy.Count != 0)
        {
            targetVillager = geAI.los.visibleEnemy[0];
            geAI.agent.SetDestination(targetVillager.transform.position);
            if(Vector3.Distance(geAI.agent.transform.position, geAI.agent.destination) <= geAI.agent.stoppingDistance)
            {
                //Get Component and call function to freeze player
            }
        }
        else
        {
            geAI.SwitchState(geAI.wanderState);
        }
    }
}
