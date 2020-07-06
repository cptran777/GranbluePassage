using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicProjectileHandler : MonoBehaviour {
    [Tooltip("The speed at which the laser will go through the screen after being shot from the player")]
    [SerializeField] float projectileSpeed = -5f;

    /**
     * The rigid body component connected to the laser game object
     */
    Rigidbody2D rigidBodyComponent;

    void Start() {
        rigidBodyComponent = GetComponent<Rigidbody2D>();
        rigidBodyComponent.velocity = new Vector2(projectileSpeed, 0);

        // Since the laser is destined to go offscreen, this prevents it from existing for too long
        // TODO: Get rid of magic number (probably should calculate based on screen size and speed + some offset
        Destroy(gameObject, 20);
    }

    void Update() {
        
    }
}
