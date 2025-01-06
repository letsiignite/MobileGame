using System.Collections.Generic;
using UnityEngine;

public class HintManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    private Hint m_Hint;
    [SerializeField]
    private List<GameObject> hintobect_List;
    private void Awake()
    {
        for (int i = 0; i < hintobect_List.Count; i++)
        {
            m_Hint.h_gameObjects.Add(hintobect_List[i]);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            m_Hint.TriggerEvent();
        }
    }
}
