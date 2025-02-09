
////Gaurav's Code (new Input system try not functional)
//using Interactable;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.EventSystems;
//using UnityEngine.InputSystem;
//using UnityEngine.InputSystem.EnhancedTouch;
//using UnityEngine.UI;

//public class Interacter : MonoBehaviour
//{
//    private bool closetointeractableobject = false;
//    private Ray debugray;
//    private Camera mainCamera;
//    private InputAction touchInput;

//    private void Awake()
//    {
//        mainCamera = Camera.main;

//        // Initialize the input action
//        touchInput = new InputAction("Touch", binding: "<Touchscreen>/primaryTouch/position");
//        touchInput.performed += ctx => HandleTouch(ctx);
//        touchInput.Enable();
//    }

//    private void HandleTouch(InputAction.CallbackContext ctx)
//    {
//        Vector2 touchPos = ctx.ReadValue<Vector2>();

//        // Check if the touch is over a UI element
//        if (EventSystem.current.IsPointerOverGameObject(Touchscreen.current.primaryTouch.touchId.ReadValue()))
//        {
//            return; // Ignore touch if over UI
//        }

//        Vector3 touchposworld = mainCamera.ScreenToWorldPoint(new Vector3(touchPos.x, touchPos.y, mainCamera.nearClipPlane));

//        // Debugging visualization
//        debugray.origin = touchposworld;
//        debugray.direction = mainCamera.transform.forward;

//        if (Physics.Raycast(touchposworld, mainCamera.transform.forward, out RaycastHit hitInformation))
//        {
//            IBaseInteractableObject interactable = hitInformation.collider.gameObject.GetComponent<IBaseInteractableObject>();
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
//            closetointeractableobject = true;
//        }
//    }

//    private void OnDrawGizmos()
//    {
//        Gizmos.DrawRay(debugray);
//    }

//    private void OnDestroy()
//    {
//        touchInput.Disable();
//        touchInput.performed -= HandleTouch;
//    }
//}



































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







































