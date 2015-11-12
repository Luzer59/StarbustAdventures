using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public int score = 0;
    public float scoreMultiplier = 1.0f;
    public int lives = 3;
    public int bombs = 3;

    private float timer = 0;
    private float timerMax = 3f;

    public void AddMultiplier()
    {
        scoreMultiplier += 0.05f;
        timer = 0f;
    }

	public void AddScore(int scoreValue)
    {
        score += (int)(scoreMultiplier * scoreValue);
    }

    void Update()
    {
        if (scoreMultiplier > 1f)
        {
            timer += Time.deltaTime;

            if (timer > timerMax)
            {
                scoreMultiplier = 1f;
                timer = 0;
            }
        }
    }
}
