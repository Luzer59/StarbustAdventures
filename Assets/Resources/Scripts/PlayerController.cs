using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public int score = 0;
    public int scoreMultiplier = 1;
    public int lives = 3;
    public int bombs = 3;
    public float dash = 1f;
    public float dashRecoverySpeed = 1f;

    private float timer = 0;
    private float timerMax = 5f;

    public void AddMultiplier()
    {
        scoreMultiplier += 1;
        timer = 0f;
    }

	public void AddScore(int scoreValue)
    {
        score += (int)(scoreMultiplier * scoreValue);
    }

    void Update()
    {
        dash += Time.deltaTime * dashRecoverySpeed;
        if (dash > 1f)
        {
            dash = 1f;
        }

        if (scoreMultiplier > 1f)
        {
            timer += Time.deltaTime;

            if (timer > timerMax)
            {
                scoreMultiplier = 1;
                timer = 0;
            }
        }
    }
}
