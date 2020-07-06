using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour {
    WaveConfig waveConfig;

    /**
     * Cached waypoints from the wave config
     */
    List<Transform> waypoints;


    /**
     * Index of the index of the current waypoint that we're currently headed towards
     */
    int targetWaypointIndex = 0;

    void Start() {
        waypoints = waveConfig.GetWaypoints();
        transform.position = waypoints[targetWaypointIndex].transform.position;
    }

    void Update() {
        Move();
    }

    private void Move() {
        if (targetWaypointIndex <= waypoints.Count - 1) {
            Vector3 targetPosition = waypoints[targetWaypointIndex].transform.position;
            float movementThisFrame = waveConfig.GetMoveSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);

            if (transform.position == targetPosition) {
                targetWaypointIndex += 1;
            }
        } else {
            Destroy(gameObject);
        }
    }

    public void setWaveConfig(WaveConfig waveConfig) {
        this.waveConfig = waveConfig;
    }
}
