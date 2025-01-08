using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public float Pickupradius = 2f;
    public KeyCode PickupKey = KeyCode.Space;
    public KeyCode DropKey = KeyCode.M;
    public Transform ItemHoldPosition;
    public Transform Playerbody;
    public Transform KeyHolder;
    GameObject HeldItem;
    bool iskey;
    private void Update()
    {
        if(Input.GetKey(PickupKey))
        {
            TryPickUpItem();

        }
        if(Input.GetKey(DropKey))
        {
            DropItem();
        }
    }
    void TryPickUpItem()
    {
        if(HeldItem!=null)
        {
            return;
        }
        Collider[] hitcolliders=Physics.OverlapSphere(transform.position, Pickupradius);
        GameObject Closeditem = null;
        float ClosedDistance = Pickupradius;
        foreach(var hitcollider in hitcolliders)
        {
            if (hitcollider.CompareTag("Key"))
            {
                float distance=Vector3.Distance(transform.position,hitcollider.transform.position);
                if (distance < ClosedDistance)
                {
                    ClosedDistance = distance;
                    Closeditem = hitcollider.gameObject;
                }
                iskey = true;
            }
        }
        if(Closeditem!=null)
        {
            PickUp(Closeditem);
        }
    }
    void PickUp(GameObject item)
    {
        HeldItem = item;
        HeldItem.GetComponent<Collider>().enabled = false;
        if(HeldItem.GetComponent<Rigidbody>())
        {
            HeldItem.GetComponent<Rigidbody>().isKinematic = true;
        }
        if(iskey)
        {
            HeldItem.transform.SetParent(Playerbody);
            HeldItem.transform.position = ItemHoldPosition.position;
            HeldItem.transform.rotation=ItemHoldPosition.rotation;
        }
        Debug.Log("PickedUp");
    }
    void DropItem()
    {
        if(HeldItem!=null)
        {
            HeldItem.GetComponent<Collider>().enabled = true;
            HeldItem.transform.position = transform.position+transform.forward;
            if(HeldItem.GetComponent<Rigidbody>() )
            {
                HeldItem.GetComponent<Rigidbody>().isKinematic = false;
            }
            HeldItem.transform.SetParent(KeyHolder);
            Debug.Log("Dropped");
            HeldItem=null;  
            iskey = false;
        }
        else
        {
            Debug.Log("No item to drop");
        }
    }
}
