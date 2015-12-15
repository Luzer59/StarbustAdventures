using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScorePopUp : MonoBehaviour
{
    public float fadeSpeed = 1f;
    public float moveSpeed = 1f;

    private RectTransform transf;
    private int screenWidth;
    private int screenHeight;
    private Camera mainCamera;
    private PoolIndex poolIndex;
    private Text text;
    private Transform canvas;

    private float timer = 1f;

    void Awake()
    {
        transf = GetComponent<RectTransform>();
        poolIndex = GetComponent<PoolIndex>();
        canvas = GameObject.Find("Canvas").transform;
        text = GetComponent<Text>();
        mainCamera = Camera.main;
        screenWidth = mainCamera.pixelWidth;
        screenHeight = mainCamera.pixelHeight;
    }

    public void Activate(Vector3 worldPos, int value)
    {
        /*Vector3 tempScreenPos = mainCamera.WorldToScreenPoint(worldPos);
        Vector3 screenPos = new Vector3((screenWidth / 2) - tempScreenPos.x, (screenHeight / 2) - tempScreenPos.y, tempScreenPos.z);
        transf.position = screenPos;*/

        transf.SetParent(canvas);

        transf.anchorMin = new Vector2(mainCamera.WorldToViewportPoint(worldPos).x, mainCamera.WorldToViewportPoint(worldPos).y);
        transf.anchorMax = new Vector2(mainCamera.WorldToViewportPoint(worldPos).x, mainCamera.WorldToViewportPoint(worldPos).y);
        transf.anchoredPosition = Vector2.zero;
        //transf.localPosition = new Vector3(mainCamera.WorldToViewportPoint(worldPos).x * screenWidth, mainCamera.WorldToViewportPoint(worldPos).y * screenHeight, 0f);
        text.text = value.ToString();
    }

    void Update()
    {
        transf.Translate(0f, moveSpeed, 0f);
        timer -= fadeSpeed * Time.deltaTime;
        text.color = new Color(255f, 255f, 255f, timer);
        if (timer <= 0f)
        {
            timer = 1f;
            ReturnToPool();
        }

    }

    void ReturnToPool()
    {
        transf.SetParent(poolIndex.container);
        transf.localPosition = poolIndex.container.position;
        transf.SetParent(poolIndex.container);
        gameObject.SetActive(false);
    }
}
