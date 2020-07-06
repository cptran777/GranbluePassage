using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Basic handler class for a typical enemy object
 */
public class EnemyHandler : MonoBehaviour, IEntity {
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

    [Tooltip("The sound effect that plays when the enemy shoots")]
    [SerializeField] AudioClip projectileSFX;

    [Tooltip("The sound effect that plays when the enemy is dead")]
    [SerializeField] AudioClip deathSFX;

    [Tooltip("The sound effect volume for the projectile SFX")]
    [SerializeField] float projectileSFXVolume = 0.5f;

    [Tooltip("The sound effect volume for the death SFX")]
    [SerializeField] float deathSFXVolume = 0.75f;

    /**
     * Reference access to the sprite renderer of this enemy object
     */
    SpriteRenderer spriteRenderer;

    /**
     * Useful flag to trigger when the enemy has a more complex dying sequence than simply 
     * disappearing, and we want to flag this to prevent unnecessary behaviors during that
     * sequence
     */
    bool isDying = false;

    /**
     * Counts down until the enemy ship's next attack
     */
    float fireTimer = 1f;

    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        SetFireTimer();
    }

    void Update() {
        if (!isDying) {
            CountdownFireTimerAndShoot();
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

        AudioSource.PlayClipAtPoint(projectileSFX, Camera.main.transform.position, projectileSFXVolume);
        // Quaternion.identity => keep the default rotation
        GameObject projectile = Instantiate(projectileObject, position, Quaternion.identity);
    }

    private void SetFireTimer() {
        fireTimer = Random.Range(fireRate - fireRateVariationFactor, fireRate + fireRateVariationFactor);
    }

    public void OnStartDeathSequence() {
        isDying = true;
        StartCoroutine(EnemyDeathSequence());
    }

    /**
     * Starts the death sequence. Since we want this to take place over a certain period of time AND
     * be framerate independent, using a coroutine with fake "frame" units to animate this over a 
     * certain period
     */
    private IEnumerator EnemyDeathSequence() {
        float totalSequenceTime = 1;
        int totalSequenceFrames = 25;
        float currentFrame = 0;

        // Changes our character look to a more dying state
        spriteRenderer.color = new Color(0.8f, 0.1f, 0.1f);

        Instantiate(deathVFX, transform.position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathSFXVolume);

        while (currentFrame < 3) {
            //Reduces the transparency of our avatar as they die per "frame"
            var color = spriteRenderer.color;
            color.a -= 0.04f;
            spriteRenderer.color = color;

            currentFrame += totalSequenceTime / totalSequenceFrames;

            yield return new WaitForSeconds(totalSequenceTime / totalSequenceFrames);
        }

        Destroy(gameObject);
    }
}
