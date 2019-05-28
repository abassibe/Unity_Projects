using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class progressBar : MonoBehaviour
{
    private float barDisplay;

    private Vector2 pos;

    private Vector2 size;

    static public float detectionPercent = 0.0f;

    private GUIStyle currentStyle = null;

    private float red = 0.2f;

    private bool isTriggered = false;

    private bool isGameover = false;

    private float timeBeforeReload;

    static public bool isHide = false;

    void OnGUI()
    {
        InitStyles();
        pos = new Vector2(Screen.width / 3, Screen.height - (Screen.height / 8));
        size = new Vector2(Screen.width / 3, Screen.height / 20);

        GUI.BeginGroup(new Rect(pos.x, pos.y, size.x, size.y));
        GUI.Box(new Rect(0, 0, size.x, size.y), Convert.ToInt32(detectionPercent).ToString() + "%");
        GUI.Box(new Rect(0, 0, size.x, size.y), "");

        GUI.BeginGroup(new Rect(0, 0, size.x * barDisplay, size.y));
        GUI.Box(new Rect(0, 0, size.x, size.y), "", currentStyle);
        GUI.EndGroup();
        GUI.EndGroup();
    }

    void Update()
    {
        if (isGameover)
        {
            if (timeBeforeReload + 10 < Time.time)
            {
                detectionPercent = 0;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            else
                return;
        }
        barDisplay = detectionPercent * 0.01f;
        if (!isTriggered && detectionPercent > 0)
            detectionPercent -= 0.2f;
    }

    void OnTriggerStay(Collider col)
    {
        if (isGameover || isHide)
            return;
        Vector3 direction = (col.gameObject.transform.position - transform.position).normalized;
        Vector3 origin = transform.position + direction;
        RaycastHit hit;
        Physics.Raycast(origin, direction, out hit, 20);
        if ((hit.collider != null && hit.collider.tag == "cam") || (col.tag == "ligth"))
        {
            if (detectionPercent < 100)
            {
                detectionPercent += 0.5f;
                if (detectionPercent >= 75)
                    red = 1.0f;
                else if ((int)(detectionPercent % 1) == 0 && red < 1)
                    red += 0.002f;
            }
            if (detectionPercent >= 100)
            {
                timeBeforeReload = Time.time;
                isGameover = true;
            }
        }
    }

    private void InitStyles()
    {
        currentStyle = new GUIStyle(GUI.skin.box);
        currentStyle.normal.background = MakeTex(2, 2, new Color(red, 0.1f, 0.1f, 0.3f));
    }

    private Texture2D MakeTex(int width, int height, Color col)
    {
        Color[] pix = new Color[width * height];
        for (int i = 0; i < pix.Length; ++i)
            pix[i] = col;
        Texture2D result = new Texture2D(width, height);
        result.SetPixels(pix);
        result.Apply();
        return result;
    }

}
