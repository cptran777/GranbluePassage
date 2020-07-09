using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCanDropPowerUp : MonoBehaviour, IEntity {
    [Tooltip("List of power ups that this enemy can drop upon dying")]
    [SerializeField] List<GameObject> powerUps;

    [Tooltip("Percentage chance that the enemy can drop the item upon dying")]
    [SerializeField] float dropChance = 0.25f;

    public void OnStartDeathSequence() {
        float randomizer = Random.Range(0, 1f);
        if (randomizer < dropChance) {
            GameObject powerUpDropped = powerUps[Random.Range(0, powerUps.Count)];
            Instantiate(powerUpDropped, transform.position, Quaternion.identity);
        }
    }

    public void OnStartHitSequence() {
        // Do nothing
    }
}
