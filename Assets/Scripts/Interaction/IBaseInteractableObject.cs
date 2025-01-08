using UnityEngine;

namespace Interactable
{
    public interface IBaseInteractableObject
    {
        /// <summary>
        /// This method handels what must happen when player is close to this interactable object.
        /// </summary>
        public void OnFocus();

        /// <summary>
        /// This method handels what happens when player taps on this object.
        /// </summary>
        public void HandlePlayerInteraction();
    }
}