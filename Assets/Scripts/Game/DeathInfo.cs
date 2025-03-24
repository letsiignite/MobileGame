using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Game;
using System;
using UI;
using System.Collections;


namespace UI
{
    public class DeathInfo : MonoBehaviour
    {
        [SerializeField]
        private GameObject deathPanel; // Reference to the UI panel
        [SerializeField]
        private TMP_Text deathMessageText; // Reference to the Text component
        [SerializeField]
        private GameManager gameManager;

        public void Init(GameObject deathPanelReference)
        {
            if (gameManager != null)
            {
                Debug.Log("GameManager found!");
            }
            else
            {
                Debug.LogError("GameManager not found in the scene!");
            }
            // Assign the panel reference passed from the GameManager
            if (deathPanel != null)
            {
                Debug.Log("Death panel reference initialized.");
            }
            else
            {
                Debug.LogError("Death panel reference is missing.");
            }
        }

        public void DeathDisplay(string cause)
        {
            // Switch UI panel to death panel
            deathPanel.SetActive(true);
            UI.InGameMenu inGameMenu = FindFirstObjectByType<InGameMenu>();
            inGameMenu.InvokePauseListener();

            string message = GetDeathMessage(cause);
            deathMessageText.text = message + "\n" + "You will relive to fulfill your Destiny";
            StartCoroutine(DisplayDeathMessage());
        }

        private IEnumerator DisplayDeathMessage()
        {
            yield return new WaitForSeconds(5f);
            gameManager.ReloadCheckpoint(3);
        }

        private string GetDeathMessage(string cause)
        {
            Dictionary<string, string> deathCause = new Dictionary<string, string>
                {
                    { "Ghost", "You were caught by a ghost." },
                    { "Trap", "You fell into a trap." },
                    { "Water", "You drowned in water." },
                    { "Sanity", "You lost your sanity." },
                    { "Food", "You succumbed to starvation." }
                };

            if (deathCause.ContainsKey(cause))
            {
                return deathCause[cause];
            }

            return "Unknown cause of death.";
        }
    }


}