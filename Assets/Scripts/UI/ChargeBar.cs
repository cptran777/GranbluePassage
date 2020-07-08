using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChargeBar : MonoBehaviour {
    PlayerState playerState;
    Image chargeBarImage;
    TextMeshProUGUI chargeBarText;

    void Start() {
        playerState = FindObjectOfType<PlayerState>();
        chargeBarImage = GetComponent<Image>();
        chargeBarText = GetComponentInChildren<TextMeshProUGUI>();
    }

    void Update() {
        chargeBarImage.fillAmount = playerState.GetChargeBarPercentage();
        int chargeBarAmount = Mathf.RoundToInt(playerState.GetChargeBarPercentage() * 100);
        chargeBarText.text = chargeBarAmount.ToString() + "%";
    }
}
