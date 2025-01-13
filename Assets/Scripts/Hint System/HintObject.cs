using System.Collections.Generic;
using UnityEngine;

public enum HintType
{
    Enable_object,
    Animation,
    Audio,
}

public class HintObject : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private string HintText;
    [SerializeField] private HintType htype;

    public List<GameObject> h_enableObjects;
    private Animator animator;
    private AudioClip clip;

    public void TriggerHint()
    {
        switch (htype)
        {
            case HintType.Enable_object:
                for (int i = 0; i < h_enableObjects.Count; i++)
                {
                    h_enableObjects[i].SetActive(true);
                    //Debug.Log("Enabling objects");
                }
                break;

            case HintType.Animation:
                animator = gameObject.GetComponent<Animator>();
                //play animation here
                break;

            case HintType.Audio:
                clip = gameObject.GetComponent<AudioClip>();
                //play audio clip here
                break;

            default:
                // Code to execute if none of the above cases match
                break;
        }
    }
}



