using Interactable;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Interacter : MonoBehaviour
{
    private bool closeToInteractableObject = false;
    private Ray debugRay;
    private Camera mainCamera;
    private PlayerInput playerInput;

    private void Awake()
    {
        mainCamera = Camera.main;
        playerInput = new PlayerInput();
        //playerInput.Touch.TouchPress.performed += ctx => HandleTouch(ctx);
    }

    private void OnEnable()
    {
        //playerInput.Enable();
    }

    private void OnDisable()
    {
        //playerInput.Disable();
    }

    private void HandleTouch(InputAction.CallbackContext context)
    {
        if (Touchscreen.current == null || Touchscreen.current.primaryTouch.press.isPressed == false)
            return;

        Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
        if (EventSystem.current.IsPointerOverGameObject())
            return; // Ignore UI touches

        Vector3 touchPosWorld = mainCamera.ScreenToWorldPoint(new Vector3(touchPosition.x, touchPosition.y, mainCamera.nearClipPlane));
        debugRay.origin = touchPosWorld;
        debugRay.direction = mainCamera.transform.forward;

        if (Physics.Raycast(touchPosWorld, mainCamera.transform.forward, out RaycastHit hitInfo))
        {
            var interactable = hitInfo.collider.GetComponent<IBaseInteractableObject>();
            if (interactable != null)
            {
                interactable.HandlePlayerInteraction();
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








































//using Interactable;
//using UnityEngine;
//using UnityEngine.EventSystems;

//public class Interacter : MonoBehaviour
//{
//    private bool closeToInteractableObject = false;
//    private Ray debugRay;

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
//                if (EventSystem.current.IsPointerOverGameObject(id))
//                {
//                    // finger over UI
//                }
//                else
//                {
//                    //Debug.Log("Pass 1-1");
//                    Vector3 touchPosWorld = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);

//                    //Debugging Tech
//                    debugRay.origin = touchPosWorld;
//                    debugRay.direction = Camera.main.transform.forward;

//                    RaycastHit hitInformation;
//                    if (Physics.Raycast(touchPosWorld, Camera.main.transform.forward, out hitInformation)) 
//                    {

//                        if (hitInformation.collider.gameObject.GetComponent<IBaseInteractableObject>() != null)
//                        {
//                            //Debug.Log("Pass 1");
//                            hitInformation.collider.gameObject.GetComponent<IBaseInteractableObject>().HandlePlayerInteraction();
//                        }
//                    }
//                }
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
