using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MeleeWeaponSystem;

public class Bullet : MonoBehaviour
{
    public float speed;
    public int damage;
    private Rigidbody2D rb;
    private PlayerControles _PlayerControles;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _PlayerControles = FindObjectOfType<PlayerControles>();
    }

    private void Update()
    {
        if (_PlayerControles != null)
        {
            if (!_PlayerControles.inMenu)
            {
                rb.velocity = transform.up * speed;
            }
            else if (_PlayerControles.inMenu)
            {
                rb.velocity = transform.up * 0;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        HealthSystem healthSystem = collision.gameObject.GetComponent<HealthSystem>();
        if (healthSystem != null)
        {
            healthSystem.TakeDamage(damage);
        }
        if (!collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
