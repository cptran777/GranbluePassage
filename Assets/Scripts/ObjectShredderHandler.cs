using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Destroys anything that enters it. Meant to eat lasers or other objects that
 * fly off the screen. Might delete and refactor to another way, not sure yet.
 */
public class ObjectShredderHandler : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D collision) {
        Destroy(collision.gameObject);
    }
}
