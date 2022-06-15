using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif


public class GameManager : MonoBehaviour
{
    public static GameManager gameManager { get; set; }
    public string playerName { get; set; }
    public float playerMoney { get; set; }
    public float playerScore { get; set; }
    float basePay = 25;
    public float highScore;
    public string highScoreName;
    private bool m_GameOver = false;

    void Awake()
    {
        //If instance isn't empty destroy the new Instance of GameManager
        if (gameManager != null)
        {
            Destroy(gameObject);
            return;
        }
        playerMoney = 1100;
        playerScore = 0;
        //Stores “this” instance of Game manager from any other script and get a link to that specific instance of it
        gameManager = this;
        //Marks the MainManager GameObject attached to this script not to be destroyed when the scene changes.
        DontDestroyOnLoad(gameObject);
        LoadHighScore();
    }

    public void CalculatePayment(float vehicleCarry)
    {
        //Update player money with <basePay> times the packages delivered
        playerMoney += basePay * vehicleCarry;
        //Update player score with the packages delivered
        playerScore += vehicleCarry;
    }


    public void Exit()
    {
        //Save Score and settings on Exit
        GameManager.gameManager.GameOver();
        //Exit the Game
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
            Application.Quit(); // original code to quit Unity player
#endif
    }

    //[System.Serializable] required for JsonUtility, it will only transform things to JSON if they are tagged as Serializable.
    [System.Serializable]
    class SaveData
    {
        public string highScoreName;
        public float highScore;
    }
    public void SaveHighScore()
    {
        //Create instance of Savedata and add information to it
        SaveData data = new SaveData();
        data.highScoreName = highScoreName;
        data.highScore = highScore;

        //Convert information in <data> to JSON
        string json = JsonUtility.ToJson(data);
        //Write string in <json> to file. Unity method <Application.persistentDataPath>
        //Will give you a folder where you can save data that will survive between application reinstall
        // or update and append to it the filename savefile.json.
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadHighScore()
    {
        //If the save file exists read it
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            //Transform JSON back to SaveData
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            highScore = data.highScore;
            highScoreName = data.highScoreName;
        }
    }

    public void GameOver()
    {
        m_GameOver = true;
        if (GameManager.gameManager != null)
        {
            if (playerScore > GameManager.gameManager.highScore)
            {
                GameManager.gameManager.highScoreName = GameManager.gameManager.playerName;
                GameManager.gameManager.highScore = playerScore;
                GameManager.gameManager.SaveHighScore();
            }
        }

    }
}
