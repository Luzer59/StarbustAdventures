﻿using UnityEngine;
using System.Collections;

public class GameObjectPublicValueHandler : MonoBehaviour
{
    public bool canBeHit = false;
    public int health = 1;
    public int scoreValue = 1;

    [HideInInspector]
    public GameObject gameController;

    void Awake()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController");
    }

    public void CheckHealth()
    {
        if (health <= 0)
        {
            if (GetComponent<DeathExplosionEffect>())
            {
                GetComponent<DeathExplosionEffect>().Effect();
                if (tag == "Enemy")
                {
                    gameController.GetComponent<PlayerController>().AddScore(scoreValue);
                }
            }

            if (tag == "Player")
            {
                Application.LoadLevel(Application.loadedLevel);
            }
            else if (tag == "Enemy")
            {
                gameController.GetComponent<PlayerController>().AddMultiplier();
            }

            gameObject.SetActive(false);
        }
        else
        {
            if (GetComponent<HitEffect>())
            {
                StartCoroutine(GetComponent<HitEffect>().Effect());
                
            }
        }
    }
}