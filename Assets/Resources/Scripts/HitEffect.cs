using UnityEngine;
using System.Collections;

public class HitEffect : MonoBehaviour
{
    private SpriteRenderer[] renderers;

    void Start()
    {
        renderers = GetComponentsInChildren<SpriteRenderer>();
    }

    public IEnumerator Effect()
    {
        for (int p = 0; p < 3; p++)
        {
            for (int i = 0; i < renderers.Length; i++)
            {
                renderers[i].color = new Color(255f, 0f, 0f);
            }

            yield return new WaitForSeconds(0.05f);

            for (int i = 0; i < renderers.Length; i++)
            {
                renderers[i].color = new Color(255f, 255f, 255f);
            }

            yield return new WaitForSeconds(0.05f);
        }
        StopCoroutine(Effect());
    }
}
