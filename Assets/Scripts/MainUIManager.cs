using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUIManager : MonoBehaviour
{
    [Header("UI Objects")]
    [SerializeField] Text scoreText;
    [SerializeField] Text moneyText;
    [SerializeField] Text playerNameText;
    // Start is called before the first frame update
    void Start()
    {
        playerNameText.text = GameManager.gameManager.playerName;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Delivered: " + GameManager.gameManager.playerScore;
        moneyText.text = "Money: " + GameManager.gameManager.playerMoney.ToString("F2");
    }
}
