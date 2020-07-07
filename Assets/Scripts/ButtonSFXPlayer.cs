using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSFXPlayer : MonoBehaviour {
    [SerializeField] AudioClip buttonSFX;

    public void PlayButtonSFX() {
        AudioSource.PlayClipAtPoint(buttonSFX, Camera.main.transform.position);
    }
}
