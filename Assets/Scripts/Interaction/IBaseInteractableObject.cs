using UnityEngine;

namespace Interactable
{
    public interface IBaseInteractableObject
    {
        /// <summary>
        /// This method handles what must happen when player is close to this interactable object.
        /// </summary>
        public void OnFocus();

        /// <summary>
        /// This method handles what happens when player taps on this object.
        /// </summary>
        public void HandlePlayerInteraction();
    }
}