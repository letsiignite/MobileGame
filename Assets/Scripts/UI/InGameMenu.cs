using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class InGameMenu : MonoBehaviour
    {
        [SerializeField]
        private List<GameObject> uiObjectsToHideOnPause;
        [SerializeField]
        private Canvas pausePanel;
        [SerializeField]
        private Button pauseButton;
        [SerializeField]
        private Button interactButton;
        private List<Action> pauseListeners;

        private void Awake()
        {
            pauseListeners = new List<Action>();
        }

        void Start()
        {
            
            if (interactButton != null)
            {
                interactButton.onClick.AddListener(pickup);
            }

            if (pauseButton != null)
            {
                pauseButton.onClick.AddListener(OnPauseClicked);
            }
        }

        private void pickup()
        {
        }

        public void OnPauseClicked()
        {
            if (uiObjectsToHideOnPause.Count > 0)
            {
                Debug.Log("Passed hide");
                foreach (GameObject go in uiObjectsToHideOnPause)
                {
                    go.SetActive(false);
                }
            }

            if (pauseListeners.Count > 0)
            {
                foreach (Action callback in pauseListeners)
                {
                    callback.Invoke();
                }
            }
            pausePanel.gameObject.SetActive(true);
        }

        public void AddPauseListeners(Action callback)
        {
            //Debug.Log("Adding Pause Listener");
            pauseListeners.Add(callback);
        }

        public void ResumeGame()
        {
            if (uiObjectsToHideOnPause.Count > 0)
            {
                Debug.Log("Passed show");
                foreach (GameObject go in uiObjectsToHideOnPause)
                {
                    go.SetActive(true);
                }
            }

            pausePanel.gameObject.SetActive(false);
        }
    }
}