using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControles : MonoBehaviour
{
    public Rigidbody2D rb;
    public bool inMenu = false;

    [Header("Movement")]
    public float moveSpeed = 5f;
    private Vector2 moveInput;
    private Vector2 mousePosition;

    [Header("Attack System")]
    // Melee System
    [SerializeField] private MeleeWeaponSystem _MeleeWeaponSystem;

    [Header("Inventory")]
    public Canvas inventoryMenu;

    [Header("Pause menu")]
    public Canvas pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!inMenu)
        {
            // Moving
            rb.velocity = moveInput * moveSpeed;

            // Mouse rotation
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

    public void Attack(InputAction.CallbackContext context)
    {
        _MeleeWeaponSystem.PerformAttack();
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
        if(pauseMenu != null)
        {
            if(pauseMenu.gameObject.activeSelf)
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
