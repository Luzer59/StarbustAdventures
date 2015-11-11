using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreMultiplierText : MonoBehaviour
{
    [SerializeField]
    private GameObject gameController;

    void Update()
    {
        GetComponent<Text>().text = "X " + gameController.GetComponent<PlayerController>().scoreMultiplier;
    }
}
