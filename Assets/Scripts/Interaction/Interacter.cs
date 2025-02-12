//Gaurav's Code (new Input system try not functional)
//Modified by Shrey (now functional fully using new input system)
using Interactable;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interacter : MonoBehaviour
{
    [SerializeField] private LayerMask interactLayer;
    [SerializeField] private UIRaycast checkUI;

    private Ray interactRay;
    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void HandleTouch()
    { 
        Vector2 touchPos = Touchscreen.current.primaryTouch.position.ReadValue();
        if (!checkUI.IsPointerOverUI(touchPos))
        {
            interactRay = mainCamera.ScreenPointToRay(touchPos);

            RaycastHit hitInformation;
            if (Physics.Raycast(interactRay, out hitInformation, 5f, interactLayer.value))
            {
                if (hitInformation.collider.gameObject.GetComponent<IBaseInteractableObject>() != null)
                {
                    hitInformation.collider.gameObject.GetComponent<IBaseInteractableObject>().HandlePlayerInteraction();
                }
            }
        }
    }

    private void Update()
    {
        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed)
        {
            HandleTouch();
        }
    }

   

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(interactRay.origin, interactRay.direction.normalized * 5f);
    }
}



































//Shrey's Code (Old Input system) 

//using Interactable;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.EventSystems;
//using UnityEngine.UI;

//public class Interacter : MonoBehaviour
//{
//    [SerializeField] private LayerMask item;
//    private bool closeToInteractableObject = false;
//    private Ray interactRay;

//    public GraphicRaycaster raycaster;
//    public EventSystem eventSystem;

//    // Start is called once before the first execution of Update after the MonoBehaviour is created
//    void Start()
//    {

//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (Input.touchCount > 0)
//        {
//            foreach (Touch touch in Input.touches)
//            {
//                int id = touch.fingerId;
//                if (touch.phase == TouchPhase.Began && !EventSystem.current.IsPointerOverGameObject(id))
//                {
//                    interactRay = Camera.main.ScreenPointToRay(touch.position);

//                    RaycastHit hitInformation;
//                    if (Physics.Raycast(interactRay, out hitInformation, 5f, item.value))
//                    {
//                        if (hitInformation.collider.gameObject.GetComponent<IBaseInteractableObject>() != null)
//                        {
//                            hitInformation.collider.gameObject.GetComponent<IBaseInteractableObject>().HandlePlayerInteraction();
//                        }
//                    }
//                }
//            }
//        }
//    }

//    private bool IsPointerOverUI(Vector2 position)
//    {
//        PointerEventData eventData = new PointerEventData(eventSystem);
//        eventData.position = position;

//        List<RaycastResult> results = new List<RaycastResult>();
//        raycaster.Raycast(eventData, results);

//        return results.Count > 0;
//    }

//    private void OnTriggerEnter(Collider other)
//    {
//        if (other.gameObject.GetComponent<IBaseInteractableObject>() != null)
//        {
//            closeToInteractableObject = true;
//        }
//    }

//    private void OnDrawGizmos()
//    {
//        Gizmos.DrawRay(interactRay.origin, interactRay.direction.normalized * 5f);

//        Gizmos.color = Color.red;
//        Gizmos.DrawRay(interactRay);
//    }
//}







































