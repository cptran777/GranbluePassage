using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This class is meant to be attached to objects representing entities that have
 * some health and are able to take damage
 */
public class EntityHealthHandler : MonoBehaviour {
    [Tooltip("Sets the player's maximum health")]
    [SerializeField] int maxHealth = 2000;

    [Tooltip("Serialized for debugging purposes only")]
    [SerializeField] int currentHealth = 2000;

    /**
     * We want to stop collision detections if we are in a dying state. Useful for entities that have
     * a long death sequence and may still be getting hit
     */
    bool isDying = false;

    void Start() {
        // Normalization in case we accidentally changed max to be lower than current 
        currentHealth = maxHealth;
    }

    void Update() {
        
    }

    private void OnTriggerEnter2D(Collider2D collidedEntity) {
        DamageDealer damageDealer = collidedEntity.GetComponent<DamageDealer>();
        if (!damageDealer) {
            return;
        }

        if (!isDying) {
            TakeDamage(damageDealer);
            damageDealer.Hit();
        }
    }

    /**
     * Given a damage dealer class handler, we will take the appropriate amount of damage
     * from whatever has collided
     */
    private void TakeDamage(DamageDealer damageDealer) {
        int damage = damageDealer.GetDamage();
        currentHealth -= damage;
        if (currentHealth <= 0) {
            isDying = true;
            SendMessage("OnStartDeathSequence");
        } else {
            SendMessage("OnStartHitSequence");
        }
    }

    public int GetCurrentHealth() { return currentHealth; }

    public void RestoreCurrentHealth(int amount) {
        currentHealth = Mathf.Min(maxHealth, currentHealth + amount);
    }

    public int GetMaxHealth() { return maxHealth; }
}
