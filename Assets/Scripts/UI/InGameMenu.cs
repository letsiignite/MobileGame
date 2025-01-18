
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace UI
{
    public class InGameMenu : MonoBehaviour
    {
        [SerializeField]
        private List<GameObject> uiObjectsToHideOnPause;
        [SerializeField]
        private GameObject pausePanel;
        [SerializeField]
        private Button pauseButton;
        [SerializeField]
        private Button interactButton;
        private List<Action> pauseListners;

        private void Awake()
        {
            pauseListners = new List<Action>();
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        { 
        
            interactButton.onClick.AddListener(pickup);
        }

        private void pickup()
        { 
            
        }

        public void OnPauseClicked()
        {
            if (uiObjectsToHideOnPause.Count > 0)
            {
                Debug.Log("Passed");
                foreach(GameObject go in uiObjectsToHideOnPause) 
                { 
                    go.SetActive(false); 
                }
            }

            if (pauseListners.Count > 0)
            {
                foreach (Action callback in pauseListners)
                {
                    callback.Invoke();
                }
            }

        }

        public void AddPauseListners(Action callback)
        {
            Debug.Log("Adding Pause Listener");
            pauseListners.Add(callback);    
        }

        
    }
}