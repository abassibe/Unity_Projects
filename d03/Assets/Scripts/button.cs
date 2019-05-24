using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class button : MonoBehaviour
{
    public Button startButton, exitButton;

    void Start()
    {
        //Calls the TaskOnClick/TaskWithParameters/ButtonClicked method when you click the Button
        startButton.onClick.AddListener(() => startGame());
        exitButton.onClick.AddListener(() => exitGame());
    }
    void startGame()
    {
        SceneManager.LoadScene(1);
    }
    void exitGame()
    {
        Application.Quit();
    }
}
