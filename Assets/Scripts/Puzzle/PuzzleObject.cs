using Game;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Puzzle
{
    public enum ObjectType
    {
        COLLECT,    // this object mut be collected for another object that will consume it, like key which will be consumed by door.
        CONSUME,    // this object will be assigned to activationObject of the puzzle object that will consume it.
       
    }

    /// <summary>
    /// this system must be redone.
    /// </summary>
    public class PuzzleObject : MonoBehaviour, IPuzzle
    {
        [SerializeField]
        private bool isInitializationObject;
        [SerializeField]
        private List<PuzzleObject> puzzleObjectsToActivate;
        private bool isActive = false;
        [SerializeField] 
        private ObjectType type;
        [SerializeField]
        private GameObject puzzleInfoProvider; // This is the object that provides the info, like text explaining what to do or any audio that explains what must be done.
                                               // this object must be setup in the scene and assigned.
        [SerializeField]
        private GameObject activationObject; // This object must be in players inventory to activate this object. this object must be placed in the scene and added through inspector.
        //[SerializeField]
        //private GameObject completionObject; // This object will be activated or deactivated to finish the puzzle. This will be assigned to last object in puzzle.

        [SerializeField]
        private bool isCompletionObject;

        private List<Action> puzzleListeners = new();

        private Inventory inventory;
        private const string ACTIVATION_OBJECT_NOT_FOUND = "Find the key object";
        public void Activate()
        {
            isActive = true;
            // draw an outline to highlight this object so player knows that this is part of current puzzle
        }

        public void AddPuzzleListener(Action callback)
        {
            puzzleListeners.Add(callback);
        }

        public void Deactivate() 
        { 
            isActive = false;
            if (GetComponent<Animator>() != null)
            {
                // trigger animation
            }
            else
            {
                foreach (var action in puzzleListeners) 
                {
                    action.Invoke();
                }
                gameObject.SetActive(false);    
            }
            activationObject.SetActive(false);
        }

        public void OnInteraction()
        {
            Debug.Log("Pass 3");
            inventory = (Inventory)FindObjectOfType(typeof(Inventory));
            if (isInitializationObject)
            {
                foreach (var obj in puzzleObjectsToActivate)
                {
                    obj.Activate();
                    obj.gameObject.SetActive(isActive);
                }
                Activate();
            }
            else 
            {
                Debug.Log("Pass 3-1");
                // check if inventory has the activation object, if yes then trigger unlock process. Else play a error sound.
                if (type == ObjectType.COLLECT)
                {
                    inventory.PickUpItem(this.gameObject);
                }
                else if (type == ObjectType.CONSUME)
                {
                    if (!inventory.ConsumeItem(activationObject))
                    {
                        //PlayerData error sound and must display a message
                        GameManager._instance.ShowHintsAndWarnings(ACTIVATION_OBJECT_NOT_FOUND);
                    }

                    if (isCompletionObject)
                    {
                        Deactivate();
                        Debug.Log("Problem Solved");
                    }
                }
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