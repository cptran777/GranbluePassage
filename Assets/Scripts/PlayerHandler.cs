using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

/**
 * Main class in charge of controlling the player object
 */
public class PlayerHandler : MonoBehaviour {
    [Tooltip("Modifier to change normalized player speed independent of frame rate")]
    [SerializeField] float playerSpeedModifier = 5f;

    [Tooltip("Reference to the game object used as the laser for the player")]
    [SerializeField] GameObject laserObject;

    [Tooltip("Relative position of the laser based on the height of the player")]
    [SerializeField] float relativeLaserYSpawnOffset = 4f;

    [Tooltip("Relative position of the laser spawn based on the width of the player")]
    [SerializeField] float relativeLaserXSpawnOffset = 3f;

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

    void Start() {
        mainCamera = Camera.main;
        spriteRenderer = GetComponent<SpriteRenderer>();
        SetupBoundaries();
    }

    void Update() {
        Move();
        FireLaser();
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
    private void FireLaser() {
        if (CrossPlatformInputManager.GetButtonDown("Fire1")) {
            Vector3 spriteSize = spriteRenderer.bounds.size;

            Vector3 position = new Vector3(
                transform.position.x + spriteSize.x / relativeLaserXSpawnOffset,
                transform.position.y + spriteSize.y / relativeLaserYSpawnOffset,
                transform.position.z
            );
            // Quaternion.identity => keep the default rotation
            GameObject laser = Instantiate(laserObject, position, Quaternion.identity);
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
}
