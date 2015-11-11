using UnityEngine;
using System.Collections;

public enum ShotType
{
    Normal, Burst, Circle, WaveR, WaveL, Tri, TriBurst, OMFG, Aim
}

public class ShotSpawner : MonoBehaviour
{
    private GameObject instance;
    private ShotType shotType;
    private GameObject shooterObject;
    private PoolManager poolManager;
    private Quaternion dir;
    private Transform target;
    private int projectilePoolIndex = 0;

    void Awake()
    {
        poolManager = GameObject.FindGameObjectWithTag("PoolController").GetComponent<PoolManager>();
        gameObject.SetActive(false);
    }

    public void Activate(ShotType type, int projIndex, Transform _target)
    {
        shotType = type;
        shooterObject = transform.parent.parent.parent.gameObject;
        target = _target;
        projectilePoolIndex = projIndex;
        StartCoroutine(Shoot());
    }

    void ReturnToPool()
    {
        transform.SetParent(GetComponent<PoolIndex>().container);
        transform.localPosition = poolManager.gameObject.transform.position;
        gameObject.SetActive(false);
    }

    void TakeFromPool(int instancePoolIndex, Quaternion direction)
    {
        for (int i = 0; i < poolManager.projectilePoolBuffer[instancePoolIndex]; i++)
        {
            if (!poolManager.projectilePool[instancePoolIndex][i].activeInHierarchy)
            {
                instance = poolManager.projectilePool[instancePoolIndex][i];
                break;
            }
        }
        instance.SetActive(true);
        instance.transform.SetParent(null, true);
        instance.transform.position = transform.position;
        instance.transform.rotation = direction;
        instance.GetComponent<ProjectileBasic>().Fire(shooterObject, transform.parent);
    }

	IEnumerator Shoot()
    {
        float angle;

        switch (shotType)
        {
            case ShotType.Normal:
                dir = transform.rotation;
                TakeFromPool(projectilePoolIndex, dir);
                EndShoot();
                break;

            case ShotType.Burst:
                for (int i = 0; i < 3; i++)
                {
                    dir = transform.rotation;
                    TakeFromPool(projectilePoolIndex, dir);
                    yield return new WaitForSeconds(0.05f);
                }
                EndShoot();
                break;

            case ShotType.Circle:
                for (int p = -180; p < 180; p += 45)
                {
                    dir = Quaternion.Euler(transform.parent.transform.rotation.eulerAngles.x, transform.parent.transform.rotation.eulerAngles.y, transform.parent.transform.rotation.eulerAngles.z + p);
                    TakeFromPool(projectilePoolIndex, dir);
                }
                EndShoot();
                break;

            case ShotType.WaveR:
                angle = 100f;
                for (int i = 0; i < 7; i ++)
                {
                    dir = Quaternion.Euler(transform.parent.transform.rotation.eulerAngles.x, transform.parent.transform.rotation.eulerAngles.y, transform.parent.transform.rotation.eulerAngles.z + angle);
                    TakeFromPool(projectilePoolIndex, dir);
                    angle -= 20f;
                    yield return new WaitForSeconds(0.1f);
                }
                EndShoot();
                break;

            case ShotType.WaveL:
                angle = -100f;
                for (int i = 0; i < 7; i++)
                {
                    dir = Quaternion.Euler(transform.parent.transform.rotation.eulerAngles.x, transform.parent.transform.rotation.eulerAngles.y, transform.parent.transform.rotation.eulerAngles.z + angle);
                    TakeFromPool(projectilePoolIndex, dir);
                    angle += 20f;
                    yield return new WaitForSeconds(0.1f);
                }
                EndShoot();
                break;

            case ShotType.Tri:
                for (int i = -1; i < 2; i++)
                {
                    dir = Quaternion.Euler(transform.parent.transform.rotation.eulerAngles.x, transform.parent.transform.rotation.eulerAngles.y, transform.parent.transform.rotation.eulerAngles.z + i * 30f);
                    TakeFromPool(projectilePoolIndex, dir);
                }
                EndShoot();
                break;

            case ShotType.TriBurst:
                for (int p = 0; p < 3; p++)
                {
                    for (int i = -1; i < 2; i++)
                    {
                        dir = Quaternion.Euler(transform.parent.transform.rotation.eulerAngles.x, transform.parent.transform.rotation.eulerAngles.y, transform.parent.transform.rotation.eulerAngles.z + i * 30f);
                        TakeFromPool(projectilePoolIndex, dir);
                    }
                    yield return new WaitForSeconds(0.1f);
                }
                EndShoot();
                break;

            case ShotType.OMFG:
                for (int i = -180; i < 180; i += 10)
                {
                    dir = Quaternion.Euler(transform.parent.transform.rotation.eulerAngles.x, transform.parent.transform.rotation.eulerAngles.y, transform.parent.transform.rotation.eulerAngles.z + i);
                    TakeFromPool(projectilePoolIndex, dir);
                }
                EndShoot();
                break;

            case ShotType.Aim:
                transform.LookAt(new Vector3(target.position.x, target.position.y, transform.position.z));
                transform.Rotate(new Vector3(0f, 90f, 90f));
                dir = transform.rotation;
                TakeFromPool(projectilePoolIndex, dir);
                EndShoot();
                break;

            default:
                EndShoot();
                break;
        }
	}
    void EndShoot()
    {
        StopCoroutine(Shoot());
        ReturnToPool();
    }
}
