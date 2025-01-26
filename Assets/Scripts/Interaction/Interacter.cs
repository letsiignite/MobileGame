using Interactable;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Interacter : MonoBehaviour
{
    [SerializeField] private LayerMask item;
    private bool closeToInteractableObject = false;
    private Ray interactRay;

    public GraphicRaycaster raycaster;
    public EventSystem eventSystem;

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
                if (touch.phase == TouchPhase.Began && !EventSystem.current.IsPointerOverGameObject(id))
                {
                    interactRay = Camera.main.ScreenPointToRay(touch.position);

                    RaycastHit hitInformation;
                    if (Physics.Raycast(interactRay, out hitInformation, 5f, item.value))
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

    private bool IsPointerOverUI(Vector2 position)
    {
        PointerEventData eventData = new PointerEventData(eventSystem);
        eventData.position = position;

        List<RaycastResult> results = new List<RaycastResult>();
        raycaster.Raycast(eventData, results);

        return results.Count > 0;
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
        Gizmos.DrawRay(interactRay.origin, interactRay.direction.normalized * 5f);

        Gizmos.color = Color.red;
        Gizmos.DrawRay(interactRay);
    }
}
