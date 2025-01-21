using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    [Header("Player Items")]
    public int gold = 0;
    public int silver = 0;
    public int ammo = 0;

    [Header("Game Info")]
    public int enemiesLeft;
    public int roundNumber = 1;

    [Header("Equiped Items")]
    public string equipedWeapon;
    public string equipedItem;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
