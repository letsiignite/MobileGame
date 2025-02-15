using UnityEngine;
using UnityEngine.UI;

interface IInteractable
{
    void Interact();
}

public class PlayerInteraction : MonoBehaviour
{
    public Transform interactorSource;  // The source position for the ray
    public float interactRange = 5f;    // Range of interaction
    public GameObject interactButton;  // Reference to the interact button GameObject
   

    private IInteractable currentInteractable;  // Stores the currently interactable object

    void Start()
    {
        // Ensure the button and hint start as hidden
        interactButton.SetActive(false);
        
    }

    void Update()
    {
        Ray ray = new Ray(interactorSource.position, interactorSource.forward);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, interactRange))
        {
            // Check if the object hit by the ray is interactable
            if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObject))
            {
                currentInteractable = interactObject;

                
                interactButton.SetActive(true);

                // Handle "E" key interaction
                if (Input.GetKeyDown(KeyCode.E))
                {
                    interactObject.Interact();
                }
                return;
            }
        }

        // Hide the button and hint if no interactable object is hit
        currentInteractable = null;
        //interactHint.gameObject.SetActive(false);
        interactButton.SetActive(false);
    }

    public void OnInteractButtonPressed()
    {
        // Trigger interaction if there's a valid interactable
        currentInteractable?.Interact();
    }
}
