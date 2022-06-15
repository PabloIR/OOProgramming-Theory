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
    [SerializeField] Text highScoreText;
    [SerializeField] Text hSNameText;
    // Start is called before the first frame update
    void Start()
    {
        playerNameText.text = GameManager.gameManager.playerName;
        highScoreText.text = "HighScore: " + GameManager.gameManager.highScore.ToString();
        hSNameText.text = "Name: " + GameManager.gameManager.highScoreName;

    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Delivered: " + GameManager.gameManager.playerScore;
        moneyText.text = "Money: " + GameManager.gameManager.playerMoney.ToString("F2");
    }

    public void Exit()
    {
        GameManager.gameManager.Exit();
    }
}
