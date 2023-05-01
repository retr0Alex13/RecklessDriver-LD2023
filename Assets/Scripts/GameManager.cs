using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int playerScore = 0;
    [SerializeField] private List<DeliveryHouse> deliveryHouses = new List<DeliveryHouse>();

    public delegate void GameManagerAction(int playerScore);
    public static event GameManagerAction OnAddingScore;

    private void OnEnable()
    {
        DeliveryPoint.OnPackageScoreDelivered += AddScore;
    }

    private void OnDisable()
    {
        DeliveryPoint.OnPackageScoreDelivered -= AddScore;
    }

    private void Awake()
    {
        playerScore = 0;
        InitializeHouses();
    }

    private void InitializeHouses()
    {
        foreach (DeliveryHouse deliveryHouse in FindObjectsOfType<DeliveryHouse>())
        {
            deliveryHouses.Add(deliveryHouse);
        }
    }

    public void AddScore(int points)
    {
        if(points < 0)
        {
            return;
        }
        playerScore += points;
        OnAddingScore?.Invoke(playerScore);
    }

}
