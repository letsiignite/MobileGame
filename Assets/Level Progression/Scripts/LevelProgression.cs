using UnityEngine;

public class LevelProgression : MonoBehaviour
{
    [Header("LevelGates")]
    // References to the Gate GameObjects
    [SerializeField] private GameObject gate1;
    [SerializeField] private GameObject gate2;
    [SerializeField] private GameObject gate3;
    [SerializeField] private GameObject gate4;
    [SerializeField] private GameObject gate5;
    [SerializeField] private GameObject gate6;
    [SerializeField] private GameObject gate7;
    //[SerializeField] private GameObject gate8;

    [Header("LevelTaskGates")]
    [SerializeField] private GameObject level4Gate;
    [SerializeField] private GameObject level5Gate;

    [Header("CheckLevelStatus")]
    // Flags to check if levels are completed
    [SerializeField] private bool isLevel1Completed;
    [SerializeField] private bool isLevel2Completed;
    [SerializeField] private bool isLevel3Completed;
    [SerializeField] private bool isLevel4Completed;
    [SerializeField] private bool isLevel5Completed;
    

    private void Start()
    {
        level4Gate.SetActive(false);
        level5Gate.SetActive(false);
        // Initialize the gates' active states based on level completion
        UpdateGateStates();
    }

    private void Update()
    {
        // Continuously update the gates' states based on level completion
        UpdateGateStates();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Player Data saved after trigger enter
        if (other.CompareTag("SafeRoom"))
        {
            SaveLoadData.saveDatainstance.SavePlayer();
            Debug.Log("Player Data Saved");
        }

        // Check for trigger enter for level 4
        if (other.CompareTag("Level4"))
        {
            level4Gate.SetActive(true);
            other.gameObject.SetActive(false);
        }

        // Check for trigger enter for level 5
        if (other.CompareTag("Level5"))
        {
            level5Gate.SetActive(true);
            other.gameObject.SetActive(false);
        }

    }

    // Call this method when Level 1 is completed
    public void CompleteLevel1()
    {
        isLevel1Completed = true;
    }

    // Call this method when Level 2 is completed
    public void CompleteLevel2()
    {
        isLevel2Completed = true;
    }

    // Call this method when Level 3 is completed
    public void CompleteLevel3()
    {
        isLevel3Completed = true;
    }

    // Call this method when Level 4 is completed
    public void CompleteLevel4()
    {
        isLevel4Completed = true;
        level4Gate.SetActive(false);
    }

    // Call this method when Level 5 is completed
    public void CompleteLevel5()
    {
        isLevel5Completed = true;
        level5Gate.SetActive(false);
    }

    // Update the states of all gates
    private void UpdateGateStates()
    {
        UpdateGateState(gate1, !isLevel1Completed, "Gate1 GameObject is not assigned!");
        UpdateGateState(gate2, isLevel3Completed || !isLevel2Completed && !isLevel1Completed, "Gate2 GameObject is not assigned!");
        UpdateGateState(gate3, !isLevel3Completed && !isLevel2Completed, "Gate3 GameObject is not assigned!");
        UpdateGateState(gate4, !isLevel3Completed && !isLevel2Completed, "Gate4 GameObject is not assigned!");
        // Set gate5 inactive if level 4 is completed
        UpdateGateState(gate5, !isLevel3Completed && !isLevel4Completed, "Gate5 GameObject is not assigned!");

        UpdateGateState(gate6, !isLevel3Completed, "Gate5 GameObject is not assigned!");
        UpdateGateState(gate7, !isLevel5Completed, "Gate5 GameObject is not assigned!");
        //UpdateGateState(gate8, !isLevel5Completed, "Gate5 GameObject is not assigned!");
    }

    // Helper method to update gate state with optional warning
    private void UpdateGateState(GameObject gate, bool state, string warningMessage = "")
    {
        if (gate != null)
        {
            if (gate.activeSelf != state)
            {
                gate.SetActive(state);
            }
        }
        else if (!string.IsNullOrEmpty(warningMessage))
        {
            Debug.LogWarning(warningMessage);
        }
    }

}
