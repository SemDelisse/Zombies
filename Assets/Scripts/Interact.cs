using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class Interact : MonoBehaviour
{

    // This script is on environment objects that can be interacted with
    // With this script on the object you can set it to different kind of interactions
    // Like open a menu, trigger something or other things.

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

    public void ActivateInteraction() {
        // If the trigger object is for the shop there needs to be a shop canvas.
        // Here it checks for it and set it active or inactive based on what it currently is.
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
