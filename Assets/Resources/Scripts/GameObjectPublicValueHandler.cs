using UnityEngine;
using System.Collections;

public class GameObjectPublicValueHandler : MonoBehaviour
{
    public bool canBeHit = false;
    public int health = 1;

    public void CheckHealth()
    {
        if (health <= 0)
        {
            if (GetComponent<DeathExplosionEffect>())
            {
                GetComponent<DeathExplosionEffect>().Effect();
            }

            if (tag == "Player")
            {
                Application.LoadLevel(Application.loadedLevel);
            }

            gameObject.SetActive(false);
        }
        else
        {
            if (GetComponent<HitEffect>())
            {
                StartCoroutine(GetComponent<HitEffect>().Effect());
                
            }
        }
    }
}
