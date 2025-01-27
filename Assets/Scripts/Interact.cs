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
    [SerializeField] private Canvas storeMenu;

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
        if (storeMenu != null) {
            storeMenu.gameObject.SetActive(true);
            _PlayerControls.inMenu = true;
        }
    }
}
