using System;
using UnityEngine;

public class RotatingCameraScript : MonoBehaviour
{
    bool enemyDetected;
    TrackingScript TrackingScript;

    void Awake()
    {
        TrackingScript = GetComponentInChildren<TrackingScript>();
        TrackingScript.EnemyDetected += () => enemyDetected = true;
    }

    void Update()
    {
        if (!enemyDetected)
            transform.Rotate(0, 50 * Time.deltaTime, 0);
    }
}
