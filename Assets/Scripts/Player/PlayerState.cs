using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour {
    /**
     * The amount of experience points a player has earned
     */
    int experience = 0;

    /**
     * The amount of charge the player has towards their next charge attack
     */
    float chargeBar = 0;

    /**
     * The amount the charge bar value that the player needs to use their charge
     * attack
     */
    float chargeBarMax = 100;

    void Start() {
        
    }

    void Update() {
        
    }

    /**
     * Returns the amount of experience the player has toward their next level
     */
    public int GetExperience() { return experience; }

    public void AddExperience(int amount) {
        experience += amount;
    }

    /**
     * Returns the amount of fill for the charge bar as a percentage between 0 and 1
     */
    public float GetChargeBarPercentage() {
        return chargeBar / chargeBarMax;
    }

    /**
     * Adds amount fo the charge bar. The amount should be a decimal number between 0 and 100
     */
    public void AddToChargeBar(float amount) {
        chargeBar = Mathf.Min(chargeBarMax, chargeBar + amount);
    }
}
