using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace GhostFSM
{
    public class WanderState : GEState
    {
        private float rangeOfSearch = 10f;
        private bool walkPointSet;
        private Vector3 destPoint;
        private GEntityAI geAI;

        public override void EnterState(GEntityAI geAI)
        {
            //data change related like changing speed and other attributes.
            name = "Wander";
            this.geAI = geAI;
            geAI.getNewWayPoint += GetWayPoint;
        }

        public override void UpdateState(GEntityAI geAI)
        {
            //detecting villagers and player and changing state if detected
            if (geAI.los.visibleEnemy.Contains(geAI.playerRef) || geAI.playerIsNearby)
            {
                //Switch To Alert State
                //Debug.Log(geAI.playerIsNearby);
                geAI.SwitchState(geAI.alertState);
            }
            else if (geAI.los.visibleEnemy.Count != 0)
            {
                //Infect others
                geAI.SwitchState(geAI.infectState);
            }
            else
            {
                if (walkPointSet)
                {
                    geAI.agent.SetDestination(destPoint);
                    if (Vector3.Distance(geAI.agent.transform.position, geAI.agent.destination) <= geAI.agent.stoppingDistance || geAI.agent.pathStatus != NavMeshPathStatus.PathComplete)
                    {
                        walkPointSet = false;
                    }
                }
                else
                {
                    geAI.getNewWayPoint.Invoke();
                }
            }
        }

        public void GetWayPoint()
        {
            float z = Random.Range(-rangeOfSearch, rangeOfSearch);
            float x = Random.Range(-rangeOfSearch, rangeOfSearch);
            List<Transform> activeWP = new List<Transform>();
            //destPoint = new Vector3(geAI.transform.position.x + x, 0, geAI.transform.position.z + z);
            for(int i = 0; i < geAI.waypoints.Count; i++)
            {
                if (geAI.waypoints[i].gameObject.activeInHierarchy)
                {
                    activeWP.Add(geAI.waypoints[i]);
                }
            }
            int choice = Random.Range(0, activeWP.Count);
            //Debug.Log(choice);

            destPoint = activeWP[choice].position;

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
}