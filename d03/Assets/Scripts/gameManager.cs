using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{

    //Vous pouvez directement changer ces valeurs de base dans l'inspecteur si vous voulez personnaliser votre jeu
    [HideInInspector] public int playerHp = 20;
    public int playerMaxHp = 20;
    [HideInInspector] public int playerEnergy = 300;
    public int playerStartEnergy = 300;
    public int delayBetweenWaves = 10;                  //Temps entre les vagues
    public int nextWaveEnnemyHpUp = 20;                 //Augmentation de la vie des bots a chaque vague (en %)
    public int nextWaveEnnemyValueUp = 30;      //Augmentation de l'energie donnee par les bots a chaque vague (en %)
    public int averageWavesLenght = 15;                 //Taille moyenne d'une vague d'ennemis
    public int totalWavesNumber = 20;                       // Nombre des vagues au total
    [HideInInspector] public bool lastWave = false;
    [HideInInspector] public int currentWave = 1;
    private float tmpTimeScale = 1;
    public int score = 0;
    public static gameManager gm;
    [HideInInspector] public List<GameObject> occupedTiles;
    public GameObject ShopItem1;
    public GameObject ShopItem2;
    public GameObject ShopItem3;
    public Canvas pauseMenu;
    public Canvas scoreBoard;
    public Canvas gameOverBoard;
    public Canvas hider;
    public Text scoreText;
    public Text rankText;
    public Button retryButton;
    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public bool isGamePausedInMenu;
    public bool isLevelEnded;

    //Singleton basique  : Voir unity design patterns sur google.
    void Awake()
    {
        if (gm == null)
            gm = this;
    }

    void Start()
    {
        Time.timeScale = 1;
        playerHp = playerMaxHp;
        playerEnergy = playerStartEnergy;
        isGamePausedInMenu = false;
        isLevelEnded = false;
        Cursor.SetCursor(cursorTexture, Vector2.zero, cursorMode);
        occupedTiles = new List<GameObject>();
        retryButton.onClick.AddListener(() => resetLevel());
    }

    void Update()
    {
        print(currentWave);
        if (gm.playerEnergy < 80)
        {
            var image = ShopItem1.GetComponent<Image>();
            var tmpColor = image.color;
            tmpColor.a = 0.4f;
            image.color = tmpColor;
        }
        if (gm.playerEnergy < 50)
        {
            var image = ShopItem2.GetComponent<Image>();
            var tmpColor = image.color;
            tmpColor.a = 0.4f;
            image.color = tmpColor;
        }
        if (gm.playerEnergy < 100)
        {
            var image = ShopItem3.GetComponent<Image>();
            var tmpColor = image.color;
            tmpColor.a = 0.4f;
            image.color = tmpColor;
        }

        if (gm.playerEnergy > 80)
        {
            var image = ShopItem1.GetComponent<Image>();
            var tmpColor = image.color;
            tmpColor.a = 1f;
            image.color = tmpColor;
        }
        if (gm.playerEnergy > 50)
        {
            var image = ShopItem2.GetComponent<Image>();
            var tmpColor = image.color;
            tmpColor.a = 1f;
            image.color = tmpColor;
        }
        if (gm.playerEnergy > 100)
        {
            var image = ShopItem3.GetComponent<Image>();
            var tmpColor = image.color;
            tmpColor.a = 1f;
            image.color = tmpColor;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGamePausedInMenu)
                return;
            pause(true);
            pauseMenu.enabled = true;
            isGamePausedInMenu = !isGamePausedInMenu;
        }
        nextLevel();
    }

    //Pour mettre le jeu en pause
    public void pause(bool paused)
    {
        if (playerHp <= 0)
        {
            Time.timeScale = 0;
            return;
        }
        if (paused == true)
        {
            tmpTimeScale = Time.timeScale;
            Time.timeScale = 0;
        }
        else
        {
            if (tmpTimeScale == 0)
                tmpTimeScale++;
            Time.timeScale = tmpTimeScale;
        }
    }

    //Pour changer la vitesse de base du jeu
    public void changeSpeed(float speed)
    {
        Time.timeScale = speed;
    }

    //Le joueur perd de la vie
    public void damagePlayer(int damage)
    {
        playerHp -= damage;
        if (playerHp <= 0)
            gameOver();
        else
            Debug.Log("Il reste au joueur " + playerHp + "hp");
    }

    public void resetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //On pause le jeu en cas de game over
    public void gameOver()
    {
        Time.timeScale = 0;
        gameOverBoard.enabled = true;
        Debug.Log("Game Over");
    }

    void nextLevel()
    {
        if (isLevelEnded)
        {
            hider.enabled = true;
            scoreBoard.enabled = true;
            isGamePausedInMenu = true;
            scoreText.text = "Score: " + score.ToString();
            if (score < 10)
                rankText.text = "Rank: Tu sais où est le clavier ?";
            else if (score < 100000)
                rankText.text = "Rank: D";
            else if (score < 50000)
                rankText.text = "Rank: C";
            else if (score < 100000)
                rankText.text = "Rank: B";
            else if (score < 150000)
                rankText.text = "Rank: A";
            else if (score < 200000)
                rankText.text = "Rank: S";
            else if (score < 250000)
                rankText.text = "Rank: S+";
        }
    }
}
