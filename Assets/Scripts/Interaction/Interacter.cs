//Gaurav's Code (new Input system try, not functional)
//Modified by Shrey (now functional fully using new input system)using Interactable;
using Interactable;
using UI;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interacter : MonoBehaviour
{
    [SerializeField] private LayerMask interactLayer;
    [SerializeField] private LayerMask uiLayer;
    [SerializeField] private UIRaycast checkUI;

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
            Ray interactRay = mainCamera.ScreenPointToRay(touchPos);
            int layersForInteraction = uiLayer.value | interactLayer.value;

            if (Physics.Raycast(interactRay, out RaycastHit hitInformation, 5f, layersForInteraction))
            {
              
                var interactable = hitInformation.collider.GetComponent<IBaseInteractableObject>();
                if (interactable != null)
                {
                    interactable.HandlePlayerInteraction();
                    return;
                }

             
                var door = hitInformation.collider.gameObject.GetComponent<DoorCtrl>();
                if (door != null)
                {
                    //Debug.Log("Toggling Door Animation");
                    door.ToggleDoor(); // Toggle the door using its own state
                }

                var mainMenuInfo = hitInformation.collider.gameObject.GetComponent<MainMenuButtons>();
                if(mainMenuInfo != null)
                {
                    Debug.Log("In mainMenu buttons flow");
                    mainMenuInfo.StartAnimateButtonPress(mainMenuInfo.gameObject);
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
        if (mainCamera != null)
        {
            Ray interactRay = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            Gizmos.color = Color.red;
            Gizmos.DrawRay(interactRay.origin, interactRay.direction.normalized * 5f);
        }
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







































