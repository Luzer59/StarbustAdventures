using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public int score = 0;
    public int scoreMultiplier = 1;
    public int lives = 3;
    public int bombs = 3;
    public float dash = 1f;
    public float dashRecoverySpeed = 1f;

    public Image multimeter;

    public float timerSpeed = 1f;
    public float timer = 0;

    public bool noDeathBonus = true;
    public bool noDashBonus = true;

    

    public void AddMultiplier()
    {
        scoreMultiplier += 1;
        timer = 1f;
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
            timer -= Time.deltaTime * timerSpeed;

            multimeter.fillAmount = timer;

            if (timer <= 0f)
            {
                scoreMultiplier = 1;
                timer = 0;
            }
        }
    }
}
