using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathVFXHandler : MonoBehaviour {
    [Tooltip("How long to wait before destroying the VFX game object")]
    [SerializeField] float timeBeforeDestroyed = 1f;

    private void Awake() {
        Destroy(gameObject, timeBeforeDestroyed);
    }
}
