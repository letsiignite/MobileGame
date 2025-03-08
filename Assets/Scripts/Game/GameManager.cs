using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UI;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager _instance;
        [SerializeField]
        private GEntityAI ghostAi;
        [SerializeField]
        private GameObject playerRef;
        [SerializeField]
        private List<VillagerContext> villagerContext;
        [SerializeField]
        private UIManager uiManager;
        [SerializeField]
        private Subtitles subtitleScript;
        public event Action OnCameraShake;

        /// <summary>
        /// This is stupid, but I want to see how it works out.
        /// </summary>
        /// <returns></returns>
        public GEntityAI GetGhostController()
        {
            return ghostAi;
        }

        public GameObject GetPlayerReference()
        {
            return playerRef;
        }

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            Invoke("InitAllObjects", 0.4f);
        }


        private void InitAllObjects()
        {
            ghostAi.Init(this);
            villagerContext = FindObjectsByType<VillagerContext>(FindObjectsSortMode.None).ToList();
            foreach (VillagerContext context in villagerContext)
            {
                context.Init(this);
            }
            subtitleScript = FindAnyObjectByType<Subtitles>();
        }

        public void AddPauseListners(Action callback)
        { 
            uiManager.AddPauseListners(callback);
        }

        public void DeathDisplay(string cause)
        {
            uiManager.DeathDisplay(cause);
        }

        public void ShowHintsAndWarnings(String msg)
        {
            uiManager.ShowHintsAndWarnings(msg);
        }
       

        //Respawning Player after death
        public void ReloadCheckpoint(float delay,Transform checkpoint = null)
        {
            if(checkpoint == null)
            {
                StartCoroutine(ReloadSceneAfterDelay(delay));
            }
            // else condition for checkpoint is not yet coded here ---- pending
        }
        IEnumerator ReloadSceneAfterDelay(float delay)
        {
            Debug.Log("in reloadScene");
            yield return new WaitForSeconds(delay);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
        }




        public void SubtitleDisplay(string line , AudioClip voiceLine)
        {
            subtitleScript.DisplayTextWithAudio(line,voiceLine);
        }


        //fire CameraShake
        public void TriggerCameraShake()
        {
            if (OnCameraShake != null)
            {
                OnCameraShake.Invoke(); 
            }
        }
    }
}