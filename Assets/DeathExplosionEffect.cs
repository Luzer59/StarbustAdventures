using UnityEngine;
using System.Collections;

public class DeathExplosionEffect : MonoBehaviour
{
    [SerializeField]
    private int explosionPoolIndex = 0;

    GameObject instance;
    public PoolManager poolManager;

    void Awake()
    {
        poolManager = GameObject.FindGameObjectWithTag("PoolController").GetComponent<PoolManager>();
    }

    public void Effect()
    {
        for (int i = 0; i < poolManager.effectPoolBuffer[explosionPoolIndex]; i++)
        {
            if (!poolManager.effectPool[explosionPoolIndex][i].activeInHierarchy)
            {
                instance = poolManager.effectPool[explosionPoolIndex][i];
                break;
            }
        }
        Debug.Log(instance, this);
        instance.SetActive(true);
        instance.transform.SetParent(null, true);
        instance.transform.position = transform.position;
    }
}
