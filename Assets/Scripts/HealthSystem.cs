using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    private GameUI _GameUI;
    public int maxHealth = 5;
    public int currentHealth;

    void Start() {
        _GameUI = FindObjectOfType<GameUI>();
        currentHealth = maxHealth;
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
        }
    }
}
