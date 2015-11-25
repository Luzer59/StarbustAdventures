using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField]
    private GameObject tex;

    public float fadeSpeed = 1f;

    public IEnumerator EndGame()
    {
        Image image = tex.GetComponent<Image>();
        yield return new WaitForSeconds(1f);
        for (float i = 0f; i <= 255f; i += fadeSpeed * Time.deltaTime)
        {
            print(image.color.a);
            image.color = new Color(255f,255f,255f,i);
            yield return null;
        }
        yield return new WaitForSeconds(3f);
        Application.LoadLevel(Application.loadedLevel);
    }
}
