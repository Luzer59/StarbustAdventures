using UnityEngine;
using System.Collections;

public class CollisionDamage : MonoBehaviour
{
    void OnTriggerEnter2D (Collider2D col)
    {
        if (col.tag == "Enemy")
        {
            col.GetComponentInParent<GameObjectPublicValueHandler>().CheckHealth();
            GetComponentInParent<GameObjectPublicValueHandler>().CheckHealth();
        }
    }
}
