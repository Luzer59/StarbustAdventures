using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ExtraLock : MonoBehaviour
{
    private int lvl1Score = 0;
    private int lvl2Score = 0;

    public int lockLimit = 0;

    public Sprite unlockedSprite;
    public GameObject extraButton;

    void Awake()
    {

    }

    void Start()
    {
        if (!PlayerPrefs.HasKey("Level_" + 1 + "_score"))
        {
            PlayerPrefs.SetInt("Level_" + 1 + "_score", 0);
        }
        lvl1Score = PlayerPrefs.GetInt("Level_" + 1 + "_score");

        if (!PlayerPrefs.HasKey("Level_" + 2 + "_score"))
        {
            PlayerPrefs.SetInt("Level_" + 2 + "_score", 0);
        }
        lvl2Score = PlayerPrefs.GetInt("Level_" + 2 + "_score");

        if (lvl1Score + lvl2Score >= lockLimit)
        {
            GetComponent<Image>().sprite = unlockedSprite;
            extraButton.SetActive(true);
        }
    }
}
