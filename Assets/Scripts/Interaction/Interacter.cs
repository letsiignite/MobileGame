using Interactable;
using UnityEngine;
using UnityEngine.EventSystems;

public class Interacter : MonoBehaviour
{
    private bool closeToInteractableObject = false;
    private Ray debugRay;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                int id = touch.fingerId;
                if (EventSystem.current.IsPointerOverGameObject(id))
                {
                    // finger over UI
                    break;
                }
                Vector3 touchPosWorld = Camera.main.ScreenToWorldPoint(touch.position);

                debugRay.origin = touchPosWorld;
                debugRay.direction = Camera.main.transform.forward;

                RaycastHit hitInformation;
                if (Physics.Raycast(touchPosWorld, Camera.main.transform.forward, out hitInformation)) 
                {
                    if (hitInformation.collider.gameObject.GetComponent<IBaseInteractableObject>() != null)
                    {
                        hitInformation.collider.gameObject.GetComponent<IBaseInteractableObject>().HandlePlayerInteraction();
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<IBaseInteractableObject>() != null)
        {
            closeToInteractableObject = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(debugRay);
    }
}
