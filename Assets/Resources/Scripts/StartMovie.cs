using UnityEngine;
using System.Collections;

public class StartMovie : MonoBehaviour
{
    private MenuController mc;
    private MovieTexture movTex;

    void Awake()
    {
        mc = GetComponent<MenuController>();
    }

    void Start()
    {
        movTex = (MovieTexture)GetComponent<MeshRenderer>().material.mainTexture;
        movTex.Play();
    }

    void Update()
    {
        if (!movTex.isPlaying)
        {
            mc.ChangeLevel(8);
        }
    }
}
