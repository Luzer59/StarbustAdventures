using UnityEngine;
using System;
using System.Collections;

public class AiNormal : AiBase
{
    public void ManualActivation()
    {
        PathStart();
    }

    void PathStart()
    {
        valueHandler.canBeHit = true;
        if (paths.Length == 1)
        {
            iTween.MoveTo(gameObject, iTween.Hash("path", paths[pathNumber].path, "movetopath", false, "speed", paths[pathNumber].speed, "easetype", paths[pathNumber].easeType, "oncomplete", "PathEnd", "oncompletetarget", gameObject));
        }
        else
        {
            iTween.MoveTo(gameObject, iTween.Hash("path", paths[pathNumber].path, "movetopath", false, "speed", paths[pathNumber].speed, "easetype", paths[pathNumber].easeType, "oncomplete", "NextPath", "oncompletetarget", gameObject));
        }
        if (paths[pathNumber].aim) // ducktaped bug
        {
            ship.transform.Rotate(new Vector3(0f, 90f, 90f));
        }
    }

    void Update()
    {
        if (startTime >= 0f)
        {
            if (gameController.GetComponent<LevelController>().levelTime >= startTime && valueHandler.canBeHit == false) // first path start
            {
                PathStart();
            }
        }

        if (valueHandler.canBeHit && player)
        {
            if (paths[pathNumber].aim)
            {
                transform.LookAt(new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z));
            }
            if (paths[pathNumber].shoot)
            {
                if (useSameShootValues)
                {

                }
                else
                {
                    for (int i = 0; i < paths[pathNumber].shootValues.Length; i++)
                    {
                        if (paths[pathNumber].shootValues[i].active)
                        {
                            if (paths[pathNumber].shootValues[i].infShoot)
                            {
                                if (gameController.GetComponent<LevelController>().levelTime - startTime > paths[pathNumber].shootValues[i].shootTiming[0])
                                {
                                    if (paths[pathNumber].shootValues[i].timer >= paths[pathNumber].shootValues[i].shootTiming[1])
                                    {
                                        paths[pathNumber].shootValues[i].timer = 0f;
                                        if (paths[pathNumber].disableAimingWhenShooting && paths[pathNumber].aim)
                                        {
                                            StartCoroutine(AimDisabledForSeconds(paths[pathNumber].disableTime));
                                        }
                                        CreateSpawner(i, useSameShootValues);
                                    }
                                    paths[pathNumber].shootValues[i].timer += Time.deltaTime;
                                }
                            }
                            else if (paths[pathNumber].shootValues[i].shootTiming.Length > paths[pathNumber].shootValues[i].shotsFired)
                            {
                                if (paths[pathNumber].shootValues[i].shootTiming[paths[pathNumber].shootValues[i].shotsFired] <= gameController.GetComponent<LevelController>().totalLevelTime - startTime + gameController.GetComponent<LevelController>().levelTime - gameController.GetComponent<LevelController>().totalLevelTime)
                                {
                                    if (paths[pathNumber].disableAimingWhenShooting && paths[pathNumber].aim)
                                    {
                                        StartCoroutine(AimDisabledForSeconds(paths[pathNumber].disableTime));
                                    }
                                    CreateSpawner(i, useSameShootValues);
                                }
                            }
                        }
                    }
                }
                /*if (paths[pathNumber].shootValues[i].active)
                {
                    if (paths[pathNumber].shootValues[i].infShoot)
                    {
                        if (gameController.GetComponent<LevelController>().levelTime - startTime > paths[pathNumber].shootValues[i].shootTiming[0])
                        {
                            shotTimer += Time.deltaTime;
                            if (shotTimer >= paths[pathNumber].shootValues[i].shootTiming[1])
                            {
                                shotTimer = 0f;
                                CreateSpawner(i, useSameShootValues);
                            }
                        }
                    }
                    else if (paths[pathNumber].shootValues[i].shootTiming.Length > paths[pathNumber].shootValues[i].shotsFired)
                    {
                        if (paths[pathNumber].shootValues[i].shootTiming[paths[pathNumber].shootValues[i].shotsFired] <= gameController.GetComponent<LevelController>().totalLevelTime - startTime + gameController.GetComponent<LevelController>().levelTime - gameController.GetComponent<LevelController>().totalLevelTime)
                        {
                            for (int p = 0; p < shootValues.Length; p++ )
                            {
                                if (shootValues[p].active)
                                {
                                    CreateSpawner(p, useSameShootValues);
                                }           
                            }
                        }
                    }
                }*/
            }
        }
	}
}
