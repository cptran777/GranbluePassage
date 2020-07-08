using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This class acts as a connector between an enemy and the player state as there are
 * items in each enemy that affects player state upon certain events. This module is
 * separated from the EnemyHandler class in order to reduce complexity of the main class
 */
public class EnemyToPlayerStateHandler : MonoBehaviour, IEntity {
    [Tooltip("The amount of charge bar this enemy is worth on a kill")]
    [SerializeField] float chargeBarKillValue = 5;

    float chargeBarHitValue = 0.5f;

    PlayerState playerState;


    void Start() {
        playerState = FindObjectOfType<PlayerState>();
        
    }

    void Update() {
        
    }

    public void OnStartDeathSequence() {
        playerState.AddToChargeBar(chargeBarKillValue);
    }

    public void OnStartHitSequence() {
        playerState.AddToChargeBar(chargeBarHitValue);
    }
}
