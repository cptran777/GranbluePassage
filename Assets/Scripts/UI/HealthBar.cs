using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour {
    /**
     * Reference to the entity health handler on the player object specifically
     */
    EntityHealthHandler playerHealthHandler;

    Image healthBarImage;

    TextMeshProUGUI healthBarText;

    void Start() {
        playerHealthHandler = FindObjectOfType<PlayerHandler>().GetComponent<EntityHealthHandler>();
        healthBarImage = GetComponent<Image>();
        healthBarText = GetComponentInChildren<TextMeshProUGUI>();
    }

    void Update() {
        float currentHealth = Mathf.Max(playerHealthHandler.GetCurrentHealth(), 0);
        float maxHealth = playerHealthHandler.GetMaxHealth();
        float currentHealthPercentage = currentHealth / maxHealth;

        healthBarImage.fillAmount = currentHealthPercentage;
        healthBarText.text = currentHealth.ToString();
    }
}
