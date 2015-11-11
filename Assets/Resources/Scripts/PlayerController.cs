using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public int score = 0;
    public int scoreMultiplier = 1;
    public int lives = 3;
    public int bombs = 3;

    private float timer = 0;
    private float timerMax = 0.7f;

	public void AddScore(int scoreValue)
    {
        score += scoreMultiplier * scoreValue;
        timer = 0;
        scoreMultiplier++;   
    }

    void Update()
    {
        if (scoreMultiplier > 1)
        {
            timer += Time.deltaTime;

            if (timer > timerMax)
            {
                scoreMultiplier--;
                timer = 0;
            }
        }
    }
}
