using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    [SerializeField]
    private GameObject gameController;

	void Update ()
    {
        GetComponent<Text>().text = "Score: " + gameController.GetComponent<PlayerController>().score;
	}
}
