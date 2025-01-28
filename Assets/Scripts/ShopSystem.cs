using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ShopSystem : MonoBehaviour
{
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

    public void Store() {
        
    }
}
