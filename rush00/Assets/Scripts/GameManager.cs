using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager gm { get; private set; }

    public GameObject infos;
    public GameObject back;
    // public GameObject game;
    public Text HUD;
    public Text title;
    public Text subtitle;
    public Button first;
    public Button second;
    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
    //private	GameObject[]	enemies;
    private int enemies;

    void Start()
    {
        gm = this;
        // game.SetActive(false);
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
        enemies = GameObject.FindGameObjectsWithTag("enemy").Length;
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            title.text = "Hot Paris Paname";
            subtitle.text = "Cluster et ta soeur";
            second.onClick.AddListener(Application.Quit);
            first.onClick.AddListener(startGame);
        }
    }
    public void setHUD(string t)
    {
        HUD.text = t;
    }
    public void onePoint()
    {
        enemies--;
        if (enemies == 0)
            win();
    }
    public void startGame()
    {
        infos.SetActive(false);
        back.SetActive(false);
        SceneManager.LoadScene(1);
        // game.SetActive(true);
    }
    public void win()
    {
        print("GG");
        // title.text = "T'es trop baleze mec!";
        // subtitle.text = "Non, vraiment,\nca vient du fond\ndu coeur";
        // infos.SetActive(true);
    }
    public void gameOver()
    {
        print("Looser");
        //    title.text = "T'es une merde mec";
        //     subtitle.text = "Mais bon, on t'aime bien quand meme";
        //     infos.SetActive(true);
    }

}
