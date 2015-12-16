using UnityEngine;
using System.Collections;

public class Audios : MonoBehaviour {

    public static Audios AudioManager
    {
        get
        {
            return audioManager;
        }


    }
    private static Audios audioManager;

    public GameObject aani;
    public GameObject aani2;




	void Awake () 
    {

        if (audioManager != this && audioManager != null)
            Destroy(gameObject);
        else
        {
            audioManager = this;
            DontDestroyOnLoad(gameObject);
        }




        print("audio created");
	}
	

    public void Play()
    {

        //_as.Play();
        Instantiate(aani, Vector3.zero, Quaternion.identity);


    }
    public void Play2()
    {

        //_as.Play();
        Instantiate(aani2, Vector3.zero, Quaternion.identity);


    }
}
