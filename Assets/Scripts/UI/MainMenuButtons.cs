using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UI;
using Unity.VisualScripting;

namespace UI
{
    public class MainMenuButtons : MonoBehaviour
    {
        public Canvas GameplayUI;
        public Canvas MainMenu;
        public float clickAnimationDepth = 0.1f;
        public float animationDuration = 0.1f;
        public float actionDelay = 0.5f;
        public float fadeSpeed = 1f;

        public string buttonName;

        public GameObject SettingsMenu;

        [SerializeField]
        private List<GameObject> resetableGameObjects = new List<GameObject>();

        private Coroutine currentAnimationCoroutine = null;
        private Canvas fadeCanvas;
        private Image fadePanel;
        private Camera mainCamera;

        void Awake()
        {
            mainCamera = Camera.main;
        }

        void Start()
        {
            CreateFadeCanvas();
        }

        
        
        void CreateFadeCanvas()
        {
            // Create canvas GameObject
            GameObject fadeCanvasObj = new GameObject("FadeCanvas");
            fadeCanvas = fadeCanvasObj.AddComponent<Canvas>();
            fadeCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
            fadeCanvas.sortingOrder = 999; // Ensure it renders on top

            // Add CanvasScaler
            CanvasScaler scaler = fadeCanvasObj.AddComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(1920, 1080);
            scaler.matchWidthOrHeight = 0.5f;

            // Create panel GameObject
            GameObject panelObj = new GameObject("FadePanel");
            panelObj.transform.SetParent(fadeCanvasObj.transform, false);
            fadePanel = panelObj.AddComponent<Image>();
            fadePanel.color = new Color(0, 0, 0, 0);
            fadePanel.raycastTarget = false;

            // Set panel to fill entire screen
            RectTransform panelRect = panelObj.GetComponent<RectTransform>();
            panelRect.anchorMin = Vector2.zero;
            panelRect.anchorMax = Vector2.one;
            panelRect.sizeDelta = Vector2.zero;
            panelRect.anchoredPosition = Vector2.zero;

            // Initially disable the canvas
            fadeCanvas.enabled = false;
        }
        public void StartAnimateButtonPress(GameObject button)
        {
            if(currentAnimationCoroutine != null)
            {
                StopCoroutine(currentAnimationCoroutine);
            }
            currentAnimationCoroutine = StartCoroutine(AnimateButtonPress(button));

        }
        IEnumerator AnimateButtonPress(GameObject button)
        {

            Vector3 originalPosition = button.transform.position;
            Vector3 pressedPosition = originalPosition - new Vector3(0, clickAnimationDepth, 0);
            button.transform.position = pressedPosition;
            yield return new WaitForSeconds(animationDuration);
            if (!MainMenu.gameObject.activeSelf)
            {
                button.transform.position = originalPosition;
                yield break;
            }
            button.transform.position = originalPosition;
            yield return new WaitForSeconds(animationDuration);
            yield return new WaitForSeconds(actionDelay);
            OnObjectClick(button.name);
        }

        public void OnObjectClick(string objectName)
        {
            Debug.Log(objectName + " clicked!");
            switch (objectName)
            {
                case "NewGameButton":
                    StartCoroutine(NewGame());
                    break;
                case "LoadGameButton":
                    StartCoroutine(LoadGame());
                    break;
                case "SettingsButton":
                    StartCoroutine(Settings());
                    break;
                default:
                    Debug.Log("Unknown object clicked!");
                    break;
            }
        }

        IEnumerator NewGame()
        {
            Debug.Log("Starting New Game...");

            yield return StartCoroutine(FadeIn());

            foreach (GameObject obj in resetableGameObjects)
            {
                if (obj != null)
                {
                    obj.SetActive(true);
                }
            }
            yield return StartCoroutine(FadeOut());
        }

        IEnumerator LoadGame()
        {
            Debug.Log("Loading Game...");

            // Future implementation:
            // 1. Disable MainMenu canvas
            // MainMenu.gameObject.SetActive(false);

            // 2. Enable LoadGame canvas
            // LoadGameCanvas.gameObject.SetActive(true);

            // 3. LoadGame canvas should contain:
            //    - List of saved game slots
            //    - Back button to return to main menu
            //    - Each save slot should display:
            //      * Save date/time
            //      * Player progress/level
            //      * Screenshot thumbnail (optional)

            // 4. When save slot is selected:
            //    - Load game state from PlayerPrefs or save file
            //    - Initialize game with loaded state
            //    - Switch to gameplay UI
            yield break;
        }

        IEnumerator Settings()
        {
            Debug.Log("Opening Settings...");


            SettingsMenu.SetActive(true);

            // Future implementation:
            // 1. Disable MainMenu canvas
            // MainMenu.gameObject.SetActive(false);

            // 2. Enable Settings canvas
            // SettingsCanvas.gameObject.SetActive(true);

            // 3. Settings canvas should contain:
            //    - Audio settings (Master, Music, SFX volumes)
            //    - Graphics settings (Quality, Resolution)
            //    - Control settings (Sensitivity, Custom bindings)
            //    - Back button to return to main menu
            //    - Apply button to save changes

            // 4. Settings should be saved to PlayerPrefs
            //    when Apply button is clicked

            yield break;
        }

        IEnumerator FadeIn()
        {
            Debug.Log("fading in");
            fadeCanvas.enabled = true;
            Color panelColor = fadePanel.color;
            panelColor.a = 0f;
            fadePanel.color = panelColor;

            while (panelColor.a < 1)
            {
                panelColor.a += Time.deltaTime * fadeSpeed;
                fadePanel.color = panelColor;
                yield return null;
            }
        }

        IEnumerator FadeOut()
        {
            Color panelColor = fadePanel.color;
            Debug.Log("fading out");
            while (panelColor.a > 0)
            {
                panelColor.a -= Time.deltaTime * fadeSpeed;
                fadePanel.color = panelColor;
                yield return null;
            }
            fadeCanvas.enabled = false;
            MainMenu.gameObject.SetActive(false);
            GameplayUI.gameObject.SetActive(true);

        }
    }

    public class InitialTransform : MonoBehaviour
    {
        public Vector3 initialPosition;
        public Quaternion initialRotation;

        void Awake()
        {
            initialPosition = transform.position;
            initialRotation = transform.rotation;
        }
    }
}


//var menuButton = hitInformation.collider.gameObject.GetComponent<MainMenuButtons>();
//if (menuButton != null)
//{
//    Debug.Log("In handletouch mainmenubuttons' flow");
//    menuButton.StartAnimateButtonPress(menuButton.gameObject);
//}