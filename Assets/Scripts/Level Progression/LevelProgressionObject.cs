using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LevelProgressionObject : MonoBehaviour
{
    [System.Serializable]
    public partial class GateData
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
        foreach (GameObject obj in enableObjects)
        {
            if (obj != null) obj.SetActive(true);
        }

        foreach (GameObject obj in disableObjects)
        {
            if (obj != null) obj.SetActive(false);
        }
    }

    public void LevelCompleted()
    {
        gateOpened = true;
        HandleGates();
        ToggleObjects();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            LevelCompleted();
        }
    }
}
