using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryPoint : MonoBehaviour
{
    public bool isPackageDeliverd = false;
    [SerializeField] private int pointsToAdd = 10;

    private DeliveryHouse deliveryHouse;

    public delegate void DeliveryPointScoreAction(int points);
    public static event DeliveryPointScoreAction OnPackageScoreDelivered;

    private void Awake()
    {
        deliveryHouse = GetComponentInParent<DeliveryHouse>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent(out Package package) && !isPackageDeliverd)
        {
            OnPackageScoreDelivered?.Invoke(pointsToAdd);
            deliveryHouse.DisableDeliveryPoints();
        }
    }
}
