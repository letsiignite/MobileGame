using Interactable;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Interacter : MonoBehaviour
{
    //private bool closetointeractableobject = false;
    private Ray debugray;

    // start is called once before the first execution of update after the monobehaviour is created
    void Start()
    {

    }

    // update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                int id = touch.fingerId;
                if (EventSystem.current.IsPointerOverGameObject(id))
                {
                    // finger over ui
                }
                else
                {
                    //debug.log("pass 1-1");
                    Vector3 touchposworld = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);

                    //debugging tech
                    debugray.origin = touchposworld;
                    debugray.direction = Camera.main.transform.forward;

                    RaycastHit hitinformation;
                    if (Physics.Raycast(touchposworld, Camera.main.transform.forward, out hitinformation))
                    {

                        if (hitinformation.collider.gameObject.GetComponent<IBaseInteractableObject>() != null)
                        {
                            //debug.log("pass 1");
                            hitinformation.collider.gameObject.GetComponent<IBaseInteractableObject>().HandlePlayerInteraction();
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
            closetointeractableobject = true;
        }
    }

    private void OnDrawGizmos()
    {
       Gizmos.DrawRay(debugray);
    }
}


































//using Interactable;
//using UnityEngine;
//using UnityEngine.EventSystems;
//using UnityEngine.InputSystem;

//public class Interacter : MonoBehaviour
//{
//    private bool closeToInteractableObject = false;
//    private Ray debugRay;
//    private Camera mainCamera;
//    private PlayerInput playerInput;

//    private void Awake()
//    {
//        mainCamera = Camera.main;
//        playerInput = new PlayerInput();
//        //playerInput.Touch.TouchPress.performed += ctx => HandleTouch(ctx);
//    }

//    private void OnEnable()
//    {
//        //playerInput.Enable();
//    }

//    private void OnDisable()
//    {
//        //playerInput.Disable();
//    }

//    private void HandleTouch(InputAction.CallbackContext context)
//    {
//        if (Touchscreen.current == null || Touchscreen.current.primaryTouch.press.isPressed == false)
//            return;

//        Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
//        if (EventSystem.current.IsPointerOverGameObject())
//            return; // Ignore UI touches

//        Vector3 touchPosWorld = mainCamera.ScreenToWorldPoint(new Vector3(touchPosition.x, touchPosition.y, mainCamera.nearClipPlane));
//        debugRay.origin = touchPosWorld;
//        debugRay.direction = mainCamera.transform.forward;

//        if (Physics.Raycast(touchPosWorld, mainCamera.transform.forward, out RaycastHit hitInfo))
//        {
//            var interactable = hitInfo.collider.GetComponent<IBaseInteractableObject>();
//            if (interactable != null)
//            {
//                interactable.HandlePlayerInteraction();
//            }
//        }
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
//        Gizmos.DrawRay(debugRay);
//    }
//}







































