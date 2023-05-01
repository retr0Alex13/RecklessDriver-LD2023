using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryHouse : MonoBehaviour
{
    public bool isPackageArrived;
    [SerializeField] private List<DeliveryPoint> deliveryPoints = new List<DeliveryPoint>();

    void Start()
    {
        InitializeDeliveryPoints();
    }

    private void InitializeDeliveryPoints()
    {
        foreach (Transform child in transform)
        {
            if (child.TryGetComponent(out DeliveryPoint deliveryPoint))
            {
                deliveryPoints.Add(deliveryPoint);
            }
        }
    }

    public void DisableDeliveryPoints()
    {
        foreach (DeliveryPoint point in deliveryPoints) 
        {
            point.isPackageDeliverd = true;
        }
        isPackageArrived = true;
    }
}
