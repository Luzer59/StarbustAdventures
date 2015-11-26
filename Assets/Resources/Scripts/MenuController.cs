using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    private int returnScene = -1;

    public void ChangeLevel(int index)
    {
        if (GetComponent<PlayerController>())
        {
            if (!PlayerPrefs.HasKey("Score"))
            {
                PlayerPrefs.SetInt("Score", 0);
            }
            if (GetComponent<PlayerController>().score > PlayerPrefs.GetInt("Score"))
            {
                PlayerPrefs.SetInt("Score", GetComponent<PlayerController>().score);
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
        if (!GetComponent<PlayerController>())
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
            }
        }
    }
}
