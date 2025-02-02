using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace UI
{
    public class PauseMenu : MonoBehaviour
    {
        public Button ResumeButton;
        public Button RestartButton;
        public Button LoadLevelButton;
        public Button SettingsButton;
        public Button ExitButton;

        [SerializeField]
        private InGameMenu inGameMenu;

        void Start()
        {

            ResumeButton.onClick.AddListener(() => StartCoroutine(HandleButtonClick(ResumeButton, ResumeGame)));
            RestartButton.onClick.AddListener(() => StartCoroutine(HandleButtonClick(RestartButton, RestartLevel)));
            LoadLevelButton.onClick.AddListener(() => StartCoroutine(HandleButtonClick(LoadLevelButton, LoadLevel)));
            SettingsButton.onClick.AddListener(() => StartCoroutine(HandleButtonClick(SettingsButton, OpenSettings)));
            ExitButton.onClick.AddListener(() => StartCoroutine(HandleButtonClick(ExitButton, ExitGame)));
        }

        IEnumerator HandleButtonClick(Button button, System.Action task)
        {
            Transform btnTransform = button.transform;
            Vector3 originalScale = btnTransform.localScale;
            Vector3 pressedScale = originalScale * 0.9f;

            btnTransform.localScale = pressedScale;
            yield return new WaitForSeconds(0.1f);

            float elapsedTime = 0f;
            while (elapsedTime < 0.1f)
            {
                btnTransform.localScale = Vector3.Lerp(pressedScale, originalScale, elapsedTime / 0.1f);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            btnTransform.localScale = originalScale;

            yield return new WaitForSeconds(0.1f);
            task.Invoke();
        }

        void ResumeGame()
        {
            Debug.Log("Resuming Game from PauseMenu...");
            // Call ResumeGame on InGameMenu
            if (inGameMenu != null)
            {
                inGameMenu.ResumeGame();
            }
            else
            {
                Debug.LogError("InGameMenu reference not found!");
            }
        }

        void RestartLevel()
        {
            Debug.Log("Restarting Level...");
        }

        void LoadLevel()
        {
            Debug.Log("Loading Level...");
        }

        void OpenSettings()
        {
            Debug.Log("Opening Settings...");
        }

        void ExitGame()
        {
            Debug.Log("Exiting Game...");
        }
    }
}
