using UnityEngine;
using System.Collections;

public class AiBoss1 : AiBase
{

    //private float timer1 = 0f;
    //private float timer1Full = 3f;
    //private bool stateActive = false;
    private int state = 0;
	
	void Update()
    {
        if (startTime >= 0f)
        {
            if (gameController.GetComponent<LevelController>().levelTime >= startTime && valueHandler.canBeHit == false) // first path start
            {
                ship.SetActive(true);
                gameController.GetComponent<LevelController>().levelTimeScale = 0f;
                iTween.MoveTo(gameObject, iTween.Hash("path", paths[pathNumber].path, "movetopath", false, "speed", paths[pathNumber].speed, "easetype", paths[pathNumber].easeType, "looptype", paths[pathNumber].loopType, "oncomplete", "NextPath", "oncompletetarget", gameObject));
                valueHandler.canBeHit = true;
                StartCoroutine(StateTimer(4f));
                StartCoroutine(BasicShot());
            }
        }
	}

    /*void OnDisable()
    {
        if (GetComponent<LevelController>())
        {
            gameController.GetComponent<LevelController>().levelTimeScale = 1f;
        }
    }*/



    protected override void PathEnd()
    {
        // deactivation override
    }

    /* 
     * Shoot positions
     * 0 = front middle
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
                // Circleshot mode
                StartCoroutine(Circleshot());
                break;

            case 1:
                // Waveshot mode
                StartCoroutine(Waveshot());
                break;

            case 2:
                // Aimburst mode
                StartCoroutine(Aimburst());
                break;

            default:
                break;
        }
        state++;
        if (state > 2)
        {
            state = 0;
        }
    }

    IEnumerator StateTimer(float delay)
    {
        yield return new WaitForSeconds(delay);
        StateMachine();
        StopCoroutine(StateTimer(delay));
    }

    IEnumerator BasicShot()
    {
        while (true)
        {
            for (int i = 0; i < 3; i++)
            {
                CreateSpawner(i, useSameShootValues);
            }
            yield return new WaitForSeconds(2f);
        }
    }

    IEnumerator Circleshot()
    {
        CreateSpawner(3, 0, ShotType.Circle);
        CreateSpawner(4, 0, ShotType.Circle);

        yield return new WaitForSeconds(1f);

        CreateSpawner(3, 0, ShotType.Circle);
        CreateSpawner(4, 0, ShotType.Circle);

        yield return new WaitForSeconds(1f);

        CreateSpawner(3, 0, ShotType.Circle);
        CreateSpawner(4, 0, ShotType.Circle);

        StartCoroutine(StateTimer(4f));
        StopCoroutine(Circleshot());
    }

    IEnumerator Waveshot()
    {
        CreateSpawner(3, 0, ShotType.WaveL);

        yield return new WaitForSeconds(1f);

        CreateSpawner(4, 0, ShotType.WaveR);

        StartCoroutine(StateTimer(4f));
        StopCoroutine(Circleshot());
    }

    IEnumerator Aimburst()
    {
        for (int i = 0; i < 20; i++ )
        {
            CreateSpawner(3, 0, ShotType.Aim);
            CreateSpawner(4, 0, ShotType.Aim);
            yield return new WaitForSeconds(0.1f);
        }

        StartCoroutine(StateTimer(4f));
        StopCoroutine(Circleshot());
    }
}
