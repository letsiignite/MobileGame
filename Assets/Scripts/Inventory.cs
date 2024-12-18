using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [Header("Item Pick up")]
    [SerializeField] private Button itemPickUpButton;
    [SerializeField] private RectTransform itemPickUpImage;
    [SerializeField] private LayerMask itemLayer;

    [Header("Colliders")]
    [SerializeField] private float overlapSphereRadius = 5;
    
    private Dictionary<string, int> itemList = new Dictionary<string, int>();
    private GameObject itemInArea;
    private Collider nearestCollider;

    private void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        Collider[] colliders = Physics.OverlapSphere(ray.origin, overlapSphereRadius, itemLayer);

        if(colliders.Length == 0)
        {
            itemPickUpImage.gameObject.SetActive(false);
            return;
        }

        nearestCollider = null;
        float shortestDistance = float.MaxValue;

        foreach (Collider collider in colliders) 
        {
            Vector3 closestPointToRay = ClosestPointToRay(ray, collider);
            float distanceToCollider = Vector3.Distance(closestPointToRay, ray.direction);

            if (distanceToCollider < shortestDistance) 
            {
                shortestDistance = distanceToCollider;
                nearestCollider = collider;
            }
        }

        Vector3 screenPoint = Camera.main.WorldToScreenPoint(nearestCollider.transform.position);
        if (screenPoint.z > 0) 
        {
            itemPickUpImage.gameObject.SetActive(true);
            itemPickUpImage.position = screenPoint;
        }
        else
        {
            itemPickUpImage.gameObject.SetActive(false);
        }
    }

    private Vector3 ClosestPointToRay(Ray ray, Collider collider)
    {
        Vector3 pointToRay = collider.transform.position - ray.origin;
        float projectionLength = Vector3.Distance(pointToRay, ray.direction);

        return ray.origin + ray.direction.normalized * projectionLength;
    }

    public void PickUpItem()
    {
        if (itemList.ContainsKey(nearestCollider.name)) 
        {
            itemList[nearestCollider.name] += 1;
        }
        else
        {
            itemList.Add(nearestCollider.name, 1);
        }
        Destroy(nearestCollider.gameObject);
        Debug.Log(itemList.Count);
        string builder = "";
        foreach (KeyValuePair<string, int> item in itemList) 
        {
            builder += "Key : " + item.Key + " Value : " + item.Value + "\n";
        };
        Debug.Log(builder);
    }
}
