using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    private GameUI _GameUI;
    public int maxHealth = 5;
    public int currentHealth;
    [SerializeField] private Slider slider;

    void Start() {
        _GameUI = FindObjectOfType<GameUI>();
        currentHealth = maxHealth;
        UpdateHealthBar(currentHealth, maxHealth);
    }

    public void TakeDamage(int damage) {
        if (currentHealth <= 1) {
            if (gameObject.CompareTag("Player")) {
                // Player Die logic
            }
            else if (gameObject.CompareTag("Enemy")) {
                _GameUI.killCount++;
                Destroy(gameObject);
            }
        } else {
            currentHealth -= damage;
            UpdateHealthBar(currentHealth, maxHealth);
        }
    }

    public void UpdateHealthBar(float currentValue, float maxValue) {
        if (slider != null) {
            slider.value = currentValue / maxValue;
        }
    }
}
