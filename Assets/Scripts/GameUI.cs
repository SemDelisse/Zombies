using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    [Header("Player Info")]
    [SerializeField] private HealthSystem _HealthSystem;
    public int health;
    public int gold = 0;
    public int silver = 0;
    public int ammo = 0;
    public string equipedWeapon;
    public string equipedItem;

    [Header("Game Info")]
    public int enemiesLeft;
    public int roundNumber = 1;
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
