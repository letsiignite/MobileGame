using UnityEngine;
using UnityEngine.AI;

public class AlertState : GEState
{
    private Vector3 destPoint;
    private Vector3 lastSeenPlayerPos;
    private float rangeOfSearch = 5f;
    private bool walkPointSet;
    private float timer = 0f;

    public bool reAlert;

    public override void EnterState(GEntityAI geAI)
    {
        name = "Alert";
        lastSeenPlayerPos = geAI.playerRef.transform.position;
    }

    public override void UpdateState(GEntityAI geAI)
    {
        timer += Time.deltaTime;

        //player in range and timer within search time.

        if (geAI.playerIsNearby && timer <= geAI.alertTime)
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

        //player in sight and timer is within search time

        else if (geAI.los.visibleEnemy.Contains(geAI.playerRef) && timer <= geAI.alertTime)
        {
            timer = 0f;
            geAI.SwitchState(geAI.chaseState);
        }

        //if player escaped the entity and not in range within 

        else if (!(geAI.los.visibleEnemy.Contains(geAI.playerRef) || geAI.playerIsNearby) && timer > geAI.alertTime && Vector3.Distance(geAI.agent.transform.position, geAI.agent.destination) <= geAI.agent.stoppingDistance)
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

            if(timer > geAI.alertTime)
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
