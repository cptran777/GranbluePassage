using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Wave Config")]
public class WaveConfig : ScriptableObject {
    [Tooltip("The enemy prefab object that will be spawned for the wave")]
    [SerializeField] GameObject enemyPrefab;

    [Tooltip("Set path for the particular enemy this script is attached to to follow")]
    [SerializeField] GameObject pathPrefab;

    [Tooltip("Time between enemies spawning within the same wave")]
    [SerializeField] float timeBetweenSpawns = 0.5f;

    [Tooltip("Introduces some randomness to time between enemy spawns to make the game feel more organic")]
    [SerializeField] float timeBetweenSpawnsRandomFactor = 0.3f;

    [Tooltip("Number of enemies per wave")]
    [SerializeField] int numberOfEnemies = 1;

    [SerializeField] float moveSpeed = 1;

    public GameObject GetEnemyPrefab() { return enemyPrefab; }
    public GameObject GetPathPrefab() { return pathPrefab; }
    public float GetTimeBetweenSpawns() { return timeBetweenSpawns; }
    public float GetTimeBetweenSpawnsRandomFactor() { return timeBetweenSpawnsRandomFactor; }
    public int GetNumberOfEnemies() { return numberOfEnemies; }
    public float GetMoveSpeed() { return moveSpeed; }

    public List<Transform> GetWaypoints() {
        var waveWaypoints = new List<Transform>();

        foreach (Transform childWaypoint in pathPrefab.transform) {
            waveWaypoints.Add(childWaypoint);              
        }

        return waveWaypoints;
    }
}
