using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField]
    private GameObject tex;

    public float fadeSpeed = 1f;

    public void EndGameStart()
    {
        StartCoroutine(EndGame());
    }

    IEnumerator EndGame()
    {
        Image image = tex.GetComponent<Image>();
        yield return new WaitForSeconds(1f);
        for (float i = 0f; i <= 1f; i += fadeSpeed * Time.deltaTime)
        {
            print(i);
            image.color = new Color(1f, 1f, 1f, i);
            yield return null;
        }
        yield return new WaitForSeconds(3f);
        Application.LoadLevel(0);
    }
}
