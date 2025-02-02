using UnityEngine;
using System.Collections;

namespace UI
{
    public class MainMenuButtons : MonoBehaviour
    {
        public Canvas GameplayUI;
        public Canvas MainMenu;
        public float clickAnimationDepth = 0.1f;
        public float animationDuration = 0.1f;
        public float actionDelay = 0.5f;

        private Coroutine currentAnimationCoroutine = null;

        void Update()
        {
            if (!MainMenu.gameObject.activeSelf)
                return;

            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    Ray ray = Camera.main.ScreenPointToRay(touch.position);
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit))
                    {
                        GameObject clickedObject = hit.collider.gameObject;

                        if (currentAnimationCoroutine != null)
                        {
                            StopCoroutine(currentAnimationCoroutine);
                            clickedObject.transform.position = clickedObject.transform.position;
                        }

                        currentAnimationCoroutine = StartCoroutine(AnimateButtonPress(clickedObject));
                    }
                }
            }
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

        void OnObjectClick(string objectName)
        {
            Debug.Log(objectName + " clicked!");

            switch (objectName)
            {
                case "NewGameButton":
                    Debug.Log("Starting New Game...");
                    MainMenu.gameObject.SetActive(false);
                    GameplayUI.gameObject.SetActive(true);
                    break;
                case "LoadGameButton":
                    Debug.Log("Loading Game...");
                    break;
                case "SettingsButton":
                    Debug.Log("Opening Settings...");
                    break;
                default:
                    Debug.Log("Unknown object clicked!");
                    break;
            }
        }
    }
}
