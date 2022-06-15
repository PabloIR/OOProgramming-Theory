using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleUIManager : MonoBehaviour
{
    [SerializeField] InputField nameInput;
    public void StartGame()
    {
        if (nameInput.text != "")
        {
            GameManager.gameManager.playerName = nameInput.text;
        }
        else
        {
            GameManager.gameManager.playerName = "Unnamed Player";
        }
        SceneManager.LoadScene(1);
    }
}
