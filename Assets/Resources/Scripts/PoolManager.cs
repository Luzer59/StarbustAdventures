using UnityEngine;
using System.Collections.Generic;

public class PoolManager : MonoBehaviour
{
    public List<GameObject>[] projectilePool;
    public List<GameObject>[] effectPool;
    [HideInInspector]
    public List<GameObject> shotSpawnerPool;
    [SerializeField]
    private GameObject shotSpawner;

    [SerializeField]
    private GameObject[] pooledProjectiles;
    public int[] projectilePoolBuffer;

    [SerializeField]
    private GameObject[] pooledEffects;
    public int[] effectPoolBuffer;

    private GameObject instance;

    void Start()
    {
        PoolShotSpawners();

        PoolProjectiles();

        PoolEffects();
    }

    void PoolShotSpawners()
    {
        GameObject shotSpawnerParent = new GameObject("shotSpawnerParent");
        shotSpawnerParent.transform.SetParent(transform);
        shotSpawnerParent.transform.position = transform.position;

        for (int p = 0; p < 50; p++)
        {
            instance = (GameObject)Instantiate(shotSpawner, Vector3.zero, Quaternion.identity);
            shotSpawnerPool.Add(instance);
            instance.transform.SetParent(shotSpawnerParent.transform);
            instance.transform.position = shotSpawnerParent.transform.position;
            instance.GetComponent<PoolIndex>().container = shotSpawnerParent.transform;
        }
    }

    void PoolProjectiles()
    {
        projectilePool = new List<GameObject>[pooledProjectiles.Length];
        for (int j = 0; j < pooledProjectiles.Length; j++)
        {
            projectilePool[j] = new List<GameObject> { };
        }

        GameObject[] projectileParent = new GameObject[pooledProjectiles.Length];

        for (int p = 0; p < pooledProjectiles.Length; p++)
        {
            projectileParent[p] = new GameObject(pooledProjectiles[p].name + "Parent");
            projectileParent[p].transform.SetParent(transform);
            projectileParent[p].transform.position = transform.position;
            for (int i = 0; i < projectilePoolBuffer[p]; i++)
            {
                instance = (GameObject)Instantiate(pooledProjectiles[p], Vector3.zero, Quaternion.identity);
                projectilePool[p].Add(instance);
                instance.GetComponent<PoolIndex>().poolIndex = p;
                instance.transform.SetParent(projectileParent[p].transform);
                instance.transform.position = projectileParent[p].transform.position;
                instance.GetComponent<PoolIndex>().container = projectileParent[p].transform;
            }
        }
    }

    void PoolEffects()
    {
        effectPool = new List<GameObject>[pooledEffects.Length];
        for (int j = 0; j < pooledEffects.Length; j++)
        {
            effectPool[j] = new List<GameObject> { };
        }

        GameObject[] effectParent = new GameObject[pooledEffects.Length];

        for (int p = 0; p < pooledEffects.Length; p++)
        {
            effectParent[p] = new GameObject(pooledEffects[p].name + "Parent");
            effectParent[p].transform.SetParent(transform);
            effectParent[p].transform.position = transform.position;
            for (int i = 0; i < projectilePoolBuffer[p]; i++)
            {
                instance = (GameObject)Instantiate(pooledEffects[p], Vector3.zero, Quaternion.identity);
                effectPool[p].Add(instance);
                instance.GetComponent<PoolIndex>().poolIndex = p;
                instance.transform.SetParent(effectParent[p].transform);
                instance.transform.position = effectParent[p].transform.position;
                instance.GetComponent<PoolIndex>().container = effectParent[p].transform;
                gameObject.SetActive(false);
            }
        }
    }  
}
