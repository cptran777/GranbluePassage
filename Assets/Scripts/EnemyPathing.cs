using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour, IEntity {
    // Note: If an enemy does not loop then they destroy themselves at the end of the path.
    // Note2: Looping is more useful for boss type monsters
    [Tooltip("Whether or not an enemy should continue looping through the path")]
    [SerializeField] bool shouldLoop = false;

    WaveConfig waveConfig;

    /**
     * Cached waypoints from the wave config
     */
    List<Transform> waypoints;


    /**
     * Index of the index of the current waypoint that we're currently headed towards
     */
    int targetWaypointIndex = 0;

    /**
     * Prevents us from moving if we are currently in a dying sequence
     */
    bool isDying = false;

    void Start() {
        waypoints = waveConfig.GetWaypoints();
        transform.position = waypoints[targetWaypointIndex].transform.position;
    }

    void Update() {
        if (!isDying) {
            Move();
        }
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
            if (shouldLoop) {
                // Note: If we are looping, the first path waypoint is presumed to be the entryway
                // and should therefore be ignored after the first time
                targetWaypointIndex = 1;
            } else {
                Destroy(gameObject);
            }
        }
    }

    public void setWaveConfig(WaveConfig waveConfig) {
        this.waveConfig = waveConfig;
    }

    /**
     * Triggers our dying sequence, i.e. stops moving in case the sequence is longer lasting
     */
    public void OnStartDeathSequence() {
        isDying = true;
    }
}
