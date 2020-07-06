using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour {
    [Tooltip("The amount of damage this object does to another upon collision")]
    [SerializeField] int damage = 100;

    [Tooltip("Whether or not the attached object should destroy itself when hitting something")]
    [SerializeField] bool shouldDestroySelfOnHit = true;

    public int GetDamage() { return damage; }

    public void Hit() {
        if (shouldDestroySelfOnHit) {
            Destroy(gameObject);
        }
    }
}
