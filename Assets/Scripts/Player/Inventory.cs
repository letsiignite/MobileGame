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

    //private Vector3 ClosestPointToRay(Ray ray, Collider collider)
    //{
    //    Vector3 pointToRay = collider.transform.position - ray.origin;
    //    float projectionLength = Vector3.Distance(pointToRay, ray.direction);

    //    return ray.origin + ray.direction.normalized * projectionLength;
    //}

    /// <summary>
    /// Used in UI Buttons when buttons are Clicked
    /// </summary>
    public void ButtonClicked()
    {
        nearestCollider.gameObject.GetComponent<IBaseInteractableObject>().HandlePlayerInteraction();
    }
    
    /// <summary>
    /// Used to Add items to list
    /// </summary>
    /// <param name="item"></param>
    public void PickUpItem(GameObject item)
    {
        if (currentCapacity + item.GetComponent<BaseInteractableObject>().GetWeight() < totalCapacity)
        {
            if (itemList.ContainsKey(item.name))
            {
                itemList[item.name].Add(item);
            }
            else
            {
                List<GameObject> newItem = new List<GameObject>(){ item };
                itemList.Add(item.name, newItem);
            }
            currentCapacity += item.GetComponent<BaseInteractableObject>().GetWeight();
            Destroy(item);
        }
        else
        {
            Debug.Log("OverLoaded!! Try losing some weight fatty");
        }
    }

    /// <summary>
    /// Used to remove Item from lists
    /// </summary>
    /// <param name="itemName"></param>
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
