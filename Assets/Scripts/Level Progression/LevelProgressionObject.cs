using Game;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace LevelProgression
{
    public class LevelProgressionObject : MonoBehaviour
    {
        [System.Serializable]
        internal class GateData
        {
            public Gates gateScript; // Reference to the gate script
            public bool gateOpen = false;
        }

        [Header("Safe Area")]
        [SerializeField] private GameObject[] enableObjects;
        [SerializeField] private GameObject[] disableObjects;

        [Header("Gates")]
        [SerializeField] private List<GateData> gates = new List<GateData>();
        [SerializeField] private bool gateOpened;

        [Header("Ghost")]
        [SerializeField] private Transform ghostWarpPosition;
        [SerializeField] private GameObject ghostRef;

        [Header("Player Data")]
        [SerializeField] private string checkPointName;

        public void HandleGates()
        {
            if (gateOpened)
            {
                foreach (GateData gate in gates)
                {
                    if (gate.gateScript != null)
                    {
                        if (gate.gateOpen)
                        {
                            gate.gateScript.CloseDoors();
                        }
                        else
                        {
                            gate.gateScript.OpenDoors();
                        }
                    }
                }
                TransportGhost();
            }
        }

        private void TransportGhost()
        {
            if (ghostWarpPosition != null)
            {
                if (ghostRef != null)
                {
                    ghostRef.GetComponent<NavMeshAgent>().ResetPath();
                    ghostRef.GetComponent<NavMeshAgent>().Warp(ghostWarpPosition.position);
                    ghostRef.transform.rotation = ghostWarpPosition.rotation;
                }
            }
        }

        private void ToggleObjects()
        {
            Debug.Log("Pass - 2");
            foreach (GameObject obj in enableObjects)
            {
                if (obj != null)
                {
                    Debug.Log(obj.name + " Enabled");
                    obj.SetActive(true);
                }
            }

            foreach (GameObject obj in disableObjects)
            {
                if (obj != null)
                {
                    Debug.Log(obj.name + " Disabled");
                    obj.SetActive(false);
                }
            }
        }

        public void LevelCompleted()
        {
            gateOpened = true;
            ToggleObjects();
            HandleGates();
            Debug.Log("Pass - 1");
            SaveLoadData currData = GameManager._instance.GetPlayerReference().GetComponent<SaveLoadData>();
            currData.checkPointName = checkPointName;
            SaveSystem.SavePlayer(currData);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                LevelCompleted();
            }
        }
    }
}