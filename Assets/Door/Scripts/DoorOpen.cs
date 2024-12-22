using UnityEngine;
using UnityEngine.UI;

public class DoorOpen : MonoBehaviour
{
    public float DistanceOpen = 3f; // Distance to detect the door
    private GameObject button; // UI Button GameObject
    private Button uiButton; // Button component for interaction
    private DoorScript.Door detectedDoor; // Reference to the detected door

    void Start()
    {
        // Find the button GameObject by tag
        button = GameObject.FindGameObjectWithTag("DoorButton");
        if (button != null)
        {
            uiButton = button.GetComponent<Button>();
            if (uiButton != null)
            {
                uiButton.onClick.AddListener(OpenDoor); // Add OpenDoor as a listener for button clicks
            }

            // Initially, hide the button
            button.SetActive(false);
        }
        else
        {
            Debug.LogWarning("No GameObject with tag 'DoorButton' found!");
        }
    }

    void Update()
    {
        RaycastHit hit;

        // Cast a ray forward from the player
        if (Physics.Raycast(transform.position, transform.forward, out hit, DistanceOpen))
        {
            // Check if the hit object has a Door component
            DoorScript.Door door = hit.transform.GetComponent<DoorScript.Door>();
            if (door != null)
            {
                detectedDoor = door; // Keep a reference to the detected door
                if (button != null)
                {
                    button.SetActive(true); // Show the UI button
                }

                // Open the door if the 'E' key is pressed
                if (Input.GetKeyDown(KeyCode.E))
                {
                    detectedDoor.OpenDoor();
                }
            }
            else
            {
                HideButton();
            }
        }
        else
        {
            HideButton();
        }
    }

    // Method called when the button is pressed
    void OpenDoor()
    {
        if (detectedDoor != null)
        {
            detectedDoor.OpenDoor();
        }
    }

    // Hides the button and clears the door reference
    void HideButton()
    {
        if (button != null)
        {
            button.SetActive(false);
        }
        detectedDoor = null;
    }

}
