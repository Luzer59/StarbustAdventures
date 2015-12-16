using UnityEngine;
using System.Collections;

public class MenuAudioController : MonoBehaviour
{

    void OnLevelWasLoaded(int index)
    {
        if (index == 2 || index == 4)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (GameObject.Find("ActiveAudioController"))
        {
            Destroy(gameObject);
        }
        else
        {
            name = "Active" + name;
            GetComponent<AudioSource>().Play();
            DontDestroyOnLoad(gameObject);
        }
    }
}
