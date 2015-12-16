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

    public AudioClip dashFullClip;
    public AudioClip multiplierEmptyClip;
    private AudioSource ac;
    private float lastDash = 0f;
    private float lastMulti = 1f;

    void Awake()
    {
        ac = GetComponent<AudioSource>();
    }

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
            if (lastDash < 1f)
            {
                ac.PlayOneShot(dashFullClip);
            }
            dash = 1f;
        }
        lastDash = dash;

        if (scoreMultiplier > 1f)
        {
            timer -= Time.deltaTime * timerSpeed;

            multimeter.fillAmount = timer;

            if (timer <= 0f)
            {
                scoreMultiplier = 1;
                timer = 0;
                if (lastMulti > 0f)
                {
                    ac.PlayOneShot(multiplierEmptyClip);
                }
            }
            lastMulti = timer;
        }
    }
}
