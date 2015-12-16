using UnityEngine;
using System.Collections;

public class EndTimer : MonoBehaviour
{
    public float time;

    void Update()
    {
        time -= Time.deltaTime;
        if (time <= 0f)
        {
            GetComponent<MenuController>().ChangeLevel(0);
        }
    }
}
