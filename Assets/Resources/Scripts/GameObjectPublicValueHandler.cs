using UnityEngine;
using System.Collections;

public class GameObjectPublicValueHandler : MonoBehaviour
{
    public bool canBeHit = false;
    public int health = 1;
    public int scoreValue = 1;
    public bool invincible = false;

    public ParticleSystem[] partikkelz;

    [HideInInspector]
    public GameObject gameController;
    private SpriteRenderer sr;
    [HideInInspector]
    public PlayerController pc;
    [HideInInspector]
    public LevelController lc;

    public int dialogueToActivate;
    public float timePassed = -1;

    public AudioClip playerBoomClip;
    public AudioClip enemyBoomClip;

    private GameObject instance;
    private int scorePopUpPoolIndex = 1;
    private PoolManager poolManager;
    private AudioSource ac;

    void Awake()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController");
        sr = GetComponentInChildren<SpriteRenderer>();
        pc = gameController.GetComponent<PlayerController>();
        lc = gameController.GetComponent<LevelController>();
        poolManager = GameObject.FindGameObjectWithTag("PoolController").GetComponent<PoolManager>();

        ac = gameObject.AddComponent<AudioSource>();


    //    ac.clip = enemyBoomClip;
     //   ac.Play();

        //ac = GetComponent<AudioSource>();
    }

    public void CheckHealth()
    {
        if (!invincible)
        {
            health--;

            if (health <= 0)
            {
                if (GetComponent<DeathExplosionEffect>())
                {
                    GetComponent<DeathExplosionEffect>().Effect();
                    if (tag == "Enemy")
                    {
                        pc.AddScore(scoreValue);
                        for (int i = 0; i < poolManager.effectPoolBuffer[scorePopUpPoolIndex]; i++)
                        {
                            if (!poolManager.effectPool[scorePopUpPoolIndex][i].activeInHierarchy)
                            {
                                instance = poolManager.effectPool[scorePopUpPoolIndex][i];
                                break;
                            }
                        }
                        instance.SetActive(true);
                        instance.GetComponent<ScorePopUp>().Activate(transform.position, scoreValue);
                        print(enemyBoomClip);



                        Audios.AudioManager.Play();

                        //ac.clip = enemyBoomClip;
                       // ac.Play();

                        print("explosions");
                    }
                }

                if (tag == "Player")
                {
                    pc.noDeathBonus = false;

                    pc.lives--;

                    if (pc.lives > 0)
                    {
                        gameController.GetComponent<PlayerDeath>().PlayerRespawn();
                    }
                    else
                    {
                        if (lc.levelTime >= timePassed && timePassed > 0)
                        {
                            lc.ManualActivation(dialogueToActivate);
                        }
                        else
                        {
                            gameController.GetComponent<PlayerDeath>().EndGameStart(false);
                        }
                    }
                    Audios.AudioManager.Play2();
                }
                else if (tag == "Enemy")
                {
                    pc.AddMultiplier();
                }
                if (GetComponent<AiBoss1>() || GetComponent<AiBoss2>())
                {
                    gameController.GetComponent<LevelController>().levelTimeScale = 1f;
                    gameController.GetComponent<PlayerDeath>().EndGameStart(true);
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

    public void StartInvincibility()
    {
        StartCoroutine(InvincibilityTimer());
    }

    IEnumerator InvincibilityTimer()
    {
        if (partikkelz.Length > 0)
        {
            for (int i = 0; i < partikkelz.Length; i++)
            {
                partikkelz[i].Play();
            }
        }
        invincible = true;
        for (int i = 0; i < 10; i++ )
        {
            yield return new WaitForSeconds(0.1f);
            sr.color = new Color(255f, 255f, 255f, 0f);
            yield return new WaitForSeconds(0.1f);
            sr.color = new Color(255f, 255f, 255f, 255f);
        }
        invincible = false;
        if (partikkelz.Length > 0)
        {
            for (int i = 0; i < partikkelz.Length; i++)
            {
                partikkelz[i].Stop();
            }
        }
    }
}
