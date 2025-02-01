using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GateData
{
    public Gates gateScript; // Reference to the gate script
    public bool gateOpen = false;
}

[System.Serializable]
public class LevelData
{
    public string roomName; // Name of the GameObject (e.g., "SafeRoom1")
    public GameObject[] enableObjects = new GameObject[0]; // Default empty array to avoid null
    public GameObject[] disableObjects = new GameObject[0]; // Default empty array to avoid null
    public bool gateOpened = false;
    public List<GateData> gates = new List<GateData>();
    public Transform ghostDestination;

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
                        gate.gateScript.OpenDoors();
                    }
                    else
                    {
                        gate.gateScript.CloseDoors();
                    }
                }
                else
                {
                    Debug.LogWarning("GateScript is not assigned for a gate.");
                }
            }
            TransportGhost();
        }
    }

    private void TransportGhost()
    {
        if (ghostDestination != null)
        {
            GameObject ghost = GameObject.FindGameObjectWithTag("Ghost");
            if (ghost != null)
            {
                ghost.transform.position = ghostDestination.position;
                ghost.transform.rotation = ghostDestination.rotation;
                Debug.Log("Ghost transported to the destination point.");
            }
            else
            {
                Debug.LogWarning("Ghost not found in the scene.");
            }
        }
        else
        {
            Debug.LogWarning("Ghost destination is not assigned.");
        }
    }
}

public class LevelProgression : MonoBehaviour
{
    [Header("Level Configuration")]
    public List<LevelData> levels;

    private void Start()
    {
        if (SaveLoadData.saveDatainstance != null)
        {
            PlayerData playerData = SaveSystem.LoadPlayer();
        }
        else
        {
            Debug.LogWarning("SaveLoadData.saveDatainstance is null.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Use the GameObject's name to find the corresponding LevelData
        string roomName = other.gameObject.name;

        foreach (LevelData level in levels)
        {
            if (level.roomName == roomName)
            {
                ToggleObjects(level);
                // Save player data only if SaveLoadData is not null
                if (SaveLoadData.saveDatainstance != null)
                {
                    SaveLoadData.saveDatainstance.SavePlayer();
                    Debug.Log("Player Data Saved");
                }
                else
                {
                    Debug.LogWarning("SaveLoadData.saveDatainstance is null, cannot save player data.");
                }
                // Exit the loop once the correct room is processed
                break;
            }
        }

        // Handle specific level completions and gate closures
        if (other.CompareTag("SafeRoom"))
        {
            CompleteLevel(0); // Assuming SafeRoom1 corresponds to index 0
        }

        HandleGateClosure(other, "Level4", "Level 4 Gate1");
        HandleGateClosure(other, "Level5", "Level 5 Gate2");
    }

    private void ToggleObjects(LevelData level)
    {
        foreach (GameObject obj in level.enableObjects)
        {
            if (obj != null) obj.SetActive(true);
            else Debug.LogWarning("A GameObject in enableObjects is null.");
        }

        foreach (GameObject obj in level.disableObjects)
        {
            if (obj != null) obj.SetActive(false);
            else Debug.LogWarning("A GameObject in disableObjects is null.");
        }
    }

    private void HandleGateClosure(Collider other, string levelTag, string gateName)
    {
        if (other.CompareTag(levelTag))
        {
            GameObject gate = GameObject.Find(gateName);
            if (gate?.GetComponent<Gates>() is Gates gateScript)
            {
                gateScript.CloseDoors();
                Debug.Log($"{gateName} closed.");
            }
            else
            {
                Debug.LogWarning($"{gateName} not found or missing Gates script.");
            }
        }
    }

    public void CompleteLevel(int levelIndex)
    {
        if (levelIndex >= 0 && levelIndex < levels.Count)
        {
            LevelData level = levels[levelIndex];
            level.gateOpened = true;
            level.HandleGates();
            Debug.Log($"Level {levelIndex} Completed and Ghost Transported.");
        }
        else
        {
            Debug.LogWarning($"Invalid level index: {levelIndex}.");
        }
    }
}