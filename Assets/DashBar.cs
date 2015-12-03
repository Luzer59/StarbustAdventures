using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DashBar : MonoBehaviour
{
    private Image image;
    private PlayerController playerController;

    void Awake()
    {
        image = GetComponent<Image>();
        playerController = GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayerController>();
    }

    void Update()
    {
        image.fillAmount = playerController.dash;
    }
}
