using System;
using UnityEngine;

public class CheckPoints : MonoBehaviour
{
    public static CheckPoints Instance;
    [SerializeField] private GameObject checkPoint;
    public GameObject[] checkpointsArray;
    private Vector3 startingPoint;
    public string[] SafeRooms; // Changed to an array of strings

    private void Awake()
    {
        LoadCheckPoints();
    }

    void Start()
    {
        Instance = this;

        PlayerData playerData = SaveSystem.LoadPlayer();

        if (playerData != null && !string.IsNullOrEmpty(playerData.checkpointName))
        {
            // Find the saved checkpoint by name
            GameObject savedCheckpoint = Array.Find(checkpointsArray, cp => cp.name == playerData.checkpointName);

            if (savedCheckpoint != null)
            {
                startingPoint = savedCheckpoint.transform.position; // Set starting point to saved checkpoint
                Debug.Log($"Respawning at checkpoint: {savedCheckpoint.name}, Position: {startingPoint}");
                RespawnPlayer();
            }
            else
            {
                Debug.LogError("Saved checkpoint not found in the array. Respawning at start position.");
                startingPoint = gameObject.transform.position;
            }
        }
        else
        {
            Debug.Log("No saved data found. Respawning at start position.");
            startingPoint = gameObject.transform.position;
        }
    }

    public void RespawnPlayer()
    {
        gameObject.transform.position = startingPoint;
    }

    private void LoadCheckPoints()
    {
        checkpointsArray = new GameObject[checkPoint.transform.childCount];
        int index = 0;

        foreach (Transform singleCheckpoint in checkPoint.transform)
        {
            checkpointsArray[index] = singleCheckpoint.gameObject;
            index++;
        }

        Debug.Log($"Loaded {checkpointsArray.Length} checkpoints.");
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the tag of the colliding object matches any tag in the SafeRooms array
        if (Array.Exists(SafeRooms, tag => other.CompareTag(tag)))
        {
            int checkpointIndex = Array.FindIndex(checkpointsArray, match => match == other.gameObject);

            if (checkpointIndex != -1)
            {
                string checkpointName = other.gameObject.name;

                // Save the checkpoint data
                SaveSystem.SavePlayer(new SaveLoadData()
                {
                    sanityMeter = 100, // Replace with actual sanityMeter
                    timeTaken = 0f,    // Replace with actual time taken
                    checkPointName = checkpointName
                });

                Debug.Log($"Checkpoint saved: {checkpointName}");

                // Update the starting point
                startingPoint = other.gameObject.transform.position;
            }
            else
            {
                Debug.LogWarning("Checkpoint not found in array.");
            }
        }
    }

}
