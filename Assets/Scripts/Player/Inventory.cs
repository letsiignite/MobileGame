using Interactable;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [Header("Item")]
    [SerializeField] private Button itemPickUpButton;
    [SerializeField] private RectTransform itemPickUpImage;
    [SerializeField] private LayerMask itemLayer;
    [SerializeField] private int fullSlotCount = 2;
    [SerializeField] private int totalCapacity = 10;
    [SerializeField] private int currentCapacity = 0;

    [Header("Colliders")]
    [SerializeField] private float overlapSphereRadius = 1;
    
    private Dictionary<string, List<GameObject>> itemList = new Dictionary<string, List<GameObject>>();
    private GameObject itemInArea;
    private Collider nearestCollider;

    private void Update()
    {
        //Ray ray = new Ray(transform.position, transform.forward);
        //Collider[] colliders = Physics.OverlapSphere(ray.origin, overlapSphereRadius, itemLayer);

        //if(colliders.Length == 0)
        //{
        //    itemPickUpImage.gameObject.SetActive(false);
        //    return;
        //}

        //nearestCollider = null;
        //float shortestDistance = float.MaxValue;

        //foreach (Collider collider in colliders) 
        //{
        //    Vector3 closestPointToRay = ClosestPointToRay(ray, collider);
        //    float distanceToCollider = Vector3.Distance(closestPointToRay, ray.direction);

        //    if (distanceToCollider < shortestDistance) 
        //    {
        //        shortestDistance = distanceToCollider;
        //        nearestCollider = collider;
        //    }
        //}

        //Vector3 screenPoint = Camera.main.WorldToScreenPoint(nearestCollider.transform.position);
        //if (screenPoint.z > 0) 
        //{
        //    itemPickUpImage.gameObject.SetActive(true);
        //    itemPickUpImage.position = screenPoint;
        //}
        //else
        //{
        //    itemPickUpImage.gameObject.SetActive(false);
        //}
    }

    private Vector3 ClosestPointToRay(Ray ray, Collider collider)
    {
        Vector3 pointToRay = collider.transform.position - ray.origin;
        float projectionLength = Vector3.Distance(pointToRay, ray.direction);

        return ray.origin + ray.direction.normalized * projectionLength;
    }

    public void ButtonClicked()
    {
        nearestCollider.gameObject.GetComponent<IBaseInteractableObject>().HandlePlayerInteraction();
    }

    public void PickUpItem()
    {
        if(currentCapacity + nearestCollider.GetComponent<BaseInteractableObject>().GetWeight() > totalCapacity || itemList.Count >= fullSlotCount)
        {
            Debug.Log("Overloaded!! Can't add more to bag!");
            return;
        }

        if (itemList.ContainsKey(nearestCollider.name)) 
        {
            itemList[nearestCollider.name].Add(nearestCollider.gameObject);
        }
        else
        {
            List<GameObject> newItem = new List<GameObject>();
            newItem.Add(nearestCollider.gameObject);
            itemList.Add(nearestCollider.name, newItem);
        }
        currentCapacity += nearestCollider.GetComponent<BaseInteractableObject>().GetWeight();
        Destroy(nearestCollider.gameObject);

        string builder = "";
        foreach (var item in itemList) 
        {
            builder += "Key : " + item.Key + " Value : " + item.Value + "\n";
        };
    }

    public void ConsumeItem(GameObject itemName)
    {
        if (itemList.ContainsKey(itemName.name))
        {
            itemList[itemName.name].Remove(itemList[itemName.name][itemList[itemName.name].Count - 1]);
            if (itemList[itemName.name].Count <= 0)
            {
                itemList.Remove(itemName.name);
            }
        }
    }

    public void UseItemTool(string toolName)
    {
        if (itemList.ContainsKey(toolName))
            //Right Hand this item
            //Need Discussion for now
            Debug.Log(toolName);
    }
}
