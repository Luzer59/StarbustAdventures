using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField]
    private GameObject tex;
    [SerializeField]
    private Sprite loseSprite;
    [SerializeField]
    private Sprite winSprite;

    public float fadeSpeed = 1f;
    private bool win = false;
    private GameObject player;
    private int respawnHealth;
    private Vector3 respawnPosition;

    void Awake()
    {
        player = GameObject.Find("Player");
    }

    void Start()
    {
        respawnHealth = player.GetComponent<GameObjectPublicValueHandler>().health;
        respawnPosition = player.transform.position;
    }

    public void PlayerRespawn()
    {
        StartCoroutine(RespawnTimer());
    }

    IEnumerator RespawnTimer()
    {
        yield return new WaitForSeconds(2f);
        player.SetActive(true);
        player.GetComponent<GameObjectPublicValueHandler>().health = respawnHealth;
        player.transform.position = respawnPosition;
        player.GetComponent<GameObjectPublicValueHandler>().StartInvincibility();
    }

    public void EndGameStart(bool _win)
    {
        win = _win;
        StartCoroutine(EndGame());
    }

    IEnumerator EndGame()
    {
        Image image = tex.GetComponent<Image>();
        if (win)
        {
            image.sprite = winSprite;
        }
        else
        {
            image.sprite = loseSprite;
        }
        yield return new WaitForSeconds(1f);
        for (float i = 0f; i <= 1f; i += fadeSpeed * Time.deltaTime)
        {
            image.color = new Color(1f, 1f, 1f, i);
            yield return null;
        }
        yield return new WaitForSeconds(3f);
        GetComponent<MenuController>().ChangeLevel(0);
    }
}
