using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public enum HintType
{
   Interactive,
   AreaTrigger,
}

[CreateAssetMenu(fileName = "Hint", menuName = "Scriptable Objects/Hint")]
public class Hint : ScriptableObject
{
    public string HintText;
    public HintType htype;
    public List<GameObject> h_gameObjects;
    public List<GameObject> v_people;
    public void TriggerEvent()
    { 
        switch (htype)
        {
            case HintType.AreaTrigger:
                for(int i = 0; i < h_gameObjects.Count;i++)
                {
                    h_gameObjects[i].SetActive(false);
                    Debug.Log("areaTrigger");
                }
                break;

            case HintType.Interactive:
                for (int i = 0; i < h_gameObjects.Count; i++)
                {
                    h_gameObjects[i].SetActive(false);
                    Debug.Log("Ineractive");
                }
                break;

            default:
                // Code to execute if none of the above cases match
                break;
        }
    }
    
    
    
}
