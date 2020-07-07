using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {
    [Tooltip("The second spawner that should appear after some time to add difficulty")]
    [SerializeField] GameObject secondaryEnemySpawner;

    [Tooltip("The game objects that hold the spawners for each boss")]
    [SerializeField] List<GameObject> bossSpawners;

    [SerializeField] float timeToSpawnSecondaryEnemies = 10f;

    [SerializeField] float timeBetweenBossSpawns = 15f;

    [SerializeField] float timeBetweenBossSpawnsRandomFactor = 5f;

    int currentBossSpawnerIndex = 0;

    void Start() {
        StartCoroutine(InitSecondaryEnemySpawner());
        StartCoroutine(WaitAndInitBossSpanwer());
    }

    public void NotifyBossDefeated() {
        StartCoroutine(WaitAndInitBossSpanwer());
    }

    private IEnumerator InitSecondaryEnemySpawner() {
        yield return new WaitForSeconds(timeToSpawnSecondaryEnemies);
        Instantiate(secondaryEnemySpawner);
    }

    private IEnumerator WaitAndInitBossSpanwer() {
        yield return new WaitForSeconds(
            Random.Range(timeBetweenBossSpawns - timeBetweenBossSpawnsRandomFactor, timeBetweenBossSpawns + timeBetweenBossSpawnsRandomFactor)
        );

        if (currentBossSpawnerIndex < bossSpawners.Count) {
            Instantiate(bossSpawners[currentBossSpawnerIndex]);
            currentBossSpawnerIndex += 1;
        }
    }
}
