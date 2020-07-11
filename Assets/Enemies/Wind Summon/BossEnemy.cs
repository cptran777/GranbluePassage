using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : MonoBehaviour, IEntity {
    EnemyManager enemyManager;

    void Start() {
        enemyManager = FindObjectOfType<EnemyManager>();        
    }

    public void OnStartDeathSequence() {
        enemyManager.NotifyBossDefeated();
    }

    public void OnStartHitSequence() { /* Do nothing */ }
}
