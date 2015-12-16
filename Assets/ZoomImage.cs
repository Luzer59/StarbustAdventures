using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ZoomImage : MonoBehaviour
{
    private bool active;
    private Image imageComponent;

    void Awake()
    {
        imageComponent = GetComponent<Image>();
    }

    public void Zoom(Sprite image)
    {
        imageComponent.sprite = image;
        imageComponent.color = new Color(255f, 255f, 255f, 255f);
        imageComponent.raycastTarget = true;
        active = true;
    }

    void Update()
    {
        if (active)
        {
            if (Input.anyKeyDown)
            {
                imageComponent.color = new Color(255f, 255f, 255f, 0f);
                imageComponent.raycastTarget = false;
                active = false;
            }
        }
    }
}
