using UnityEngine;

public class DoorCtrl : MonoBehaviour
{
    private Animator animator;
    private bool isOpen = false; // Each door tracks its own state

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void ToggleDoor()
    {
        isOpen = !isOpen; // Flip state only for this door
        animator.SetBool("DoorOpen", isOpen);
    }
}
