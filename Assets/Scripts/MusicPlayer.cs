using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {
    [SerializeField] List<AudioClip> songs;

    int currentSongIndex = 0;
    AudioSource audioSource;

    void Start() {
        // Aligns ourselves with the camera in order to make sure the volume matches up correctly
        transform.position = Camera.main.transform.position;
        audioSource = GetComponent<AudioSource>();
    }

    void Update() {
        if (!audioSource.isPlaying) {
            audioSource.PlayOneShot(songs[currentSongIndex]);
            currentSongIndex += 1;
            if (currentSongIndex >= songs.Count) {
                currentSongIndex = 0;
            }
        }
    }
}
