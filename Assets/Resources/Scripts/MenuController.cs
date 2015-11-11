using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour
{
    public void ChangeLevel(int index)
    {
        Application.LoadLevel(index);
    }
}
