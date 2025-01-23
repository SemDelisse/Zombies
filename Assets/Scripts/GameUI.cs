using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    [Header("Player Info")]
    [SerializeField] private HealthSystem _HealthSystem;
    [SerializeField] private int health;
    [SerializeField] private int gold = 0;
    [SerializeField] private int silver = 0;
    [SerializeField] private int ammo = 0;
    [SerializeField] private string equipedWeapon;
    [SerializeField] private string equipedItem;

    [Header("Game Info")]
    [SerializeField] private int enemiesLeft;
    [SerializeField] private int roundNumber = 1;
    // Game time and time till next round also needs to be in this script

    // Start is called before the first frame update
    void Start()
    {
        health = _HealthSystem.maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
