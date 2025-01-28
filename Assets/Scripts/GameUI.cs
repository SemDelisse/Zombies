using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static MeleeWeaponSystem;
using static RangedWeaponSystem;
using static PlayerControls;

public class GameUI : MonoBehaviour
{

    [Header("Scripts")]
    private PlayerControls _PlayerControles;
    private HealthSystem _HealthSystem;
    private MeleeWeaponSystem _MeleeWeaponSystem;
    private RangedWeaponSystem _RangedWeaponSystem;

    [Header("Player Info")]
    [SerializeField] private int health;
    [SerializeField] private int mana;
    public int gold = 0;
    public int silver = 0;
    public int ammo = 0;
    public int killCount = 0;
    [SerializeField] private string currentEquipedWeapon;
    [SerializeField] private string secondEquipedWeapon;
    [SerializeField] private string equipedItem;

    [Header("Game Info")]
    [SerializeField] private float totalGameTime;
    [SerializeField] private int enemiesLeft;
    [SerializeField] private int roundNumber = 1;
    [SerializeField] private float nextRoundIn;
    // Game time and time till next round also needs to be in this script

    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null) {
            _PlayerControles = player.GetComponent<PlayerControls>();
            _HealthSystem = player.GetComponent<HealthSystem>();
            _MeleeWeaponSystem = player.GetComponent<MeleeWeaponSystem>();
            _RangedWeaponSystem = player.GetComponent<RangedWeaponSystem>();
        }
    }

    // Update is called once per frame
    void Update() {
        MeleeWeapon meleeWeapon = _MeleeWeaponSystem.weapons[_MeleeWeaponSystem.currentWeaponIndex];
        RangedWeapon rangedWeapon = _RangedWeaponSystem.weapons[_RangedWeaponSystem.currentWeaponIndex];

        health = _HealthSystem.maxHealth;

        if (_PlayerControles.currentWeapon == WeaponType.Melee) {
            currentEquipedWeapon = meleeWeapon.weaponName;
            secondEquipedWeapon = rangedWeapon.weaponName;
        } else if (_PlayerControles.currentWeapon != WeaponType.Ranged) {
            currentEquipedWeapon = rangedWeapon.weaponName;
            secondEquipedWeapon = meleeWeapon.weaponName;
        }
    }
}
