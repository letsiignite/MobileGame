//Gaurav's Code (new Input system try, not functional)
//Modified by Shrey (now functional fully using new input system)using Interactable;
using Game;
using Interactable;
using UI;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interacter : MonoBehaviour
{
    [SerializeField] private LayerMask interactLayer;
    [SerializeField] private UIRaycast checkUI;
    [SerializeField] private AudioClip testClip;

    private Camera mainCamera;
    private Ray interactRay;

    public LayerMask InteractLayer { get => interactLayer; }

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

            if (Physics.Raycast(interactRay, out RaycastHit hitInformation, 5f, interactLayer))
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
                    //GameManager._instance.TriggerCameraShake();
                    GameManager._instance.SubtitleDisplay("door opened, don't angry me, shut up, something just like this", testClip);
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
        //Don't Touch this area. You touch, you gay!!

        Gizmos.color = Color.red;
        Gizmos.DrawRay(interactRay.origin, interactRay.direction.normalized * 5f);
    }
}