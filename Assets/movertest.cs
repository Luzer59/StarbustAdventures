using UnityEngine;
using System.Collections;

public class movertest : MonoBehaviour
{
    public float moveSpeed = 1f;

    void Update()
    {
        transform.Translate(0f, moveSpeed * Time.deltaTime, 0f);
    }
}
