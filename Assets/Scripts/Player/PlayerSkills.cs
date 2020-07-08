using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerSkills : MonoBehaviour {
    [Tooltip("References the game object that contains the player's charge attack skill")]
    [SerializeField] GameObject chargeAttack;

    PlayerState playerState;

    void Start() {
        playerState = FindObjectOfType<PlayerState>();    
    }

    void Update() {
        HandleUserSkillInput();
    }

    private void HandleUserSkillInput() {
        if (CrossPlatformInputManager.GetButtonUp("Fire2")) {
            if (Mathf.Approximately(playerState.GetChargeBarPercentage(), 1)) {
                UseChargeAttack();
            }
        }
    }

    private void UseChargeAttack() {
        // Positions the skill in the bottom right quadrant. Quick and easy solution so we don't have to
        // worry about actually trying to place this charge attack strategically
        Vector3 relativePlacement = Camera.main.ViewportToWorldPoint(new Vector3(0.75f, 0.25f));
        // Since we are using the camera's position, we need to offset the z since the camera is "further back"
        relativePlacement.z = 0;

        Instantiate(chargeAttack, relativePlacement, Quaternion.identity);
        playerState.ChargeAttackUsed();
    }
}
