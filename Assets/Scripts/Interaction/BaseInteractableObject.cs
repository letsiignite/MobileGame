using Game;
using Puzzle;
using UnityEngine;

namespace Interactable
{
    public enum InteractType
    {
        CONSUME_ITEM,
        PICKUP_ITEM,
        PUZZLE_ITEM,
        HINT_ITEM,
        GAME_MECHS_ANIM
    }

    public class BaseInteractableObject : MonoBehaviour, IBaseInteractableObject
    {
        [SerializeField] private InteractType interactType;
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
            if (interactType == InteractType.HINT_ITEM)
            {
                TryGetComponent<HintObject>(out hintObject);
                hintObject.TriggerHint();
            }
            else if (interactType == InteractType.CONSUME_ITEM)
            {
                GameManager._instance.GetPlayerReference().GetComponent<Inventory>().ConsumeItem(gameObject);
            }
            else if (interactType == InteractType.PICKUP_ITEM)
            {
                GameManager._instance.GetPlayerReference().GetComponent<Inventory>().PickUpItem(gameObject);
            }
            else if (interactType == InteractType.PUZZLE_ITEM)
            {
                //Debug.Log("Pass 2");
                TryGetComponent<PuzzleObject>(out puzzleObject);
                puzzleObject.OnInteraction();
            }
            else if (interactType == InteractType.GAME_MECHS_ANIM)
            {
                GetComponent<Animator>().SetTrigger("Interact");
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
            gameObject.layer = LayerMask.NameToLayer("Item");
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}