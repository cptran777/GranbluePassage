using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour {
    int score = 0;

    void Start() {
        
    }

    public int GetScore() {
        return score;
    }

    public void AddToScore(int points) {
        score += points;
    }
}
