using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryHouse : MonoBehaviour
{
    public bool isPackageArrived = false;
    public bool isActive = false;
    [SerializeField] private List<DeliveryPoint> deliveryPoints = new List<DeliveryPoint>();

    public delegate void DeliveryHouseAction();
    public static event DeliveryHouseAction OnPackageArrived;

    public void InitializeDeliveryPoints()
    {
        deliveryPoints.Clear();
        foreach (Transform child in transform)
        {
            if (child.TryGetComponent(out DeliveryPoint deliveryPoint))
            {
                deliveryPoints.Add(deliveryPoint);
            }
        }
    }

    public void ActivateDeliveryPoints()
    {
        foreach (Transform child in transform)
        {
            if (child.TryGetComponent(out DeliveryPoint deliveryPoint))
            {
                deliveryPoint.isPackageDeliverd = false;
                ChangePointMaterialAlpha(deliveryPoint, 0.1f);
            }
        }
        isPackageArrived = false;
        isActive = true;
    }

    private void ChangePointMaterialAlpha(DeliveryPoint deliveryPoint, float colorAlpha)
    {
        Renderer renderer = deliveryPoint.GetComponent<Renderer>();
        Material material = renderer.material;
        Color color = material.color;
        color.a = colorAlpha;
        material.color = color;
    }

    public void DisableDeliveryPoints()
    {
        foreach (DeliveryPoint point in deliveryPoints) 
        {
            point.isPackageDeliverd = true;
            ChangePointMaterialAlpha(point, 0);
        }
        isPackageArrived = true;
        isActive = false;
        OnPackageArrived?.Invoke();
    }
}
