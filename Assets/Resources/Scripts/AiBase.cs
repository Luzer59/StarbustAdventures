using UnityEngine;
using System.Collections;

public abstract class AiBase : MonoBehaviour
{
    #region Variables
    [SerializeField]
    protected float startTime;
    [SerializeField]
    protected Path[] paths;
    [SerializeField]
    protected float zPlane;
    protected int pathNumber = 0;

    protected GameObject ship; // Ai object
    protected GameObject player; // Player
    protected GameObject gameController; // Game Controller
    [SerializeField]
    protected GameObject shotSpawner; // ShotSpawner prefab
    protected GameObject projectileSpawner; // ShotSpawner instance
    protected GameObjectPublicValueHandler valueHandler; // Interaction value class
    protected PoolManager poolManager;
    [SerializeField]
    protected bool useSameShootValues = false;
    [SerializeField]
    protected ShootValues[] shootValues;

    public GameObject Ship
    {
        get
        {
            return ship;
        }
    }

    #endregion
    void Start()
    {
        startTime += transform.parent.GetComponent<SequenceStart>().sequenceStartTime + transform.parent.parent.GetComponent<WaveStart>().waveStartTime; // Set start time      

        InitializeReferences(); // Get and adjust paths

        valueHandler.canBeHit = false;

        InitializePaths(); // Get references
    }

    private void InitializePaths() // Get and adjust paths
    {
        pathNumber = 0;

        Vector3 firstNode = iTweenPath.GetPath(paths[pathNumber].pathName)[pathNumber];
        for (int i = 0; i < paths.Length; i++)
        {
            paths[i].path = iTweenPath.GetPath(paths[i].pathName);
            for (int p = 0; p < paths[i].path.Length; p++)
            {
                paths[i].path[p] = new Vector3(paths[i].path[p].x - firstNode.x + transform.position.x, paths[i].path[p].y - firstNode.y + transform.position.y, zPlane);
            }
        }
    }

    private void InitializeReferences() // Get references
    {
        ship = GetComponentInChildren<Collider2D>().gameObject;
        player = GameObject.FindGameObjectWithTag("Player");
        gameController = GameObject.FindGameObjectWithTag("GameController");
        valueHandler = GetComponent<GameObjectPublicValueHandler>();
        poolManager = GameObject.FindGameObjectWithTag("PoolController").GetComponent<PoolManager>();
    }

    protected void CreateSpawner(int position, bool unifiedShootValues)
    {

        for (int i = 0; i < 50; i++)
        {
            if (!poolManager.shotSpawnerPool[i].activeInHierarchy)
            {
                projectileSpawner = poolManager.shotSpawnerPool[i];
                break;
            }
        }
        projectileSpawner.SetActive(true);
        if (unifiedShootValues)
        {
            shootValues[position].shotsFired++;
            projectileSpawner.transform.SetParent(shootValues[position].shootPos.transform, true);
            projectileSpawner.transform.position = projectileSpawner.transform.parent.transform.position;
            projectileSpawner.transform.rotation = projectileSpawner.transform.parent.transform.rotation;
            projectileSpawner.GetComponent<ShotSpawner>().Activate(shootValues[position].shootType, shootValues[position].projectileIndex, player.transform);
        }
        else
        {
            paths[pathNumber].shootValues[position].shotsFired++;
            projectileSpawner.transform.SetParent(paths[pathNumber].shootValues[position].shootPos.transform, true);
            projectileSpawner.transform.position = projectileSpawner.transform.parent.transform.position;
            projectileSpawner.transform.rotation = projectileSpawner.transform.parent.transform.rotation;
            projectileSpawner.GetComponent<ShotSpawner>().Activate(paths[pathNumber].shootValues[position].shootType, paths[pathNumber].shootValues[position].projectileIndex, player.transform);
        }
        projectileSpawner.transform.position = projectileSpawner.transform.parent.transform.position;
        projectileSpawner.transform.rotation = projectileSpawner.transform.parent.transform.rotation;
    }

    protected void CreateSpawner(int position, int projIndex, ShotType shotType)
    {

        for (int i = 0; i < 50; i++)
        {
            if (!poolManager.shotSpawnerPool[i].activeInHierarchy)
            {
                projectileSpawner = poolManager.shotSpawnerPool[i];
                break;
            }
        }
        projectileSpawner.SetActive(true);
        projectileSpawner.transform.SetParent(shootValues[position].shootPos.transform, true);
        projectileSpawner.transform.position = projectileSpawner.transform.parent.transform.position;
        projectileSpawner.transform.rotation = projectileSpawner.transform.parent.transform.rotation;
        projectileSpawner.GetComponent<ShotSpawner>().Activate(shotType, projIndex, player.transform);
    }

    protected virtual void PathEnd()
    {
        gameObject.SetActive(false);
    }

    protected IEnumerator EndWait()
    {
        yield return new WaitForSeconds(paths[pathNumber].endWaitTime);
        paths[pathNumber].endWait = false;
        NextPath();
        StopCoroutine(EndWait());
    }

    protected void NextPath()
    {
        if (paths[pathNumber].endWait)
        {
            StartCoroutine(EndWait());
        }
        else
        {
            pathNumber++;
            if (pathNumber == paths.Length - 1)
            {
                iTween.MoveTo(gameObject, iTween.Hash("path", paths[pathNumber].path, "movetopath", false, "speed", paths[pathNumber].speed, "easetype", paths[pathNumber].easeType, "looptype", paths[pathNumber].loopType, "oncomplete", "PathEnd", "oncompletetarget", gameObject));
            }
            else
            {
                iTween.MoveTo(gameObject, iTween.Hash("path", paths[pathNumber].path, "movetopath", false, "speed", paths[pathNumber].speed, "easetype", paths[pathNumber].easeType, "looptype", paths[pathNumber].loopType, "oncomplete", "NextPath", "oncompletetarget", gameObject));
            }
        }
    }

    protected IEnumerator AimDisabledForSeconds(float time)
    {
        paths[pathNumber].aim = false;
        yield return new WaitForSeconds(time);
        paths[pathNumber].aim = true;
    }
}
