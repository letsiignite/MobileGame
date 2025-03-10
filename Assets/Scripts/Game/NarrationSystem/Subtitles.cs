using TMPro;
using UnityEngine;
using UnityEngine.Audio;

public class Subtitles : MonoBehaviour
{
    private TextMeshProUGUI subtitleBox;
    [SerializeField]
    private AudioSource Narrator;
    [SerializeField]
    private float displayDuration = 3f;
    private void Awake()
    {
        subtitleBox = GetComponent<TextMeshProUGUI>();
    }
    public void DisplayTextWithAudio(string text, AudioClip voiceClip)
    {
        Debug.Log("In here");
        CancelInvoke(nameof(HideText));

        // Play the voice line
        if (voiceClip != null)
        {
            Narrator.Stop();
            Narrator.clip = voiceClip;
            Narrator.Play();
        }

        // Display the subtitle
        subtitleBox.text = text;
        gameObject.SetActive(true);


        float duration = voiceClip != null ? voiceClip.length : displayDuration;
        Invoke(nameof(HideText), duration);
    }

    private void HideText()
    {
        if (subtitleBox != null)
        {
            gameObject.SetActive(false);
        }
    }
}
