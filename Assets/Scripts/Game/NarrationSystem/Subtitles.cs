using TMPro;
using UnityEngine;
using UnityEngine.Audio;

public class Subtitles : MonoBehaviour
{
    private TextMeshPro subtitleBox;
    private AudioSource Narrator;
    [SerializeField]
    private float displayDuration = 3f; 
    private void Awake()
    {
        subtitleBox = GetComponent<TextMeshPro>();
        subtitleBox.gameObject.SetActive(false); 
    }
    public void DisplayTextWithAudio(string text, AudioClip voiceClip)
    {

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
        subtitleBox.gameObject.SetActive(true);


        float duration = voiceClip != null ? voiceClip.length : displayDuration;
        Invoke(nameof(HideText), duration);
    }

    private void HideText()
    {
        if (subtitleBox != null)
        {
            subtitleBox.gameObject.SetActive(false);
        }
    }
}
