using UnityEngine;
using System.Collections;

public class LevelController : MonoBehaviour
{
    public float levelTime = 0;
    public float totalLevelTime = 0;
    public GameObject paaseMenu;

    private bool isPaused = false;

    void Update()
    {
        levelTime += Time.deltaTime;
        totalLevelTime += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                paaseMenu.SetActive(true);
                isPaused = true;
                Time.timeScale = 0f;
            }
            else
            {
                paaseMenu.SetActive(false);
                isPaused = false;
                Time.timeScale = 1f;
            }
        }
    }
}
