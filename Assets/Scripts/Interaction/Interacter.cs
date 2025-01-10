using Interactable;
using UnityEngine;
using UnityEngine.EventSystems;

public class Interacter : MonoBehaviour
{
    private bool cloaseToInteractableObject = false;
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
                    Vector3 touchPosWorld = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);

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
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<IBaseInteractableObject>() != null)
        {
            cloaseToInteractableObject = true;
        }
    }
}
