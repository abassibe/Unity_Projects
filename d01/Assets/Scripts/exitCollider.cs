using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class exitCollider : MonoBehaviour
{

    static private bool redIsPlaced = false;
    static private bool yellowIsPlaced = false;
    static private bool blueIsPlaced = false;

    void Update()
    {
		if (redIsPlaced && yellowIsPlaced && blueIsPlaced)
        {
            int indexScene = SceneManager.GetActiveScene().buildIndex;
            if (indexScene + 1 < SceneManager.sceneCountInBuildSettings)
                SceneManager.LoadScene(indexScene + 1);
            redIsPlaced = false;
            yellowIsPlaced = false;
            blueIsPlaced = false;
        }
    }

    void OnTriggerEnter2D(Collider2D obj)
    {
		if (obj.name == "red" && gameObject.name == "red_exit")
			redIsPlaced = true;
		else if (obj.name == "yellow" && gameObject.name == "yellow_exit")
			yellowIsPlaced = true;
		else if (obj.name == "blue" && gameObject.name == "blue_exit")
			blueIsPlaced = true;
    }

    void OnTriggerExit2D(Collider2D obj)
    {
		if (obj.name == "red" && gameObject.name == "red_exit")
			redIsPlaced = false;
		else if (obj.name == "yellow" && gameObject.name == "yellow_exit")
			yellowIsPlaced = false;
		else if (obj.name == "blue" && gameObject.name == "blue_exit")
			blueIsPlaced = false;
    }
}
