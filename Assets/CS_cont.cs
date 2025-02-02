using UnityEngine;
using UnityEngine.Playables;

public class CS_cont : MonoBehaviour
{
    PlayableDirector director;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        director = GetComponent<PlayableDirector>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Submit"))
        {
            director.Play();
        }
    }
}
