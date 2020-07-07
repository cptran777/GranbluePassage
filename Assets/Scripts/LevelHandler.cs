using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * Manages the transitions between levels
 */
public class LevelHandler : MonoBehaviour {
    private void Awake() {
        if (FindObjectsOfType<LevelHandler>().Length > 1) {
            Destroy(gameObject);
        }
    }

    public void LoadGameOver(float delay) {
        StartCoroutine(WaitAndLoadScene("Game Over", delay));
    }

    public void LoadMainMenu(float delay) {
        StartCoroutine(WaitAndLoadScene("Menu", delay));
    }

    public void LoadGame(float delay) {
        StartCoroutine(WaitAndLoadScene("Main", delay));
    }

    public void QuitApplication() {
        Application.Quit();
    }

    private IEnumerator WaitAndLoadScene(string sceneName, float delay) {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }
}
