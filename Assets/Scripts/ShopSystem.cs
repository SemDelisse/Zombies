using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ShopSystem : MonoBehaviour
{

    // This is the script for the shop that checks all the logic for buying (or selling) things
    // The script checks if you have enough money or materials and have the right ones
    // It also sends or changes the information in the right scripts

    [Header("Scripts and UI")]
    private PlayerControls _PlayerControls;
    private Interact _Interact;
    private GameUI _GameUI;

    [Header("Shop items and money")]
    private int gold;
    private int silver;
    private int ammo;

    // Start is called before the first frame update
    void Start()
    {
        _PlayerControls = FindObjectOfType<PlayerControls>();
        _Interact = GetComponent<Interact>();
        _GameUI = FindObjectOfType<GameUI>();
        gold = _GameUI.gold;
        silver = _GameUI.silver;
    }

    public void Shop() {
        
    }
}
