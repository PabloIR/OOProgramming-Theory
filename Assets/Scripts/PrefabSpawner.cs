using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabSpawner : MonoBehaviour
{
    [SerializeField] GameObject truckPrefab;
    [SerializeField] GameObject eTruckPrefab;
    [SerializeField] GameObject bikePrefab;
    public void AddTruck()
    {
        //Add a truck for 1000 credits
        Instantiate(truckPrefab);
        GameManager.gameManager.playerMoney -= 1000;
    }
    public void AddETruck()
    {
        //Add a truck for 1000 credits
        Instantiate(eTruckPrefab);
        GameManager.gameManager.playerMoney -= 2500;
    }
    public void AddBike()
    {
        //Add a bike for 250 credits
        Instantiate(bikePrefab);
        GameManager.gameManager.playerMoney -= 250;
    }
}
