using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SafeRoom
{
    public string roomTag;
    public GameObject[] enableObjects;
    public GameObject[] disableObjects;
}

[System.Serializable]
public class Gate
{
    public Gates gateScript; // Reference to the GateScript
    public bool gateOpen = false; // Determines if this specific gate is open
}

public class LevelProgression : MonoBehaviour
{
    [Header("Safe Rooms Configuration")]
    public List<SafeRoom> safeRooms; // List of SafeRooms

    [Header("Gate Settings")]
    public Gate[] level1Gates; // Gates for Level 1
    public Gate[] level2Gates; // Gates for Level 2
    public Gate[] level3Gates; // Gates for Level 3
    public Gate[] level4Gates; // Gates for Level 4
    public Gate[] level5Gates; // Gates for Level 5
    public Gate[] FinalGates; // Gates for Level 5
    public Gates End1Gates;
    public Gates End2Gates;

    [Header("Level Win Status")]
    public bool level1Win = false; // Tracks if Level 1 is won
    public bool level2Win = false; // Tracks if Level 2 is won
    public bool level3Win = false; // Tracks if Level 3 is won
    public bool level4Win = false; // Tracks if Level 4 is won
    public bool level5Win = false; // Tracks if Level 5 is won
    public bool allLevelWin = false; 

    [Header("Ghost Settings")]
    public GameObject ghost; // The ghost (enemy) GameObject
    public Transform level2Destination; // Destination for Level 2
    public Transform level3Destination; // Destination for Level 3
    public Transform level4Destination; // Destination for Level 4
    public Transform level5Destination; // Destination for Level 5

    private void Start()
    {
        PlayerData playerData = SaveSystem.LoadPlayer();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SafeRoom1"))
        {
            Level1Completed();
            Debug.Log("Triggered");
        }

        if (other.CompareTag("SafeRoom4"))
        {
            End1Gates.CloseDoors();
            Debug.Log("Triggered");
        }

        if (other.CompareTag("SafeRoom6"))
        {
            End2Gates.CloseDoors();
            Debug.Log("Triggered");
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
                        obj.SetActive(true);
                }

                // Disable all objects in the disableObjects array
                foreach (GameObject obj in room.disableObjects)
                {
                    if (obj != null)
                        obj.SetActive(false);
                }

                // Save player data
                SaveLoadData.saveDatainstance.SavePlayer();
                Debug.Log("Player Data Saved");

                // Exit the loop once the correct room is processed
                break;
            }
        }

        
    }

    private void Update()
    {
        //// Check for level win status and handle gates and ghost logic
        //if (level1Win)
        //{
        //    HandleGates(level1Gates);
        //    TransportGhostToDestination(level2Destination);
        //}
        //if (level2Win)
        //{
        //    HandleGates(level2Gates);
        //    TransportGhostToDestination(level3Destination);
        //}
        //if (level3Win)
        //{
        //    HandleGates(level3Gates);
        //    TransportGhostToDestination(level4Destination);
        //}
        //if (level4Win)
        //{
        //    HandleGates(level4Gates);
        //    TransportGhostToDestination(level5Destination);
        //}
        //if (level5Win)
        //{
        //    HandleGates(level5Gates);
        //}

        //if (allLevelWin)
        //{
        //    HandleGates(FinalGates);
        //}
    }

    public void Level1Completed() 
    {
        HandleGates(level1Gates);
        TransportGhostToDestination(level2Destination);
        level1Win = true;
    }

    public void Level2Completed() 
    {
        HandleGates(level2Gates);
        TransportGhostToDestination(level3Destination);
        level2Win = true;
    }

    public void Level3Completed() 
    {
        HandleGates(level3Gates);
        TransportGhostToDestination(level4Destination);
        level3Win = true;
    }

    public void Level4Completed() 
    {
        HandleGates(level4Gates);
        TransportGhostToDestination(level5Destination);
        level4Win = true;
    }

    public void Level5Completed() 
    {
        HandleGates(level5Gates);
    }

    public void AllLevelCompleted()
    {
        HandleGates(FinalGates);
    }

    private void HandleGates(Gate[] gates)
    {
        // Loop through all gates and open/close based on their state
        foreach (Gate gate in gates)
        {
            if (gate.gateScript != null)
            {
                if (gate.gateOpen)
                {
                    gate.gateScript.OpenDoors(); // Open the gate
                }
                else
                {
                    gate.gateScript.CloseDoors(); // Close the gate
                }
            }
            else
            {
                Debug.LogWarning("GateScript is not assigned for one of the gates.");
            }
        }
    }

    private void TransportGhostToDestination(Transform destination)
    {
        if (ghost != null && destination != null)
        {
            ghost.transform.position = destination.position;
            ghost.transform.rotation = destination.rotation;
            Debug.Log("Ghost transported to the destination point.");
        }
        else
        {
            Debug.LogWarning("Ghost or destination is not assigned.");
        }
    }
}