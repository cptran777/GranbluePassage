﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

/**
 * Main class in charge of controlling the player object
 */
public class PlayerHandler : MonoBehaviour, IEntity {
    [Header("Movement Parameters")]

    [Tooltip("Modifier to change normalized player speed independent of frame rate")]
    [SerializeField] float playerSpeedModifier = 5f;

    [Header("Shooting Parameters")]

    [Tooltip("Reference to the game object used as the laser for the player")]
    [SerializeField] GameObject laserObject;

    [Tooltip("Relative position of the laser based on the height of the player")]
    [SerializeField] float relativeLaserYSpawnOffset = 4f;

    [Tooltip("Relative position of the laser spawn based on the width of the player")]
    [SerializeField] float relativeLaserXSpawnOffset = 3f;

    [Tooltip("Rate at which the laser fires when the player holds down the fire button, defined as time between shots in seconds")]
    [SerializeField] float laserFireRate = 0.5f;

    [Tooltip("The sound effect that plays when the player shoots")]
    [SerializeField] AudioClip laserSFX;

    [Tooltip("The sound effect that plays when the player is dead")]
    [SerializeField] AudioClip deathSFX;

    [Tooltip("The sound effect volume for the laser SFX")]
    [SerializeField] float laserSFXVolume = 0.5f;

    [Tooltip("The sound effect volume for the death SFX")]
    [SerializeField] float deathSFXVolume = 0.75f;

    /**
     * Reference to the main camera so that we can handle restrictions on player controls based on
     * the view port
     */
    Camera mainCamera;

    /**
     * Sets up the boundaries for the player based on the camera view port so that we don't have the
     * player moving beyond the view or to areas we don't want them to
     */
    float minimumXBoundary;
    float maximumXBoundary;
    float minimumYBoundary;
    float maximumYBoundary;

    /**
     * Reference access to the sprite renderer of this object
     */
    SpriteRenderer spriteRenderer;

    /**
     * If the player has fired their laser, this variable will keep the coroutine associated with that
     * so that we can also stop it on command
     */
    Coroutine laserFiringCoroutine;

    bool isDying = false;

    void Start() {
        mainCamera = Camera.main;
        spriteRenderer = GetComponent<SpriteRenderer>();
        SetupBoundaries();

        if (laserSFX == null) {
            Debug.LogError("laserSFX on object (" + gameObject.name + ") was not found");
        }

        if (deathSFX == null) {
            Debug.LogError("deathSFX on object (" + gameObject.name + ") was not found");
        }
    }

    void Update() {
        if (!isDying) {
            Move();
            HandleUserFireInput();
        }
    }

    /**
     * Sets up the player object's boundaries within the viewport
     */
    private void SetupBoundaries() {
        Vector3 bottomLeftCorner = mainCamera.ViewportToWorldPoint(new Vector3(0, 0));
        Vector3 topRightCorner = mainCamera.ViewportToWorldPoint(new Vector3(1, 1));

        // We need to offset the boundaries so that the player object doesn't clip too far off screen
        Vector3 spriteSize = spriteRenderer.bounds.size;
        float offsetRadius = 3;

        float offsetX = spriteSize.x / offsetRadius;
        float offsetY = spriteSize.y / offsetRadius;

        minimumXBoundary = bottomLeftCorner.x + offsetX;
        maximumXBoundary = topRightCorner.x - offsetX;

        minimumYBoundary = bottomLeftCorner.y + offsetY;
        maximumYBoundary = topRightCorner.y - offsetY;
    }

    /**
     * Upon player fire control, shoots a basic laser ammo from the cross bow
     */
    private void HandleUserFireInput() {
        if (CrossPlatformInputManager.GetButtonDown("Fire1") && laserFiringCoroutine == null) {
            laserFiringCoroutine = StartCoroutine(ContinuousLaserFire());
        }

        if (CrossPlatformInputManager.GetButtonUp("Fire1")) {
            // Since we have multiple ways to shoot, we need to have proper handling for the coroutine or
            // else we will have a scenario where a player clicks the mouse and the fire button and messes
            // everything up
            if (laserFiringCoroutine != null) {
                StopCoroutine(laserFiringCoroutine);
            }
            // Resets the coroutine so that we can actually trigger firing again
            laserFiringCoroutine = null;
        }
    }

    /**
     * Based on input controls, move the player in the specified direction within the game allowed boundaries
     */
    private void Move() {
        float deltaX = CrossPlatformInputManager.GetAxis("Horizontal") * Time.deltaTime * playerSpeedModifier;
        float deltaY = CrossPlatformInputManager.GetAxis("Vertical") * Time.deltaTime * playerSpeedModifier;

        float nextXPostion = Mathf.Clamp(transform.position.x + deltaX, minimumXBoundary, maximumXBoundary);
        float nextYPosition = Mathf.Clamp(transform.position.y + deltaY, minimumYBoundary, maximumYBoundary);
        transform.position = new Vector2(nextXPostion, nextYPosition);
    }

    /**
     * Creates lasers at the player position continuously at the desired fire rate. Set as IEnumerator for
     * coroutines so that we can control the rate of fire. We now have a poor man's debounce
     */
    IEnumerator ContinuousLaserFire() {
        while (true) {
            Vector3 spriteSize = spriteRenderer.bounds.size;

            Vector3 position = new Vector3(
                transform.position.x + spriteSize.x / relativeLaserXSpawnOffset,
                transform.position.y + spriteSize.y / relativeLaserYSpawnOffset,
                transform.position.z
            );
            // Quaternion.identity => keep the default rotation
            GameObject laser = Instantiate(laserObject, position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(laserSFX, Camera.main.transform.position, laserSFXVolume);
            yield return new WaitForSeconds(laserFireRate);
        }
    }

    public void OnStartDeathSequence() {
        isDying = true;
        StartCoroutine(PlayerDeathSequence());
    }

    public void OnStartHitSequence() {
        // Do nothing
    }

    /**
     * Starts the death sequence. Since we want this to take place over a certain period of time AND
     * be framerate independent, using a coroutine with fake "frame" units to animate this over a 
     * certain period
     */
    private IEnumerator PlayerDeathSequence() {
        float totalSequenceTime = 3;
        int totalSequenceFrames = 100;
        float currentFrame = 0;

        if (laserFiringCoroutine != null) {
            StopCoroutine(laserFiringCoroutine);
        }

        // Changes our character look to a more dying state
        spriteRenderer.color = new Color(0.8f, 0.1f, 0.1f);

        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathSFXVolume);

        while (currentFrame < 3) {
            //Reduces the transparency of our avatar as they die per "frame"
            var color = spriteRenderer.color;
            color.a -= 0.01f;
            spriteRenderer.color = color;

            if (Mathf.Approximately(currentFrame, 1.2f) || Mathf.Approximately(currentFrame, 2.4f)) {
                AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathSFXVolume);
            }

            currentFrame += totalSequenceTime / totalSequenceFrames;

            yield return new WaitForSeconds(totalSequenceTime / totalSequenceFrames);
        }

        FindObjectOfType<LevelHandler>().LoadGameOver(2.5f);
        Destroy(gameObject);
    }
}
