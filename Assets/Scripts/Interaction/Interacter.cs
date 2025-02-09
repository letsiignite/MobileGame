using Interactable;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;

public class Interacter : MonoBehaviour
{
    private bool closetointeractableobject = false;
    private Ray debugray;
    private Camera mainCamera;
    private InputAction touchInput;

    private void Awake()
    {
        mainCamera = Camera.main;

        // Initialize the input action
        touchInput = new InputAction("Touch", binding: "<Touchscreen>/primaryTouch/position");
        touchInput.performed += ctx => HandleTouch(ctx);
        touchInput.Enable();
    }

    private void HandleTouch(InputAction.CallbackContext ctx)
    {
        Vector2 touchPos = ctx.ReadValue<Vector2>();

        // Check if the touch is over a UI element
        if (EventSystem.current.IsPointerOverGameObject(Touchscreen.current.primaryTouch.touchId.ReadValue()))
        {
            return; // Ignore touch if over UI
        }

        Vector3 touchposworld = mainCamera.ScreenToWorldPoint(new Vector3(touchPos.x, touchPos.y, mainCamera.nearClipPlane));

        // Debugging visualization
        debugray.origin = touchposworld;
        debugray.direction = mainCamera.transform.forward;

        if (Physics.Raycast(touchposworld, mainCamera.transform.forward, out RaycastHit hitInformation))
        {
            IBaseInteractableObject interactable = hitInformation.collider.gameObject.GetComponent<IBaseInteractableObject>();
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
            closetointeractableobject = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(debugray);
    }

    private void OnDestroy()
    {
        touchInput.Disable();
        touchInput.performed -= HandleTouch;
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







































