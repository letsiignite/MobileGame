using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SettingsMenu : MonoBehaviour
    {
        [SerializeField]
        private Slider slider1;
        [SerializeField]
        private Slider slider2;
        [SerializeField]
        private Button CrossButton;
        [SerializeField]
        private Canvas Settings;
        [SerializeField]
        private Canvas MainMenu;
        [SerializeField]
        private Canvas PauseMenu;

        public GameObject object1;
        public GameObject object2;

        private UIVirtualJoystick script1;
        private UIVirtualJoystick script2;
        private float storedValue1;
        private float storedValue2;

        private bool openedFromPauseMenu = false;

        void Start()
        {
            storedValue1 = slider1.value;
            storedValue2 = slider2.value;

            slider1.onValueChanged.AddListener(UpdateMagnitude1);
            slider2.onValueChanged.AddListener(UpdateMagnitude2);
            CrossButton.onClick.AddListener(CloseSettings);

            // Find and initialize scripts
            TryGetScript1();
            TryGetScript2();
        }

        void OnEnable()
        {
            // When this object is enabled, make sure to refresh references
            // and apply current values to the joysticks
            TryGetScript1();
            TryGetScript2();

            // Set slider values to match stored values
            if (slider1 != null)
                slider1.value = storedValue1;

            if (slider2 != null)
                slider2.value = storedValue2;
        }

        public void OpenFromMainMenu()
        {
            openedFromPauseMenu = false;
            Settings.gameObject.SetActive(true);
            MainMenu.gameObject.SetActive(false);
        }

        public void OpenFromPauseMenu()
        {
            openedFromPauseMenu = true;
            Settings.gameObject.SetActive(true);
            PauseMenu.gameObject.SetActive(false);
        }

        void UpdateMagnitude1(float value)
        {
            storedValue1 = value;
            Debug.Log("Stored Value 1: " + storedValue1);

            // Always attempt to refresh the script reference first
            TryGetScript1();

            // Apply the value to the script directly
            if (script1 != null)
            {
                script1.magnitudeMultiplier = storedValue1;
                Debug.Log("Applied Magnitude 1: " + storedValue1 + " to joystick");
            }
            else
            {
                Debug.LogWarning("Cannot apply Magnitude 1: script1 is null");
            }
        }

        void UpdateMagnitude2(float value)
        {
            storedValue2 = value;
            Debug.Log("Stored Value 2: " + storedValue2);

            // Always attempt to refresh the script reference first
            TryGetScript2();

            // Apply the value to the script directly
            if (script2 != null)
            {
                script2.magnitudeMultiplier = storedValue2;
                Debug.Log("Applied Magnitude 2: " + storedValue2 + " to joystick");
            }
            else
            {
                Debug.LogWarning("Cannot apply Magnitude 2: script2 is null");
            }
        }

        void CloseSettings()
        {
            Settings.gameObject.SetActive(false);

            if (openedFromPauseMenu)
            {
                PauseMenu.gameObject.SetActive(true);
                Debug.Log("Returning to Pause Menu");
            }
            else
            {
                MainMenu.gameObject.SetActive(true);
                Debug.Log("Returning to Main Menu");
            }
            openedFromPauseMenu = false;

            // Make sure the values are applied before closing
            ApplySettingsToActiveJoysticks();
        }

        // Apply current settings to joysticks if they are active
        private void ApplySettingsToActiveJoysticks()
        {
            if (object1 != null && object1.activeInHierarchy)
            {
                TryGetScript1();
                if (script1 != null)
                {
                    script1.magnitudeMultiplier = storedValue1;
                    Debug.Log("Final Apply Magnitude 1: " + storedValue1);
                }
            }

            if (object2 != null && object2.activeInHierarchy)
            {
                TryGetScript2();
                if (script2 != null)
                {
                    script2.magnitudeMultiplier = storedValue2;
                    Debug.Log("Final Apply Magnitude 2: " + storedValue2);
                }
            }
        }

        private void LateUpdate()
        {
            // Continuously check for joystick references
            // This catches cases where objects become active during gameplay
            if (object1 != null && object1.activeInHierarchy && script1 == null)
            {
                TryGetScript1();
            }

            if (object2 != null && object2.activeInHierarchy && script2 == null)
            {
                TryGetScript2();
            }
        }

        private void TryGetScript1()
        {
            if (object1 != null)
            {
                script1 = object1.GetComponent<UIVirtualJoystick>();
                if (script1 != null)
                {
                    script1.magnitudeMultiplier = storedValue1;
                    Debug.Log("Initialized Magnitude 1: " + storedValue1);
                }
                else
                {
                    Debug.LogWarning("UIVirtualJoystick component not found on object1");
                }
            }
        }

        private void TryGetScript2()
        {
            if (object2 != null)
            {
                script2 = object2.GetComponent<UIVirtualJoystick>();
                if (script2 != null)
                {
                    script2.magnitudeMultiplier = storedValue2;
                    Debug.Log("Initialized Magnitude 2: " + storedValue2);
                }
                else
                {
                    Debug.LogWarning("UIVirtualJoystick component not found on object2");
                }
            }
        }

        private void OnDisable()
        {
            // Make sure settings are applied when this component is disabled
            ApplySettingsToActiveJoysticks();
        }
    }
}