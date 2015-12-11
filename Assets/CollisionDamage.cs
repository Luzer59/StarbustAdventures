using UnityEngine;
using System.Collections;

public class CollisionDamage : MonoBehaviour
{
    void OnTriggerEnter2D (Collider2D col)
    {
        print("Asd");
        if (col.tag == "Player")
        {
            //col.GetComponentInParent<GameObjectPublicValueHandler>().CheckHealth();
            //GetComponentInParent<GameObjectPublicValueHandler>().CheckHealth();
        }
    }
}
