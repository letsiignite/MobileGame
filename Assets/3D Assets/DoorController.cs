using Unity.VisualScripting;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    Animator DoorAnimator;
    public Transform Player;
    public float DitectionDistance = 3f;
    public LayerMask PlayerLayer;
    bool isPlayerNear=false;
    public string KeyLayer_name;
    public GameObject Key;


    private void Start()
    {
        DoorAnimator = GetComponent<Animator>();    
    }
    void CheckPlayerDistance()
    {
        RaycastHit hit;
        Vector3 DirectionToPlayer=Player.position - transform.position; 
        if(Physics.Raycast(transform.position, DirectionToPlayer, out hit,DitectionDistance,PlayerLayer))
        {
            if(hit.transform==Player)
            {
                isPlayerNear = true;
                return;
            }

        }
        isPlayerNear=false;
    }
    void OPenDoor()
    {
        if(DoorAnimator!= null)
        {
            if(!string.IsNullOrEmpty(KeyLayer_name))
            {
                bool PlayerHasKey = false;
                foreach(Transform Key in Player)
                {
                    if(Key.gameObject.layer==LayerMask.NameToLayer(KeyLayer_name))
                    {
                        PlayerHasKey = true;
                        break;
                    }
                }
                if(!PlayerHasKey)
                {
                    return;
                }
            }
            DoorAnimator.SetTrigger("Open");
            Key.SetActive(false);   


        }
    }
    public void OnDoorButtonPressed()
    {
        OPenDoor();
    }
    private void Update()
    {
        CheckPlayerDistance();
        if(isPlayerNear && Input.GetKeyDown(KeyCode.M))
        {
            OnDoorButtonPressed();  
        }
    }
}
