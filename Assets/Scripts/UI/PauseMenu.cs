using UnityEngine;
using UnityEngine.UI;
using System.Collections;

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
        private Canvas newGameplayUI;

        [SerializeField]
        private Canvas SettingsMenu;

        void Start()
        {
            this.gameObject.SetActive(true);
            RestartButton.onClick.AddListener(() => StartCoroutine(HandleButtonClick(RestartButton, RestartLevel)));
            ResumeButton.onClick.AddListener(() => StartCoroutine(ResumeGame()));
            LoadLevelButton.onClick.AddListener(() => StartCoroutine(HandleButtonClick(LoadLevelButton, LoadLevel)));
            SettingsButton.onClick.AddListener(() => StartCoroutine(HandleButtonClick(SettingsButton, OpenSettings)));
            ExitButton.onClick.AddListener(() => StartCoroutine(HandleButtonClick(ExitButton, ExitGame)));
        }

        IEnumerator ResumeGame()
        {
            Debug.Log("Resuming Game from PauseMenu...");

            yield return StartCoroutine(AnimateButton(ResumeButton));

            if (newGameplayUI != null)
            {
                newGameplayUI.gameObject.SetActive(true);
                gameObject.SetActive(false);
            }
            else
            {
                Debug.LogError("New GameplayUI reference not found!");
            }
        }

        IEnumerator HandleButtonClick(Button button, System.Action task)
        {
            yield return StartCoroutine(AnimateButton(button));
            task.Invoke();
        }

        IEnumerator AnimateButton(Button button)
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
        }

        void RestartLevel()
        {
            Debug.Log("Restarting Level...");
            // Add level restart logic
        }

        void LoadLevel()
        {
            Debug.Log("Loading Level...");
            // Add level loading logic
        }

        void OpenSettings()
        {
            Debug.Log("Opening Settings...");
            if (SettingsMenu != null)
            {
                SettingsMenu.gameObject.GetComponent<SettingsMenu>().OpenFromPauseMenu();
            }
            else
            {
                Debug.LogError("SettingsMenu reference not found!");
            }
        }

        void ExitGame()
        {
            Debug.Log("Exiting Game...");
            // Application.Quit(); // Uncomment this line to quit the game in a built version
        }
    }
}
