using UnityEngine;
using UnityEngine.UI; // For UI components
using DG.Tweening;    // For DOTween

public class MainMenuButtons: MonoBehaviour
{
    private Button button;

    void Start()
    {
        button = GetComponent<Button>();

        if (button != null)
        {
            // Add the animation to the button click event
            button.onClick.AddListener(OnButtonClicked);
        }
    }

    void OnButtonClicked()
    {
        // Animate the scale to 0.9x (shrink) and back to 1x (original size)
        transform.DOScale(0.9f, 0.1f)    // Shrink to 90% size in 0.1 seconds
                 .SetEase(Ease.OutQuad)  // Easing for smooth shrinking
                 .OnComplete(() =>
                 {
                     transform.DOScale(1f, 0.1f) // Return to original size
                              .SetEase(Ease.OutBounce); // Easing for bounce effect
                 });
    }

    void OnDestroy()
    {
        // Remove the listener when the object is destroyed to avoid memory leaks
        if (button != null)
            button.onClick.RemoveListener(OnButtonClicked);
    }
}