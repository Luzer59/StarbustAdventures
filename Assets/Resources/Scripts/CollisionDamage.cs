using UnityEngine;
using System.Collections;

public class CollisionDamage : MonoBehaviour
{
    private GameObjectPublicValueHandler colHandler;

    void Awake()
    {
        colHandler = GetComponentInParent<GameObjectPublicValueHandler>();
    }

    void OnTriggerEnter2D (Collider2D col)
    {
        if (col.tag == "Enemy")
        {
            
            GameObjectPublicValueHandler otherColHandler = col.GetComponentInParent<GameObjectPublicValueHandler>();

            if (colHandler && otherColHandler)
            {
                otherColHandler.CheckHealth();
                colHandler.CheckHealth();
            }
        }
    }
}
