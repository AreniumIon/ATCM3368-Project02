using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderLeg : MonoBehaviour
{
    //references
    [SerializeField] GiantEnemySpider spider = null;
    [SerializeField] bool alternateFirst = false;
    [SerializeField] bool rightLeg = false;

    //Rotate amount
    static Vector3 minRotation = new Vector3();
    static Vector3 maxRotation = new Vector3(0f, 0f, 25f);

    //Timer
    static float cycleDuration = 0.15f;
    float cycleTimer = 0.5f;

    private void Update()
    {
        if (!spider.isFrozen && spider.IsPlayerNearby())
        {
            //Cycle timing
            if (alternateFirst)
                cycleTimer += Time.deltaTime / cycleDuration;
            else if (!alternateFirst)
                cycleTimer -= Time.deltaTime / cycleDuration;

            if (cycleTimer >= 1f)
                alternateFirst = false;
            else if (cycleTimer <= 0f)
                alternateFirst = true;

            //Lerp rotation
            if (rightLeg)
                transform.localEulerAngles = Vector3.Lerp(minRotation, maxRotation, cycleTimer);
            else
                transform.localEulerAngles = Vector3.Lerp(minRotation, -1 * maxRotation, cycleTimer);
        }
    }
}
