using UnityEngine;
using UnityEngine.AI;

public class AlertState : GEState
{
    private Vector3 destPoint;
    private Vector3 lastSeenPlayerPos;
    private float rangeOfSearch = 5f;
    private bool walkPointSet;
    private float timer = 0f;
    private float waitTime = 4f;

    public bool reAlert;

    public override void EnterState(GEntityAI geAI)
    {
        name = "Alert";
        lastSeenPlayerPos = geAI.playerRef.transform.position;
    }

    public override void UpdateState(GEntityAI geAI)
    {
        timer += Time.deltaTime;
        if (geAI.playerIsNearby && timer <= waitTime)
        {
            if (walkPointSet) 
            {
                geAI.agent.SetDestination(destPoint);
                if (Vector3.Distance(geAI.agent.transform.position, geAI.agent.destination) <= geAI.agent.stoppingDistance)
                {
                    walkPointSet = false;
                }
            }
            else
            {
                GetWayPoint(geAI);
            }
        }
        else if (geAI.los.visibleEnemy.Contains(geAI.playerRef) && timer <= waitTime)
        {
            Debug.Log(timer + " : Chase");
            timer = 0f;
            geAI.SwitchState(geAI.chaseState);
        }
        else if (!(geAI.los.visibleEnemy.Contains(geAI.playerRef) || geAI.playerIsNearby) && timer > waitTime)
        {
            Debug.Log(timer + " : Wander");
            timer = 0f;
            geAI.SwitchState(geAI.wanderState);
        }
        else
        {
            if (walkPointSet)
            {
                geAI.agent.SetDestination(destPoint);
                if (Vector3.Distance(geAI.agent.transform.position, geAI.agent.destination) <= geAI.agent.stoppingDistance)
                {
                    walkPointSet = false;
                }
            }
            else
            {
                GetWayPoint(geAI);
            }

            if(timer > waitTime)
            {
                geAI.SwitchState(geAI.wanderState);
            }
        }
    }

    private void GetWayPoint(GEntityAI geAI)
    {
        if (reAlert)
        {
            lastSeenPlayerPos = geAI.playerRef.transform.position;
            reAlert = false;
        }

        float z = Random.Range(-rangeOfSearch, rangeOfSearch);
        float x = Random.Range(-rangeOfSearch, rangeOfSearch);
        destPoint = new Vector3(lastSeenPlayerPos.x + x, 0, lastSeenPlayerPos.z + z);

        NavMeshHit hit;
        if (NavMesh.SamplePosition(destPoint, out hit, 0.1f, NavMesh.AllAreas))
        {
            walkPointSet = true;
        }
        else
        {
            walkPointSet = false;
        }
    }
}
