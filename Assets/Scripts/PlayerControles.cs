using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControles : MonoBehaviour
{
    private Rigidbody2D rb;
    public bool inMenu = false;

    [Header("Movement")]
    public float moveSpeed = 5f;
    private Vector2 moveInput;
    private Vector2 mousePosition;

    [Header("Attack System")]
    private MeleeWeaponSystem _MeleeWeaponSystem;
    private ShootingWeaponSystem _RangedWeaponSystem;
    private enum WeaponType { Melee, Ranged }
    private WeaponType currentWeapon;
    private float lastSwitchTime;
    [SerializeField] private float switchCooldown = 0.1f;

    [Header("Item System")]

    [Header("Inventory")]
    [SerializeField] private Canvas inventoryMenu;

    [Header("Pause menu")]
    [SerializeField] private Canvas pauseMenu;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _MeleeWeaponSystem = GetComponent<MeleeWeaponSystem>();
        _RangedWeaponSystem = GetComponent<ShootingWeaponSystem>();
        currentWeapon = WeaponType.Melee;
    }

    private void Update()
    {
        if (!inMenu)
        {
            rb.velocity = moveInput * moveSpeed;

            mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
            transform.up = direction;
        }
        else
        {
            rb.velocity = moveInput * 0;
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void Interact(InputAction.CallbackContext context)
    {
        
    }

    public void SwitchWeapon(InputAction.CallbackContext context)
    {
        if (Time.time < lastSwitchTime + switchCooldown) return;
        float scrollValue = context.ReadValue<float>();

        if (scrollValue > 0)
        {
            Debug.Log("ranged");
            currentWeapon = WeaponType.Ranged;
        }
        else if (scrollValue < 0)
        {
            Debug.Log("melee");
            currentWeapon = WeaponType.Melee;
        }
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (currentWeapon == WeaponType.Melee)
        {
            _MeleeWeaponSystem.PerformAttack();
        }
        else if (currentWeapon == WeaponType.Ranged)
        {
            _RangedWeaponSystem.PerformAttack();
        }
    }

    public void Use(InputAction.CallbackContext context)
    {
        
    }

    public void Inventory(InputAction.CallbackContext context)
    {
        if (inventoryMenu != null)
        {
            if (inventoryMenu.gameObject.activeSelf)
            {
                inventoryMenu.gameObject.SetActive(false);
                inMenu = true;
            }
            else if (!inventoryMenu.gameObject.activeSelf)
            {
                inventoryMenu.gameObject.SetActive(true);
                inMenu = false;
            }
        }
    }

    public void Pause()
    {
        if (pauseMenu != null)
        {
            if (pauseMenu.gameObject.activeSelf)
            {
                pauseMenu.gameObject.SetActive(false);
                inMenu = false;
            }
            else if (!pauseMenu.gameObject.activeSelf)
            {
                pauseMenu.gameObject.SetActive(true);
                inMenu = true;
            }
        }
    }
}