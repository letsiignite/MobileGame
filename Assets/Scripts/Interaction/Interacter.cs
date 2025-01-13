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
                }
                else
                {
                    Debug.Log("Pass 1-1");
                    Vector3 touchPosWorld = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);

                    //Debugging Tech
                    debugRay.origin = touchPosWorld;
                    debugRay.direction = Camera.main.transform.forward;

                    RaycastHit hitInformation;
                    if (Physics.Raycast(touchPosWorld, Camera.main.transform.forward, out hitInformation)) 
                    {
                        
                        if (hitInformation.collider.gameObject.GetComponent<IBaseInteractableObject>() != null)
                        {
                            Debug.Log("Pass 1");
                            hitInformation.collider.gameObject.GetComponent<IBaseInteractableObject>().HandlePlayerInteraction();
                        }
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
