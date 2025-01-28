using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MeleeWeaponSystem;

public class Bullet : MonoBehaviour
{
    public float speed;
    public int damage;

    private Rigidbody2D _rb;
    private PlayerControls _PlayerControles;

    private float maxTime = 10f;
    private float timer;

    void Start() {
        _rb = GetComponent<Rigidbody2D>();
        _PlayerControles = FindObjectOfType<PlayerControls>();
        timer = maxTime;
    }

    private void Update() {
        if (_PlayerControles != null) {
            if (!_PlayerControles.inMenu) {
                _rb.velocity = transform.up * speed;
            } else if (_PlayerControles.inMenu) {
                _rb.velocity = transform.up * 0;
            }
        }

        timer -= Time.deltaTime;

        if (timer <= 0) {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision) {
        HealthSystem healthSystem = collision.gameObject.GetComponent<HealthSystem>();

        if (healthSystem != null) {
            healthSystem.TakeDamage(damage);
        }

        if (!collision.gameObject.CompareTag("Player")) {
            Destroy(gameObject);
        }
    }
}
