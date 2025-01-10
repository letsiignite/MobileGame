using Puzzle;
using UnityEngine;

namespace Interactable
{
    public enum InteractType
    {
        CONSUME_ITEM,
        PICKUP_ITEM,
        PUZZLE_ITEM,
        HINT_ITEM
    }

    public class BaseInteractableObject : MonoBehaviour, IBaseInteractableObject
    {
        [SerializeField] private InteractType interactType;
        [SerializeField] private GameObject playerRef;
        [SerializeField] private int weight = 3;

        public int GetWeight()
        {
            return weight;
        }

        private HintObject hintObject;
        private PuzzleObject puzzleObject;

        public void HandlePlayerInteraction()
        {
            // Here we must trigger the action - add to inventory (water, food and collectables will go to inventory),
            // trigger the hint (if it is a hint object) or trigger the related event or animation, etc.
            if (TryGetComponent<HintObject>(out hintObject))
            {
                hintObject.TriggerHint();
            }
            if (interactType == InteractType.CONSUME_ITEM)
            {
                playerRef.GetComponent<Inventory>().ConsumeItem(gameObject);
            }
            else if (interactType == InteractType.PICKUP_ITEM)
            {
                playerRef.GetComponent<Inventory>().PickUpItem(gameObject);
            }
            else if(interactType == InteractType.PUZZLE_ITEM)
            {
                TryGetComponent<PuzzleObject>(out puzzleObject);
                puzzleObject.OnInteraction();

            }
        }

        public void OnFocus()
        {
            //Activate the outline for the object
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                OnFocus();
            }
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