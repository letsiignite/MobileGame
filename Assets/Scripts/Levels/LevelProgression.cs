using UnityEngine;

public class LevelProgression : MonoBehaviour
{
   
    private GameObject gate;

    private void Start()
    {
        gate = GameObject.FindGameObjectWithTag("Gate");

        if (gate == null)
        {
            Debug.LogError("Door GameObject with tag 'Door' not found!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SafeRoom"))
        {
            SaveLoadData.saveDatainstance.SavePlayer();
            Debug.Log("Player Data Saved");

            // Turn Gate Collider IsTrigger = true
            OpenGate();
        }
    }

    void OpenGate() 
    {
        if (gate != null)
        {
            BoxCollider doorCollider = gate.GetComponent<BoxCollider>();
            if (doorCollider != null)
            {
                doorCollider.isTrigger = true;
                Debug.Log("Door collider is now set to trigger!");
            }
            else
            {
                Debug.LogError("BoxCollider not found on the door GameObject!");
            }
        }
    }

    
}
