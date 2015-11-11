using UnityEngine;
using System.Collections;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField]
    private ShotType shotType;
    [SerializeField]
    private int projectileIndex;
    private GameObject projectileSpawner;
    [SerializeField]
    private GameObject shootPos;
    [SerializeField]
    private GameObject shotSpawner;
    private PoolManager poolManager;
    private float timer = 0f;
    [SerializeField]
    private float rateOfFire = 1f;

    void Start()
    {
        poolManager = GameObject.FindGameObjectWithTag("PoolController").GetComponent<PoolManager>();
    }

	void Update ()
    {
        timer += Time.deltaTime;
        if (Input.GetButton("Fire1") && timer >= rateOfFire)
        {
            timer = 0f;
            for (int i = 0; i < 50; i++)
            {
                if (!poolManager.shotSpawnerPool[i].activeInHierarchy)
                {
                    projectileSpawner = poolManager.shotSpawnerPool[i];
                    break;
                }
            }
            projectileSpawner.SetActive(true);
            projectileSpawner.transform.SetParent(shootPos.transform, true);
            projectileSpawner.transform.position = projectileSpawner.transform.parent.transform.position;
            projectileSpawner.transform.rotation = projectileSpawner.transform.parent.transform.rotation;
            projectileSpawner.GetComponent<ShotSpawner>().Activate(shotType, projectileIndex, transform);
        }
        
	}
}
