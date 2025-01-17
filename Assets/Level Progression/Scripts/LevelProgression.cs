using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class LevelProgression : MonoBehaviour
{
    [System.Serializable]
    public class SafeRoom
    {
        public string roomTag;
        public GameObject[] enableObjects;
        public GameObject[] disableObjects;
        public GateConfig[] gates; // Array of gates with individual configurations
    }

    [System.Serializable]
    public class GateConfig
    {
        public Gates gate; // Reference to the gate script
        public bool open;  // Determines whether to open or close this specific gate
    }

    [Header("Safe Rooms Configuration")]
    public List<SafeRoom> safeRooms;


    private void Start()
    {
        PlayerData playerData = SaveSystem.LoadPlayer();
    }

    private void OnTriggerEnter(Collider other)
    {
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

                // Handle individual gate operations based on their configuration
                foreach (GateConfig gateConfig in room.gates)
                {
                    if (gateConfig.gate != null)
                    {
                        if (gateConfig.open)
                        {
                            gateConfig.gate.OpenDoors(); // Call the Open method of the Gates script
                        }
                        else
                        {
                            gateConfig.gate.CloseDoors(); // Call the Close method of the Gates script
                        }
                    }
                }

                // Save player data
                
                SaveLoadData.saveDatainstance.SavePlayer();
                Debug.Log("Player Data Saved");

                // Exit the loop once the correct room is processed
                break;
            }
        }
    }
}


