using UnityEngine;
using System.Collections;

[System.Serializable]
public class ShootValues
{
    // If infShoot is on shootTiming will require 2 parameters, 1. delay from start and 2. RoF
    public GameObject shootPos;
    public int projectileIndex;
    public bool active = true;
    public float[] shootTiming;
    public ShotType shootType;
    [HideInInspector]
    public int shotsFired;
    public bool infShoot;
    [HideInInspector]
    public float timer = 0f;
}
