using Game;
using LevelProgression;
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
        [SerializeField] private Transform[] houseTransforms; // Array of house positions
        [SerializeField] private Vector3 spawnAreaSize = new Vector3(1, 0, 1);

        private LevelProgressionObject levelObject;
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
            //Debug.Log("Pass 3");
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
                //Debug.Log("Pass 3-1");
                // check if inventory has the activation object, if yes then trigger unlock process. Else play a error sound.
                if (type == ObjectType.COLLECT)
                {
                    inventory.PickUpItem(gameObject);
                    //Debug.Log("Invetory Consumption : " + this.gameObject.name + " " + this.name);
                }
                else if (type == ObjectType.CONSUME)
                {
                    //Debug.Log("Invetory Consumption : " + activationObject);
                    if (!inventory.ConsumeItem(activationObject))
                    {
                        //PlayerData error sound and must display a message
                        GameManager._instance.ShowHintsAndWarnings(ACTIVATION_OBJECT_NOT_FOUND);
                    }

                    if (isCompletionObject)
                    {
                        GameManager._instance.GetGhostController().SetDistractedObject(gameObject);
                        Deactivate();
                        levelObject.LevelCompleted();
                        Debug.Log("Problem Solved");
                    }
                }
            }
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            if(houseTransforms.Length > 0)
            {
                Transform houseTransform = houseTransforms[UnityEngine.Random.Range(0, houseTransforms.Length)];
                Vector3 spawnPosition = houseTransform.position;
                if (spawnPosition.y <= 0)
                {
                    spawnPosition.y = 2;
                }
                this.transform.position = spawnPosition;
                //Debug.Log("spawned lvl 4 object");
            }

            levelObject = GetComponentInParent<LevelProgressionObject>();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}