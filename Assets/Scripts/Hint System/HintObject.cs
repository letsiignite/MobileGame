using Game;
using Interactable;
using LevelProgression;
using System.Collections.Generic;
using UnityEngine;

public enum HintType
{
    Enable_object,
    Animation,
    Audio,
}

[RequireComponent(typeof(BaseInteractableObject))]
public class HintObject : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private string HintText;
    [SerializeField] private HintType htype;
    [SerializeField] private bool levelProjBool;

    public List<GameObject> h_enableObjects;
    private Animator animator;
    private bool animBoolTrig;
    private AudioClip clip;
    private LevelProgressionObject levelObject;

    private void Start()
    {
        levelObject = GetComponentInParent<LevelProgressionObject>();
    }

    public void TriggerHint()
    {
        if (levelProjBool)
        {
            levelObject.LevelCompleted();
        }
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
                animBoolTrig = !animBoolTrig;
                animator.SetBool("Interact", animBoolTrig);
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



