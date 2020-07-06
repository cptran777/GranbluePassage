using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    [Tooltip("List of all of our wave configurations to use")]
    [SerializeField] List<WaveConfig> waveConfigs;

    [Tooltip("Starts the waves at a specified index and goes on from there")]
    [SerializeField] int startingWaveIndex = 0;

    [Tooltip("Whether or not we are looping the enemy spawns")]
    [SerializeField] bool isLooping = false;

    IEnumerator Start() {
        do {
            yield return StartCoroutine(SpawnAllEnemyWaves());
        } while (isLooping);
    }

    void Update() {
        
    }

    /**
     * Spawns all the waves, one after another
     */
    private IEnumerator SpawnAllEnemyWaves() {
        for (int waveIndex = startingWaveIndex; waveIndex < waveConfigs.Count; waveIndex++) {
            var currentWave = waveConfigs[waveIndex];
            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
        }
    }

    /**
     * For each wave, we want to spawn every enemy in the wave based on the configuration
     */
    private IEnumerator SpawnAllEnemiesInWave(WaveConfig wave) {
        int enemiesCount = wave.GetNumberOfEnemies();
        for (int x = 0; x < enemiesCount; x++) {
            var newEnemy = Instantiate(
                wave.GetEnemyPrefab(),
                wave.GetWaypoints()[0].transform.position,
                Quaternion.identity
            );

            newEnemy.GetComponent<EnemyPathing>().setWaveConfig(wave);

            float timeBetweenSpawns = wave.GetTimeBetweenSpawns();
            float spawnTimeRandomFactor = wave.GetTimeBetweenSpawnsRandomFactor();

            float waitTime = UnityEngine.Random.Range(
                timeBetweenSpawns - spawnTimeRandomFactor,
                timeBetweenSpawns + spawnTimeRandomFactor
            );

            yield return new WaitForSeconds(waitTime);
        }

    }
}
