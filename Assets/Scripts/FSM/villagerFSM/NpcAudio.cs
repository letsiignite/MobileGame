//using GhostFSM;
//using UnityEngine;

//public class NpcAudio : MonoBehaviour
//{
//    public AudioSource audioSource; // Reference to the audio source
//    public AudioClip cheeringClip;
//    public AudioClip screamingClip;
//    public AudioClip murmuringClip;


//    private GEState currentState;

//    void Start()
//    {
//        if (audioSource == null)
//        {
//            audioSource = GetComponent<AudioSource>();
//        }
//    }

//    public void SetCrowdState(GEState newState)
//    {
//        if (newState == currentState) return;

//        currentState = newState;

//        switch (currentState)
//        {
//            case CrowdState.Idle:
//                audioSource.Stop();
//                break;

//            //case CrowdState.Cheering:
//            //    PlaySound(cheeringClip);
//            //    break;

//            case CrowdState.Screaming:
//                PlaySound(screamingClip);
//                break;

//            case CrowdState.Murmuring:
//                PlaySound(murmuringClip);
//                break;
//        }
//    }

//    private void PlaySound(AudioClip clip)
//    {
//        if (clip == null)
//        {
//            Debug.LogWarning("Audio clip is missing!");
//            return;
//        }

//        audioSource.clip = clip;
//        audioSource.loop = true; // Set to true if you want continuous sound
//        audioSource.Play();
//    }

//}
