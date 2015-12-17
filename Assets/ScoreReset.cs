using UnityEngine;
using System.Collections;

public class ScoreReset : MonoBehaviour
{
    private float timer = 0f;

	void Update ()
    {
	    if (Input.GetKey(KeyCode.R))
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0f;
        }
        if (timer >= 3f)
        {
            print("Score reset");
            PlayerPrefs.SetInt("Level_" + 1 + "_score", 0);
            PlayerPrefs.SetInt("Level_" + 2 + "_score", 0);
        }
	}
}
