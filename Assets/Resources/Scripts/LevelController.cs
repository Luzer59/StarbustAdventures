using UnityEngine;
using System.Collections;

public class LevelController : MonoBehaviour
{
    public float levelTime = 0;
    public float totalLevelTime = 0;

    void Update()
    {
        levelTime += Time.deltaTime;
        totalLevelTime += Time.deltaTime;
    }
}
