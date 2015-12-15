using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    private int returnScene = -1;
    [SerializeField]
    private bool anyKeyQuit = false;
    private LevelController lc;
    private PlayerController pc;

    void Awake()
    {
        lc = GetComponent<LevelController>();
        pc = GetComponent<PlayerController>();
    }

    public void ChangeLevel(int index)
    {
        if (index == 2 || index == 4)
        {
            GameObject ac = GameObject.Find("AudioController");
            if (ac)
            {
                Destroy(ac);
            }
        }
        print("change level");
        if (pc)
        {
            // bonuses here!
            print("change level passed");
            if (!PlayerPrefs.HasKey("Level_" + lc.levelNumber + "_score"))
            {
                PlayerPrefs.SetInt("Level_" + lc.levelNumber + "_score", 0);
            }
            if (pc.score > PlayerPrefs.GetInt("Level_" + lc.levelNumber + "_score"))
            {
                print("[Level_" + lc.levelNumber + "_score] is set to " + pc.score.ToString());
                PlayerPrefs.SetInt("Level_" + lc.levelNumber + "_score", pc.score);
            }
        }

        if (index == -1)
        {
            Application.Quit();
        }
        else
        {
            Application.LoadLevel(index);
        }
    }

    void Update()
    {
        if (!pc)
        {
            AudioSource ac = GetComponent<AudioSource>();
            if (anyKeyQuit)
            {
                if (Input.anyKeyDown)
                {
                    if (returnScene == -1)
                    {
                        Application.Quit();
                    }
                    else
                    {
                        ChangeLevel(returnScene);
                    }
                    if (ac)
                    {
                        ac.Play();
                    }
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    if (returnScene == -1)
                    {
                        Application.Quit();
                    }
                    else
                    {
                        ChangeLevel(returnScene);
                    }
                    if (ac)
                    {
                        ac.Play();
                    }
                }
            }
        }
    }
}
