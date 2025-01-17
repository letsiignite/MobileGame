using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DoorScript
{
    [RequireComponent(typeof(AudioSource))]
    public class DoorInteract : MonoBehaviour, IInteractable
    {
        public bool open;
        public float smooth = 1.0f;
        public float DoorOpenAngle = -90.0f;
        public float DoorCloseAngle = 0.0f;
        public AudioSource asource;
        public AudioClip openDoor, closeDoor;

        // Use this for initialization
        void Start()
        {
            asource = GetComponent<AudioSource>();
        }

        public void Interact()
        {
            // Toggle door state
            open = !open;

            // Play appropriate sound
            asource.clip = open ? openDoor : closeDoor;
            asource.Play();

            
            // Rotate the door to the target angle
            StartCoroutine(RotateDoor());
        }

        // Coroutine for smooth door rotation
        private IEnumerator RotateDoor()
        {
            float elapsedTime = 0f;
            float duration = 0.75f / smooth; // Adjust duration based on smooth factor

            Quaternion startRotation = transform.localRotation;
            Quaternion targetRotation = Quaternion.Euler(0, open ? DoorOpenAngle : DoorCloseAngle, 0);

            while (elapsedTime < duration)
            {
                transform.localRotation = Quaternion.Slerp(startRotation, targetRotation, elapsedTime / duration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            transform.localRotation = targetRotation; // Ensure exact rotation at the end
        }

        
    }
}