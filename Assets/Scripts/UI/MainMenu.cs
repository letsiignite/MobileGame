
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField]
        private List<GameObject> uiObjectsToHideOnPause;
        [SerializeField]
        private GameObject pausePanel;
        [SerializeField]
        private Button pauseButton;
        private List<Action> pauseListners;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

        }

        private void OnPauseClicked()
        {
            if (uiObjectsToHideOnPause.Count > 0)
            { 
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
            pauseListners.Add(callback);    
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}