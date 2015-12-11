using UnityEngine;
using System.Collections;

public class ActivateObjectCondition : MonoBehaviour
{
    public int dialogueToActivate;
    public float timePassed;

    void OnDisable()
    {
        if (GetComponent<GameObjectPublicValueHandler>().pc.lives <= 0 && GetComponent<GameObjectPublicValueHandler>().gameController.GetComponent<LevelController>().levelTime >= timePassed)
        {

            GetComponent<GameObjectPublicValueHandler>().gameController.GetComponent<LevelController>().ManualActivation(dialogueToActivate);
        }
    }
}
