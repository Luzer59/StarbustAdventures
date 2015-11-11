using UnityEngine;
using System.Collections;

[System.Serializable]
public class Path
{
    public string pathName;
    [HideInInspector]
    public Vector3[] path;
    public float speed;
    public string easeType;
    public string loopType = "none";
    public bool aim;
    public bool shoot;
    public bool disableAimingWhenShooting;
    public float disableTime;
    public ShootValues[] shootValues;
    public bool endWait;
    public float endWaitTime;
}
