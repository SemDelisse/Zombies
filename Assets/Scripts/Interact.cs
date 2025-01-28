using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class Interact : MonoBehaviour
{

    public bool playerIsHere = false;
    private PlayerControls _PlayerControls;

    [Header("Store menu")]
    private ShopSystem _ShopSystem;
    public Canvas shopMenu;

    private void Start() {
        _PlayerControls = FindAnyObjectByType<PlayerControls>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            playerIsHere = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            playerIsHere = false;
        }
    }

    public void ActivateInteration() {
        if (shopMenu != null) {
            if (shopMenu.gameObject.activeSelf) {
                shopMenu.gameObject.SetActive(false);
                _PlayerControls.inMenu = false;
            } else if (!shopMenu.gameObject.activeSelf) {
                shopMenu.gameObject.SetActive(true);
                _PlayerControls.inMenu = true;
            }
        }
    }
}
