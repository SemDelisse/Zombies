using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{

    // In this class a lot of player controls and player movement gets registered here
    // Some voids are refering to other scripts where more things are happening surroundings the player controls

    // The player controls script is on the player and works with the events in unity input system
    // All input of the player that is needed for this game goes through this script and if needed to other scripts

    [Header("Scripts and UI")]
    private ShopSystem _ShopSystem;
    private Interact _Interact;
    [SerializeField] private Canvas pauseMenu;
    [SerializeField] private Canvas inventoryMenu;
    public bool inMenu;

    [Header("Movement")]
    private Rigidbody2D _rb;
    public float moveSpeed = 5f;
    public float maxSprint = 1.5f;
    private float sprintMultiplier = 1f;
    private Vector2 moveInput;
    private Vector2 mousePosition;
    
    [Header("Attack System")]
    private MeleeWeaponSystem _MeleeWeaponSystem;
    private RangedWeaponSystem _RangedWeaponSystem;
    public enum WeaponType { Melee, Ranged }
    public WeaponType currentWeapon;
    private float lastSwitchTime;
    private float switchCooldown = 0.1f;

    void Start() {
        _rb = GetComponent<Rigidbody2D>();
        _MeleeWeaponSystem = GetComponent<MeleeWeaponSystem>();
        _RangedWeaponSystem = GetComponent<RangedWeaponSystem>();
        _Interact = GetComponent<Interact>();
        currentWeapon = WeaponType.Ranged;
    }

    private void Update() {
        if (!inMenu) {
            _rb.velocity = moveInput * moveSpeed * sprintMultiplier;

            mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
            transform.up = direction;
        } else {
            _rb.velocity = moveInput * 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        Interact interactable = collision.GetComponent<Interact>();
        if (interactable != null) {
            _Interact = interactable;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (_Interact != null && collision.GetComponent<Interact>() == _Interact && !inMenu) {
            _Interact = null;
        }
    }

    public void Move(InputAction.CallbackContext context) {
        moveInput = context.ReadValue<Vector2>();
    }

    public void Sprint(InputAction.CallbackContext context) {
        if (context.started) {
            sprintMultiplier = maxSprint;
        }
        else if (context.canceled) {
            sprintMultiplier = 1f;
        }
    }

    public void Interact() {
        Interact[] objectsWithScript = FindObjectsOfType<Interact>();
        foreach (Interact obj in objectsWithScript)
        {
            if (obj.playerIsHere == true)
            {
                obj.ActivateInteraction();
            }
        }
    }

    public void SwitchWeapon(InputAction.CallbackContext context) {
        if (Time.time < lastSwitchTime + switchCooldown) return;
        float scrollValue = context.ReadValue<float>();

        if (scrollValue > 0 || scrollValue < 0) {
            lastSwitchTime = Time.time;
            if (currentWeapon == WeaponType.Melee) {
                Debug.Log("Weapon typ Ranged activated");
                currentWeapon = WeaponType.Ranged;
            } else if (currentWeapon == WeaponType.Ranged) {
                Debug.Log("Weapon typ Melee activated");
                currentWeapon = WeaponType.Melee;
            }
        }
    }

    public void Attack() {
        if (currentWeapon == WeaponType.Melee) {
            _MeleeWeaponSystem.PerformAttack();
        }
        else if (currentWeapon == WeaponType.Ranged) {
            _RangedWeaponSystem.PerformAttack();
        }
    }

    public void Use() {
        
    }

    public void Escape() {
        if (inMenu) {
            Interact[] objectsWithScript = FindObjectsOfType<Interact>();
            foreach (Interact obj in objectsWithScript) {
                if (obj.shopMenu != null) {
                    if (obj.shopMenu.gameObject.activeSelf) {
                        obj.ActivateInteraction();
                    }
                }
            }

            if (pauseMenu != null) {
                if (pauseMenu.gameObject.activeSelf) {
                    Pause();
                }
            } else if (inventoryMenu != null) {
                if (inventoryMenu.gameObject.activeSelf) {
                    Inventory();
                }
            }
        } else {
            Pause();
        }
    }

    public void Inventory() {
        if (inventoryMenu != null) {
            if (inventoryMenu.gameObject.activeSelf) {
                inventoryMenu.gameObject.SetActive(false);
                inMenu = false;
            }
            else if (!inventoryMenu.gameObject.activeSelf) {
                inventoryMenu.gameObject.SetActive(true);
                inMenu = true;
            }
        }
    }

    public void Pause() {
        if (pauseMenu != null) {
            if (pauseMenu.gameObject.activeSelf) {
                pauseMenu.gameObject.SetActive(false);
                inMenu = false;
            }
            else if (!pauseMenu.gameObject.activeSelf) {
                pauseMenu.gameObject.SetActive(true);
                inMenu = true;
            }
        }
    }
}