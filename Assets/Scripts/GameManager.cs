using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int playerScore = 0;
    [SerializeField] private int deliveryHousesAmount = 2;
    [SerializeField] private List<DeliveryHouse> deliveryHouses = new List<DeliveryHouse>();
    [SerializeField] private float speedIncreasing = 2f;
    [SerializeField] private float gameOverReloadSeconds = 3f;

    [SerializeField] private BikeMovement playerMovement;

    public delegate void GameManagerUIAction(int playerScore);
    public static event GameManagerUIAction OnAddingScore;

    private void OnEnable()
    {
        DeliveryPoint.OnPackageScoreDelivered += AddScore;
        DeliveryHouse.OnPackageArrived += CheckAndRemoveHouseFromDelivery;
        PlayerFail.OnPlayerFail += GameOver;
    }

    private void OnDisable()
    {
        DeliveryPoint.OnPackageScoreDelivered -= AddScore;
        DeliveryHouse.OnPackageArrived -= CheckAndRemoveHouseFromDelivery;
        PlayerFail.OnPlayerFail -= GameOver;
    }

    private void Awake()
    {
        playerScore = 0;
        InitializeHouses();
    }
    private List<DeliveryHouse> GetHouses()
    {
        List<DeliveryHouse> houses = new List<DeliveryHouse>();
        foreach(DeliveryHouse deliveryHouse in FindObjectsOfType<DeliveryHouse>())
        {
            houses.Add(deliveryHouse);
        }
        return houses;
    }

    private void InitializeHouses()
    {
        List<DeliveryHouse> allHouses = GetHouses();
        System.Random random = new System.Random();

        for (int i = 0; i < deliveryHousesAmount; i++)
        {
            int randomHouseIndex = random.Next(allHouses.Count);
            DeliveryHouse randomHouse = allHouses[randomHouseIndex];
            if (!deliveryHouses.Contains(randomHouse))
            {
                deliveryHouses.Add(randomHouse);
                randomHouse.InitializeDeliveryPoints();
                randomHouse.ActivateDeliveryPoints();
            }
            else
            {
                // If the random house is already in the list, generate a new random index
                i--;
            }
        }
    }

    public void CheckAndRemoveHouseFromDelivery()
    {
        foreach(DeliveryHouse house in  deliveryHouses.ToList()) 
        {
            if(house.isPackageArrived)
            {
                deliveryHouses.Remove(house);
            }
        }
        if(deliveryHouses.Count <= 0)
        {
            InitializeHouses();
            playerMovement.speed += speedIncreasing;
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

    private void GameOver()
    {
        StartCoroutine(ReloadScene());
    }

    private IEnumerator ReloadScene()
    {
        yield return new WaitForSeconds(gameOverReloadSeconds);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
