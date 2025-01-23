using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShootingWeaponSystem : MonoBehaviour
{
    [System.Serializable]
    public class ShootingWeapon
    {
        public string weaponName;
        public int damage;
        public float bulletSpeed;
        public float attackSpeed;
        public float cooldown;
        public GameObject bulletPrefab;
    }

    public List<ShootingWeapon> weapons = new List<ShootingWeapon>();
    [SerializeField] private int currentWeaponIndex;

    private float cooldownTimer;
    [SerializeField] private Transform shootingPoint;

    void Update()
    {
        cooldownTimer -= Time.deltaTime;
    }

    public void PerformAttack()
    {
        if (cooldownTimer > 0) return;
        ShootingWeapon weapon = weapons[currentWeaponIndex];
        cooldownTimer = weapon.cooldown;

        GameObject bulletInstance = Instantiate(weapon.bulletPrefab, shootingPoint.position, transform.rotation);
        Bullet bulletScript = bulletInstance.GetComponent<Bullet>();

        if (bulletScript != null)
        {
            bulletScript.damage = weapon.damage;
            bulletScript.speed = weapon.bulletSpeed;
        }
    }

    void OnDrawGizmos()
    {
        if (shootingPoint == null)
            return;

        Vector2 start = shootingPoint.position;
        Vector2 direction = shootingPoint.right;

        float maxDistance = 10f;
        RaycastHit2D hit = Physics2D.Raycast(start, direction, maxDistance);

        float hitDistance = hit.collider != null ? hit.distance : maxDistance;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(start, start + direction * hitDistance);

        if (hit.collider != null)
        {
            Gizmos.DrawWireSphere(hit.point, 0.1f);
        }
    }
}
