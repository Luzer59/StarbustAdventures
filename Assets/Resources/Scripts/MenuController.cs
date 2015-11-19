using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    private int returnScene = -1;

    public void ChangeLevel(int index)
    {
        Application.LoadLevel(index);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (returnScene == -1)
            {
                    Application.Quit();
            }
            else
            {
                ChangeLevel(returnScene);
            }
            
        }
    }
}
