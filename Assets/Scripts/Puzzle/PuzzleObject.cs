using System.Collections.Generic;
using UnityEngine;

namespace Puzzle
{
    public class PuzzleObject : MonoBehaviour, IPuzzle
    {
        [SerializeField]
        private bool isInitializationObject;
        [SerializeField]
        private List<PuzzleObject> puzzleObjectsToActivate;
        private bool isActive = false;
        [SerializeField]
        private GameObject puzzleInfoProvider; // This is the object that provides the info, like text explaining what to do or any audio that explains what must be done.
                                               // this object must be setup in the scene and assigned.
        [SerializeField]
        private GameObject activationObject; // This object must be in players inventory to activate this object. this object must be placed in the scene and added through inspector.
        public void Activate()
        {
            isActive = true;
            // draw an outline to highlight this object so player knows that this is part of current puzzle
        }

        public void OnInteraction()
        {
            if (isInitializationObject)
            {
                foreach (var obj in puzzleObjectsToActivate)
                {
                    obj.Activate();
                }
                Activate();
            }
            else
            { 
                // check if inventory has the activation object, if yes then trigger unlock process. Else play a error sound.
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