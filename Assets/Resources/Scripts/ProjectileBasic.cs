﻿using UnityEngine;
using System.Collections;

enum WeaponType
{
    Projectile, Beam, Quickbeam
}

public class ProjectileBasic : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private WeaponType weaponType;
    private GameObject otherParent;
    private PoolManager poolManager;
    private GameObjectPublicValueHandler valueHandler;
    //private int poolIndex = 0;
    private SpriteRenderer spriteRenderer;
    private Vector3 pos;
    private BoxCollider2D col;

    public AudioClip playerProjClip;
    public AudioClip enemyProjClip;
    public AudioClip beamStartClip;
    public AudioClip beamShootClip;
    private AudioSource ac;

    void Awake()
    {
        poolManager = GameObject.FindGameObjectWithTag("PoolController").GetComponent<PoolManager>();
        //poolIndex = GetComponent<PoolIndex>().poolIndex;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        col = GetComponent<BoxCollider2D>();
        ac = GetComponent<AudioSource>();
        gameObject.SetActive(false);
    }

    // Activation method
    public void Fire(GameObject shooterObject, Transform shootPosition)
    {
        tag = shooterObject.tag;
        if (weaponType == WeaponType.Projectile)
        {
            StartCoroutine(DestroyByTime());
            GetComponent<Rigidbody2D>().velocity = transform.up * speed;
            if (tag == "Player")
            {
                ac.PlayOneShot(playerProjClip);
            }
            else
            {
                ac.PlayOneShot(enemyProjClip);
            }
        }
        else if (weaponType == WeaponType.Beam)
        {
            transform.SetParent(shootPosition, true);
            StartCoroutine(BeamController());
            ac.PlayOneShot(beamStartClip);
        }
        else if (weaponType == WeaponType.Quickbeam)
        {
            transform.SetParent(shootPosition, true);
            StartCoroutine(QuickbeamController());
            ac.PlayOneShot(beamShootClip);
        }
    }

    void Update()
    {
        if (weaponType == WeaponType.Beam)
        {

        }
    }

    void ReturnToPool()
    {
        transform.SetParent(GetComponent<PoolIndex>().container);
        transform.localPosition = poolManager.gameObject.transform.position;
        gameObject.SetActive(false);
    }

    IEnumerator DestroyByTime()
    {
        yield return new WaitForSeconds(4f);
        ReturnToPool();
    }

    IEnumerator MercyTimer()
    {
        col.enabled = false;
        yield return new WaitForSeconds(0.2f);
        col.enabled = true;
    }

    IEnumerator BeamController()
    {
        col.enabled = false;
        spriteRenderer.color = new Color(255f, 255f, 255f, 0.3f);
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = new Color(255f, 255f, 255f, 0f);
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = new Color(255f, 255f, 255f, 0.3f);
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = new Color(255f, 255f, 255f, 0f);
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = new Color(255f, 255f, 255f, 0.3f);
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = new Color(255f, 255f, 255f, 1f);
        ac.PlayOneShot(beamShootClip);
        GetComponentInChildren<ParticleSystem>().Play();
        col.enabled = true;
        yield return new WaitForSeconds(speed);
        col.enabled = false;
        ReturnToPool();
    }

    IEnumerator QuickbeamController()
    {
        GetComponentInChildren<ParticleSystem>().Play();
        col.enabled = true;
        yield return new WaitForSeconds(speed);
        col.enabled = false;
        ReturnToPool();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        valueHandler = other.GetComponentInParent<GameObjectPublicValueHandler>();

        if (valueHandler)
        {
            if (valueHandler.tag != tag)
            {
                if (valueHandler.canBeHit == true)
                {
                    valueHandler.CheckHealth();

                    if (weaponType == WeaponType.Projectile)
                    {
                        ReturnToPool();
                    }

                    else if (weaponType == WeaponType.Beam)
                    {
                        StartCoroutine(MercyTimer());
                    }
                }
            }
        }
    }
}
