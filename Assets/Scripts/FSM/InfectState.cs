using UnityEngine;

public class InfectState : GEState
{
    public override void EnterState(GEntityAI geAI)
    {
        
    }

    public override void UpdateState(GEntityAI geAI)
    {
        if(geAI.los.visibleEnemy.Count != 0)
        {
            //get the infected people component and call "that" function.
        }
        else
        {
            geAI.SwitchState(geAI.alertState);
        }
    }
}
