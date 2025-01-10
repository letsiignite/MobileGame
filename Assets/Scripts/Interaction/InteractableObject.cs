using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private HintObject h_object;
    
    private bool player_interacted = false;

    void Start()
    {
        h_object = GetComponent<HintObject>();
    }

    private void Update()
    {
        if (h_object != null && player_interacted)
        {
            h_object.TriggerHint();
        }
    }

    public void Set_player_interacted(bool b)
    {
        player_interacted = b;
    }

}
