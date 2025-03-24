using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class InGameMenu : MonoBehaviour
    {
        [SerializeField]
        private Canvas pausePanel;
        [SerializeField]
        private Button pauseButton;
        [SerializeField]
        private Button interactButton;
        [SerializeField]
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
            InvokePauseListener();
            this.gameObject.SetActive(false);
            pausePanel.gameObject.SetActive(true);
        }

        public void InvokePauseListener()
        {
            if (pauseListeners.Count > 0)
            {
                foreach (Action callback in pauseListeners)
                {
                    callback.Invoke();
                }
            }
        }
        public void AddPauseListeners(Action callback)
        {
            //Debug.Log("Adding Pause Listener");
            pauseListeners.Add(callback);
        }
    }
}