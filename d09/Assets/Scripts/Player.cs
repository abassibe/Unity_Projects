using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int HP = 100;

    public Text lifeText;

    public Scrollbar scrollLife;

    void Start()
    {

    }

    void Update()
    {
        lifeText.text = HP.ToString();
        scrollLife.size = (float)((float)HP / 100);
    }
}
