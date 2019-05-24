using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class pauseScript : MonoBehaviour
{
    public gameManager gm;
    public Canvas confirmExitMenu;
    public Button nextLevelButton, continueButton, exitButton, confirmExitButton, denyExitButton, speedButton, superSpeedButton, pauseGameButton, resumeGameButton;
    // Use this for initialization
    void Start()
    {
        continueButton.onClick.AddListener(() => resumeGame());
        exitButton.onClick.AddListener(() => confirmExit());
        confirmExitButton.onClick.AddListener(() => returnToMainMenu());
        denyExitButton.onClick.AddListener(() => denyExit());
        speedButton.onClick.AddListener(() => speedGame());
        superSpeedButton.onClick.AddListener(() => superSpeedGame());
        pauseGameButton.onClick.AddListener(() => pauseGameWithButton());
        resumeGameButton.onClick.AddListener(() => resumeGameWithButton());
        nextLevelButton.onClick.AddListener(() => nextLevel());
    }
    void resumeGame()
    {
        gm.pause(false);
        gm.pauseMenu.enabled = false;
        gm.isGamePausedInMenu = false;

    }

    void confirmExit()
    {
        confirmExitMenu.enabled = true;
    }

    void denyExit()
    {
        confirmExitMenu.enabled = false;
    }

    void returnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    void speedGame()
    {
        if (gm.isGamePausedInMenu || gm.playerHp <= 0)
            return;
        gm.changeSpeed(2f);
    }

    void superSpeedGame()
    {
        if (gm.isGamePausedInMenu || gm.playerHp <= 0)
            return;
        gm.changeSpeed(4f);
    }

    void resumeGameWithButton()
    {
        if (gm.isGamePausedInMenu || gm.playerHp <= 0)
            return;
        gm.pause(false);
    }

    void pauseGameWithButton()
    {
        if (gm.isGamePausedInMenu || gm.playerHp <= 0)
            return;
        gm.pause(true);
    }

    void nextLevel()
    {
        int id = SceneManager.GetActiveScene().buildIndex;
        if (id == 1)
        {
            SceneManager.LoadScene(2);
        }
        else if (id + 1 < SceneManager.sceneCount)
            SceneManager.LoadScene(id + 1);
    }
}
