using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour {
    TextMeshProUGUI textComponent;
    GameSession gameSession;

    void Start() {
        gameSession = FindObjectOfType<GameSession>();
        textComponent = GetComponent<TextMeshProUGUI>();
    }

    void Update() {
        textComponent.text = gameSession.GetScore().ToString();
    }
}
