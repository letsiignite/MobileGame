using UnityEngine;

namespace Interactable
{
    public class BaseInteractableObject : MonoBehaviour, IBaseInteractableObject
    {
        public string name;
        public void HandlePlayerInteraction()
        {
            // Here we must trigger the action - add to inventory (water, food and collectabels will go to inventory),
            // trigger the hint (if it is a hint object) or trigger the related event or animation, etc.
            if (gameObject.GetComponent<HintObject>() != null)
            { 
            
            }
        }

        public void OnFocus()
        {
            //Activate the outline for the object
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            OnFocus();
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}