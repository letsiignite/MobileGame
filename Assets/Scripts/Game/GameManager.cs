using System;
using System.Collections.Generic;
using System.Linq;
using UI;
using Unity.VisualScripting;
using UnityEngine;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager _instance;
        [SerializeField]
        private GEntityAI ghostAi;
        [SerializeField]
        private List<VillagerContext> villagerContext;
        [SerializeField]
        private UIManager uiManager;

        /// <summary>
        /// This is stupid, but I want to see how it works out.
        /// </summary>
        /// <returns></returns>
        public GameManager GetGameManager()
        {
            return this;
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
        }

        public void AddPauseListners(Action callback)
        { 
            uiManager.AddPauseListners(callback);
        }

        public void DeathDisplay(string cause)
        {
            uiManager.DeathDisplay(cause);
        }

        public void Interact()
        { 
            
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}