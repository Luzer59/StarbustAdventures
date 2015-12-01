using UnityEngine;
using System.Collections;

enum WeaponType
{
    Projectile, Beam
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

    void Awake()
    {
        poolManager = GameObject.FindGameObjectWithTag("PoolController").GetComponent<PoolManager>();
        //poolIndex = GetComponent<PoolIndex>().poolIndex;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
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
        }
        else if (weaponType == WeaponType.Beam)
        {
            transform.SetParent(shootPosition, true);
            StartCoroutine(BeamController());
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
        StopCoroutine(DestroyByTime());
    }

    IEnumerator MercyTimer()
    {
        yield return new WaitForSeconds(0.2f);
        GetComponent<BoxCollider2D>().enabled = true;
        StopCoroutine(MercyTimer());
    }

    IEnumerator BeamController()
    {
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
        GetComponentInChildren<ParticleSystem>().Play();
        GetComponent<BoxCollider2D>().enabled = true;
        yield return new WaitForSeconds(2f);
        GetComponent<BoxCollider2D>().enabled = false;
        ReturnToPool();
        StopCoroutine(BeamController());
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        valueHandler = other.GetComponentInParent<GameObjectPublicValueHandler>();

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
                    GetComponent<BoxCollider2D>().enabled = false;
                    StartCoroutine(MercyTimer());
                }
            }
        }      
    }
}
