using UnityEngine;
using System.Collections;

public class MenuAudioController : MonoBehaviour
{
    void Awake()
    {
        if (GameObject.Find("AudioController"))
        {
            Destroy(gameObject);
        }
        else
        {
            GetComponent<AudioSource>().Play();
            DontDestroyOnLoad(gameObject);
        }
    }
}
