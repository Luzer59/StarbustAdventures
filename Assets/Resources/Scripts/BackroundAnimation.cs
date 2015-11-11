using UnityEngine;
using System.Collections;

public class BackroundAnimation : MonoBehaviour
{
    private float mainTexOffset = 0f;
    public float mainTexOffsetIncrement = 0f;

	void Update ()
    {
        mainTexOffset += mainTexOffsetIncrement;
        GetComponent<MeshRenderer>().material.SetTextureOffset("_MainTex", new Vector2(0f, mainTexOffset));
	}
}
