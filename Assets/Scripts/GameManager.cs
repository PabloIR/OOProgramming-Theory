using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    public string playerName;
    public float playerMoney;
    public float playerScore;
    float basePay = 25;


    void Awake()
    {
        //If instance isn't empty destroy the new Instance of GameManager
        if (gameManager != null)
        {
            Destroy(gameObject);
            return;
        }
        //Stores “this” instance of Game manager from any other script and get a link to that specific instance of it
        gameManager = this;
        //Marks the MainManager GameObject attached to this script not to be destroyed when the scene changes.
        DontDestroyOnLoad(gameObject);
    }

    public void CalculatePayment(float vehicleCarry)
    {
        //Update player money with <basePay> times the packages delivered
        playerMoney += basePay * vehicleCarry;
        //Update player score with the packages delivered
        playerScore += vehicleCarry;
    }
}
