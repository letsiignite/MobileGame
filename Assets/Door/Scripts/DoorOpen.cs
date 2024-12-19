using UnityEngine;
using UnityEngine.UI;

public class DoorOpen : MonoBehaviour
{
    public float DistanceOpen = 3f; // Distance to detect the door
    public GameObject button; // UI Button GameObject
    public Button uiButton; // Button component for interaction
    private DoorScript.Door detectedDoor; // Reference to the detected door

    void Start()
    {
        // Get the Button component from the assigned button GameObject
        if (button != null)
        {
            uiButton = button.GetComponent<Button>();
            if (uiButton != null)
            {
                uiButton.onClick.AddListener(OpenDoor); // Add OpenDoor as a listener for button clicks
            }
        }

        // Initially, hide the button
        if (button != null)
        {
            button.SetActive(false);
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
                button.SetActive(true); // Show the UI button

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
