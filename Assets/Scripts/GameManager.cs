using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    public string playerName;
    public float playerMoney;
    float basePay = 5;
    
    void Awake()
    {
        //If instance isn't empty destroy the new Instance of GameManager
        if (gameManager != null)
        {
            Destroy(gameObject);
            return;
        }
        //Stores “this” in the class member Instance
        //You can now call MainManager.Instance from any other script and get a link to that specific instance of it
        gameManager = this;
        //Marks the MainManager GameObject attached to this script not to be destroyed when the scene changes.
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CalculatePayment(float vehicleCarry)
    {
        playerMoney += basePay*vehicleCarry;
    }
}
