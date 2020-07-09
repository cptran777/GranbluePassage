using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluePotion : MonoBehaviour {
    [Tooltip("The amount the potion will heal for")]
    [SerializeField] int healAmount = 1500;

    [Tooltip("SFX attached to the healing process")]
    [SerializeField] AudioClip healSFX;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            GameObject player = collision.gameObject;
            EntityHealthHandler playerHealthHandler = player.GetComponent<EntityHealthHandler>();
            playerHealthHandler.RestoreCurrentHealth(healAmount);

            if (healSFX != null) {
                AudioSource.PlayClipAtPoint(healSFX, Camera.main.transform.position);
            }
            Destroy(gameObject);
        }
    }
}
