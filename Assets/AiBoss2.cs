using UnityEngine;
using System.Collections;

public class AiBoss2 : AiBase
{
    private int state = 0;

    void Update()
    {
        if (startTime >= 0f)
        {
            if (gameController.GetComponent<LevelController>().levelTime >= startTime && valueHandler.canBeHit == false) // first path start
            {
                ship.SetActive(true);
                gameController.GetComponent<LevelController>().levelTimeScale = 0f;
                iTween.MoveTo(gameObject, iTween.Hash("path", paths[pathNumber].path, "movetopath", false, "speed", paths[pathNumber].speed, "easetype", paths[pathNumber].easeType, "looptype", paths[pathNumber].loopType, "oncomplete", "PathEnd", "oncompletetarget", gameObject));
                valueHandler.canBeHit = true;
                StartCoroutine(StateTimer(1f));
            }
        }
    }

    void OnDisable()
    {
        if (GetComponent<LevelController>())
        {
            gameController.GetComponent<LevelController>().levelTimeScale = 1f;
        }
    }



    protected override void PathEnd()
    {
        // deactivation override
    }

    /* 
     * Shoot positions
     * 0 = middle
     * 1 = front left
     * 2 = front right
     * 3 = top left
     * 4 = top right
     */

    void StateMachine()
    {
        //int random = Random.Range(0, 3);

        //Debug.Log(random);

        switch (state)
        {
            case 0:
                // Sweep mode
                Sweep1();
                break;

            case 1:
                // Cage mode
                StartCoroutine(Cage());
                break;

            case 2:
                // All but one lasers mode
                StartCoroutine(AllButOne());
                break;

            case 3:
                // One laset at a time mode
                StartCoroutine(OneAtATime());
                break;

            default:
                break;
        }
        state++;
        if (state > 3)
        {
            state = 0;
        }
    }

    IEnumerator StateTimer(float delay)
    {
        yield return new WaitForSeconds(delay);
        StateMachine();
    }

    /*IEnumerator BasicShot()
    {
        while (true)
        {
            for (int i = 0; i < 3; i++)
            {
                CreateSpawner(i, useSameShootValues);
            }
            yield return new WaitForSeconds(2f);
        }
    }*/

    void Sweep1()
    {
        iTween.MoveTo(gameObject, iTween.Hash("path", paths[1].path, "movetopath", false, "speed", paths[1].speed, "easetype", paths[1].easeType, "looptype", paths[1].loopType, "oncomplete", "Sweep2", "oncompletetarget", gameObject));
    }
    void Sweep2()
    {
        CreateSpawner(0, 3, ShotType.Normal);
        iTween.MoveTo(gameObject, iTween.Hash("path", paths[2].path, "movetopath", false, "speed", paths[2].speed, "easetype", paths[2].easeType, "looptype", paths[2].loopType, "oncomplete", "Sweep3", "oncompletetarget", gameObject));
    }
    void Sweep3()
    {
        iTween.MoveTo(gameObject, iTween.Hash("path", paths[3].path, "movetopath", false, "speed", paths[3].speed, "easetype", paths[3].easeType, "looptype", paths[3].loopType, "oncomplete", "StateMachine", "oncompletetarget", gameObject));
    }

    IEnumerator Cage()
    {
        CreateSpawner(1, 3, ShotType.Normal);
        CreateSpawner(8, 3, ShotType.Normal);

        yield return new WaitForSeconds(1f);

        for (int i = 0; i < 3; i++ )
        {
            CreateSpawner(2, 2, ShotType.Aim);
            CreateSpawner(7, 2, ShotType.Aim);
            yield return new WaitForSeconds(2f);
        }
        yield return new WaitForSeconds(3f);
        StateMachine();
    }

    IEnumerator AllButOne()
    {
        int random;
        while (true)
        {
            random = Random.Range(0, 9);
            if (random == 1 || random == 8)
            {

            }
            else
            {
                break;
            }
        }

        for (int i = 0; i < 9; i++)
        {
            if (i != random)
            {
                CreateSpawner(i, 3, ShotType.Normal);
            }
        }
        yield return new WaitForSeconds(8f);
        StateMachine();
    }

    IEnumerator OneAtATime()
    {
        for (int i = 8; i > -1; i--)
        {
            CreateSpawner(i, 2, ShotType.Aim);
            yield return new WaitForSeconds(0.5f);
        }
        yield return new WaitForSeconds(3f);
        StateMachine();
    }
}

