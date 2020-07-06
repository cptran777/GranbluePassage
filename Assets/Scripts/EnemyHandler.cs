using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Basic handler class for a typical enemy object
 */
public class EnemyHandler : MonoBehaviour {
    [Tooltip("Amount of health the enemy has before being destroyed")]
    [SerializeField] int health = 100;

    [Tooltip("Rate, in seconds, that the enemy will shoot at the player as time between shots")]
    [SerializeField] float fireRate = 2f;

    [Tooltip("Delta in variation to randomize the fire rate")]
    [SerializeField] float fireRateVariationFactor = 0.8f;

    [Tooltip("Reference to the game object used as the graphic for the enemy projectile")]
    [SerializeField] GameObject projectileObject;

    [Tooltip("Relative position of the projectile based on the height of the enemy")]
    [SerializeField] float relativeProjectileYSpawnOffset = 4f;

    [Tooltip("Relative position of the projectile spawn based on the width of the enemy")]
    [SerializeField] float relativeProjectileXSpawnOffset = 3f;

    [Tooltip("The visual effect game object that shows up upon this enemy's death")]
    [SerializeField] GameObject deathVFX;

    /**
     * Reference access to the sprite renderer of this enemy object
     */
    SpriteRenderer spriteRenderer;

    /**
     * Counts down until the enemy ship's next attack
     */
    float fireTimer = 1f;

    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        SetFireTimer();
    }

    void Update() {
        CountdownFireTimerAndShoot();
        
    }

    private void OnTriggerEnter2D(Collider2D collidedEntity) {
        print("Collision on enemy");
        DamageDealer damageDealer = collidedEntity.GetComponent<DamageDealer>();
        if (!damageDealer) {
            Debug.LogError("Damage dealer not attached to object in collision: " + collidedEntity.name);
            return;
        }

        damageDealer.Hit();
        TakeDamage(damageDealer);
    }

    /**
     * Given a damage dealer class handler, we will take the appropriate amount of damage
     * from whatever has collided
     */
    private void TakeDamage(DamageDealer damageDealer) {
        int damage = damageDealer.GetDamage();
        health -= damage;
        if (health <= 0) {
            StartDeathSequence();
        }
    }

    private void CountdownFireTimerAndShoot() {
        fireTimer -= Time.deltaTime;
        if (fireTimer <= 0) {
            Shoot();
            SetFireTimer();
        }
    }

    private void Shoot() {
        Vector3 spriteSize = spriteRenderer.bounds.size;

        Vector3 position = new Vector3(
            transform.position.x + spriteSize.x / relativeProjectileXSpawnOffset,
            transform.position.y + spriteSize.y / relativeProjectileYSpawnOffset,
            transform.position.z
        );

        // Quaternion.identity => keep the default rotation
        GameObject projectile = Instantiate(projectileObject, position, Quaternion.identity);
    }

    private void SetFireTimer() {
        fireTimer = Random.Range(fireRate - fireRateVariationFactor, fireRate + fireRateVariationFactor);
    }

    private void StartDeathSequence() {
        Instantiate(deathVFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
