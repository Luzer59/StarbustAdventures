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
<<<<<<< HEAD
        for (float i = 0f; i <= 1f; i += fadeSpeed * Time.deltaTime)
=======
        for (float i = 0f; i <= 255f; i += fadeSpeed * Time.deltaTime)
>>>>>>> 49894c4d154f1c4b0ed6c2de610769ed0e08348d
        {
            print(i);
            image.color = new Color(1f, 1f, 1f, i);
            yield return null;
        }
        yield return new WaitForSeconds(3f);
<<<<<<< HEAD
        Application.LoadLevel(0);
=======
        Application.LoadLevel(Application.loadedLevel);
>>>>>>> 49894c4d154f1c4b0ed6c2de610769ed0e08348d
    }
}
