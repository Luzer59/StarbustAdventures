using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LivesText : MonoBehaviour
{
    [SerializeField]
    private GameObject gameController;

    void Update()
    {
        GetComponent<Text>().text = "X " + gameController.GetComponent<PlayerController>().lives;
    }
}
