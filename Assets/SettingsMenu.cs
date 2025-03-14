using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public Slider slider1;  // Assign from Inspector
    public Slider slider2;  // Assign from Inspector

    public GameObject object1; // Assign the first GameObject
    public GameObject object2; // Assign the second GameObject

    private UIVirtualJoystick script1;
    private UIVirtualJoystick script2;

    private float storedValue1; // Store slider 1 value when inactive
    private float storedValue2; // Store slider 2 value when inactive

    void Start()
    {
        // Add listeners to the sliders
        slider1.onValueChanged.AddListener(UpdateMagnitude1);
        slider2.onValueChanged.AddListener(UpdateMagnitude2);
    }

    void UpdateMagnitude1(float value)
    {
        storedValue1 = value; // Store the value

        if (object1.activeInHierarchy)
        {
            script1 = object1.GetComponent<UIVirtualJoystick>();
            if (script1 != null)
            {
                script1.magnitudeMultiplier = value;
                Debug.Log("Magnitude 1: " + value);
            }
        }
    }

    void UpdateMagnitude2(float value)
    {
        storedValue2 = value; // Store the value

        if (object2.activeInHierarchy)
        {
            script2 = object2.GetComponent<UIVirtualJoystick>();
            if (script2 != null)
            {
                script2.magnitudeMultiplier = value;
                Debug.Log("Magnitude 2: " + value);
            }
        }
    }

    void Update()
    {
        // Check if the objects become active and apply stored values
        if (object1.activeInHierarchy && script1 == null)
        {
            script1 = object1.GetComponent<UIVirtualJoystick>();
            if (script1 != null)
            {
                script1.magnitudeMultiplier = storedValue1;
                Debug.Log("Magnitude 1: " + storedValue1);
            }
        }

        if (object2.activeInHierarchy && script2 == null)
        {
            script2 = object2.GetComponent<UIVirtualJoystick>();
            if (script2 != null)
            {
                script2.magnitudeMultiplier = storedValue2;
                Debug.Log("Magnitude 2: " + storedValue2);
            }
        }
    }
}
