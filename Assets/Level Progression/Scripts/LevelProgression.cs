using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SafeRoom
{
    public string roomTag;
    public GameObject[] enableObjects = new GameObject[0]; // Default empty array to avoid null
    public GameObject[] disableObjects = new GameObject[0]; // Default empty array to avoid null
}

[System.Serializable]
public class Gate
{
    public Gates gateScript; // Reference to the GateScript
    public bool gateOpen = false; // Determines if this specific gate is open
}

[System.Serializable]
public class LevelGates
{
    public bool levelWon = false;
    public Transform ghostDestination;
    public Gate[] gates;

    public void HandleGates()
    {
        foreach (Gate gate in gates)
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
                Debug.LogWarning("GateScript is not assigned for one of the gates.");
            }
        }
    }

    public void TransportGhostToDestination(GameObject ghost)
    {
        if (ghost != null && ghostDestination != null)
        {
            ghost.transform.position = ghostDestination.position;
            ghost.transform.rotation = ghostDestination.rotation;
            Debug.Log("Ghost transported to the destination point.");
        }
        else
        {
            Debug.LogWarning("Ghost or destination is not assigned.");
        }
    }

    public bool IsLevelCompleted()
    {
        return levelWon;
    }
}

public class LevelProgression : MonoBehaviour
{
    [Header("Safe Rooms Configuration")]
    public List<SafeRoom> safeRooms; // List of SafeRooms

    [Header("Level Gates")]
    public List<LevelGates> gates;
    public GameObject ghost;

    private void Start()
    {
        // Make sure SaveLoadData is not null
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
        if (other.CompareTag("SafeRoom1"))
        {
            Level1Completed();
        }

        if (other.CompareTag("SafeRoom4"))
        {
            Level4GateClose();
        }

        if (other.CompareTag("SafeRoom6"))
        {
            Level5GateCloses();
        }
        // Iterate through the list of SafeRooms and check for matching tags
        foreach (SafeRoom room in safeRooms)
        {
            

            if (other.CompareTag(room.roomTag))
            {
                // Enable all objects in the enableObjects array
                foreach (GameObject obj in room.enableObjects)
                {
                    if (obj != null)
                    {
                        obj.SetActive(true);
                    }
                    else
                    {
                        Debug.LogWarning("A GameObject in enableObjects is null.");
                    }
                }

                // Disable all objects in the disableObjects array
                foreach (GameObject obj in room.disableObjects)
                {
                    if (obj != null)
                    {
                        obj.SetActive(false);
                    }
                    else
                    {
                        Debug.LogWarning("A GameObject in disableObjects is null.");
                    }
                }

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
    }

    public void CompleteLevel(int levelIndex)
    {
        if (gates.Count > levelIndex) // Ensure there is a LevelGates at the given index
        {
            LevelGates levelGate = gates[levelIndex];
            levelGate.levelWon = true;
            levelGate.HandleGates();
            levelGate.TransportGhostToDestination(ghost);
            Debug.Log($"Level {levelIndex + 1} Completed and Ghost Transported.");
        }
        else
        {
            Debug.LogWarning($"No LevelGates available at index {levelIndex}.");
        }
    }

    public void Level1Completed()
    {
        CompleteLevel(0); // Complete level 1 (index 0)
    }

    public void Level2Completed()
    {
        CompleteLevel(1); // Complete level 2 (index 1)
    }

    public void Level3Completed()
    {
        CompleteLevel(2); // Complete level 3 (index 2)
    }

    public void Level4Completed()
    {
        CompleteLevel(3); // Complete level 4 (index 3)
    }

    public void Level5Completed()
    {
        CompleteLevel(4); // Complete level 5 (index 4)
    }

    public void Level4GateClose()
    {
        CompleteLevel(5); // Complete level 4 gateclose (index 5)
    }

    public void Level5GateCloses()
    {
        CompleteLevel(6); // Complete level 5 GateClose (index 6)
    }
}
