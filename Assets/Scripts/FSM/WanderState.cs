using UnityEngine;
using UnityEngine.AI;

public class WanderState : GEState
{
    private float rangeOfSearch = 10;
    private bool walkPointSet;
    private Vector3 destPoint;

    public override void EnterState(GEntityAI geAI)
    {
        //data change related like changing speed and other attributes.
        name = "Wander";
    }

    public override void UpdateState(GEntityAI geAI)
    {
        //detecting villagers and player and changing state if detected
        if (geAI.los.visibleEnemy.Contains(geAI.playerRef)) 
        {
            //Switch To Alert State
        }
        else if(geAI.los.visibleEnemy.Count != 0)
        {
            //Infect others
        }
        else
        {
            if (walkPointSet)
            {
                geAI.agent.SetDestination(destPoint);
                Debug.Log(Vector3.Distance(geAI.agent.transform.position, geAI.agent.destination) + " : " + geAI.agent.transform.position + ", " + geAI.agent.destination + ", " + destPoint);
                if (Vector3.Distance(geAI.agent.transform.position, geAI.agent.destination) <= geAI.agent.stoppingDistance)
                {
                    Debug.Log("Passed 2");
                    walkPointSet = false;
                }
            }
            else
            {
                GetWayPoint(geAI);
            }
        }
    }

    private void GetWayPoint(GEntityAI geAI)
    {
        float z = Random.Range(-rangeOfSearch, rangeOfSearch);
        float x = Random.Range(-rangeOfSearch, rangeOfSearch);
        destPoint = new Vector3(geAI.transform.position.x + x, 0, geAI.transform.position.z + z);

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
