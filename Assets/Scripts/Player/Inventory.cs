using Interactable;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [Header("Item")]
    [SerializeField] private Button itemPickUpButton;
    [SerializeField] private RectTransform itemPickUpImage;
    [SerializeField] private LayerMask itemLayer;
    //[SerializeField] private int fullSlotCount = 2;
    [SerializeField] private int totalCapacity = 10;
    [SerializeField] private int currentCapacity = 0;

    [Header("Colliders")]
    //[SerializeField] private float overlapSphereRadius = 1;

    [Header("Hands")]
    [SerializeField] private GameObject leftHand;
    [SerializeField] private GameObject rightHand;
    
    private Dictionary<string, List<GameObject>> itemDict = new Dictionary<string, List<GameObject>>();
    private GameObject itemInArea;
    private Collider nearestCollider;

    private void Update()
    {
        //Ray ray = new Ray(transform.position, transform.forward);
        //Collider[] colliders = Physics.OverlapSphere(ray.origin, overlapSphereRadius, itemLayer);

        //if (colliders.Length == 0)
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

    private Vector3 FindSpawnCenter()
    {
        Vector3 center = (leftHand.transform.position + rightHand.transform.position)/2 + (Vector3.up * 0.5f) + new Vector3(Random.Range(-.1f, .1f), Random.Range(-.1f, .1f), Random.Range(-.1f, .1f));
        return center;
    }

    //private Vector3 ClosestPointToRay(Ray ray, Collider collider)
    //{
    //    Vector3 pointToRay = collider.transform.position - ray.origin;
    //    float projectionLength = Vector3.Distance(pointToRay, ray.direction);

    //    return ray.origin + ray.direction.normalized * projectionLength;
    //}

    /// <summary>
    /// UI Button to active Inventory Panel
    /// </summary>
    public void InventoryButtonClicked()
    {
        leftHand.SetActive(true);
        rightHand.SetActive(true);
        foreach (var item in itemDict.Keys)
        {
            GameObject itemGO = itemDict[item][0];
            itemDict[item][0].transform.position = FindSpawnCenter();
            itemGO.transform.localScale *= 0.25f;
            itemGO.SetActive(true);
        }
    }
    
    /// <summary>
    /// Used to Add items to list
    /// </summary>
    /// <param name="item"></param>
    public void PickUpItem(GameObject item)
    {
        if (currentCapacity + item.GetComponent<BaseInteractableObject>().GetWeight() < totalCapacity)
        {
            if (itemDict.ContainsKey(item.name))
            {
                itemDict[item.name].Add(item);
            }
            else
            {
                List<GameObject> newItem = new List<GameObject>(){ item };
                itemDict.Add(item.name, newItem);
            }
            currentCapacity += item.GetComponent<BaseInteractableObject>().GetWeight();
            item.SetActive(false);
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
    public bool ConsumeItem(GameObject itemName)
    {
        if (itemDict.ContainsKey(itemName.name))
        {
            itemDict[itemName.name].Remove(itemDict[itemName.name][itemDict[itemName.name].Count - 1]);
            if (itemDict[itemName.name].Count <= 0)
            {
                itemDict.Remove(itemName.name);
            }
            return true;
        }
        return false;
    }

    public void UseItemTool(string toolName)
    {
        if (itemDict.ContainsKey(toolName))
            //Right Hand this item
            //Need Discussion for now
            Debug.Log(toolName);
    }
}
