using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DisplayScore : MonoBehaviour
{
    public int levelNumber = 0;
    void Start()
    {
        if (!PlayerPrefs.HasKey("Level_" + levelNumber + "_score"))
        {
            PlayerPrefs.SetInt("Level_" + levelNumber + "_score", 0);
        }
        GetComponent<Text>().text = PlayerPrefs.GetInt("Level_" + levelNumber + "_score").ToString();
    }
}
