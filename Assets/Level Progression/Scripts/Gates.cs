using UnityEngine;

public class Gates : MonoBehaviour
{
    public static Gates Instance;
    [Header("Door Settings")]
    public Transform leftDoor;   // Assign the left door Transform
    public Transform rightDoor;  // Assign the right door Transform
    public float openAngle = 90f; // Angle to open the door
    public float openSpeed = 2f;  // Speed of the door opening/closing

    private Quaternion leftDoorClosedRotation;
    private Quaternion leftDoorOpenRotation;
    private Quaternion rightDoorClosedRotation;
    private Quaternion rightDoorOpenRotation;

    private bool isOpen = false; // Track whether the doors are open
    private bool isAnimating = false; // Prevent multiple animations simultaneously

    private void Start()
    {
        Instance = this;
        // Store the initial rotations as closed rotations
        leftDoorClosedRotation = leftDoor.rotation;
        rightDoorClosedRotation = rightDoor.rotation;

        // Calculate the open rotations
        leftDoorOpenRotation = leftDoorClosedRotation * Quaternion.Euler(0, -openAngle, 0);
        rightDoorOpenRotation = rightDoorClosedRotation * Quaternion.Euler(0, openAngle, 0);
    }

    public void OpenDoors()
    {
        if (!isAnimating && !isOpen)
        {
            StartCoroutine(AnimateDoors(true));
        }
    }

    public void CloseDoors()
    {
        if (!isAnimating && isOpen)
        {
            StartCoroutine(AnimateDoors(false));
        }
    }

    private System.Collections.IEnumerator AnimateDoors(bool opening)
    {
        isAnimating = true;

        // Choose target rotations based on the opening or closing state
        Quaternion targetLeftRotation = opening ? leftDoorOpenRotation : leftDoorClosedRotation;
        Quaternion targetRightRotation = opening ? rightDoorOpenRotation : rightDoorClosedRotation;

        // Smoothly rotate doors
        float timeElapsed = 0f;
        while (timeElapsed < 1f)
        {
            timeElapsed += Time.deltaTime * openSpeed;
            leftDoor.rotation = Quaternion.Slerp(leftDoor.rotation, targetLeftRotation, timeElapsed);
            rightDoor.rotation = Quaternion.Slerp(rightDoor.rotation, targetRightRotation, timeElapsed);
            yield return null;
        }

        // Ensure final rotation is exact
        leftDoor.rotation = targetLeftRotation;
        rightDoor.rotation = targetRightRotation;

        // Toggle the state
        isOpen = opening;
        isAnimating = false;
    }

}
