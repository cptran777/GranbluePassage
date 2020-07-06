using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * Manages the transitions between levels
 */
public class LevelHandler : MonoBehaviour {
    public void LoadGameOver() {
        SceneManager.LoadScene("Game Over");
    }
}
