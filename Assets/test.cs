using UnityEngine;
using System.Collections;

public class test : MonoBehaviour
{
    public PoolManager poolManager;

    // Use this for initialization
    void Start()
    {
        poolManager = GameObject.FindGameObjectWithTag("PoolController").GetComponent<PoolManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
