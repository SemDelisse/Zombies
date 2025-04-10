using UnityEngine;
using System.Collections.Generic;

public class MeleeWeaponSystem : MonoBehaviour
{
    [System.Serializable]
    public class MeleeWeapon {
        public string weaponName;
        public int damage;
        public float attackDistance;
        public float coneAngle;
        public float cooldown;
        public int maxTargetsToCheck;
    }

    public List<MeleeWeapon> weapons = new List<MeleeWeapon>();
    public int currentWeaponIndex;

    [SerializeField] private LayerMask targetLayer;
    private int absoluteMaxTargets = 50;

    private Vector2 attackDirection;
    private float cooldownTimer;
    private Collider2D[] hitColliders;

    private void Awake() => hitColliders = new Collider2D[absoluteMaxTargets];


    void Update() {
        cooldownTimer -= Time.deltaTime;
        UpdateAttackDirection();
    }

    void UpdateAttackDirection() {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        attackDirection = (mouseWorldPos - transform.position).normalized;
    }

    public void PerformAttack() {
        if (cooldownTimer > 0) return;

        MeleeWeapon weapon = weapons[currentWeaponIndex];
        cooldownTimer = weapon.cooldown;

        int hitCount = Physics2D.OverlapCircleNonAlloc(
            transform.position,
            weapon.attackDistance,
            hitColliders,
            targetLayer
        );

        int processedTargets = 0;
        for (int i = 0; i < hitCount && processedTargets < weapon.maxTargetsToCheck; i++) {
            if (hitColliders[i] == null) continue;

            if (IsColliderInCone(transform.position, attackDirection,
                weapon.attackDistance, weapon.coneAngle * 0.5f, hitColliders[i])) {
                if (hitColliders[i].TryGetComponent<HealthSystem>(out HealthSystem health)) {
                    health.TakeDamage(weapon.damage);
                    processedTargets++;
                }
            }
        }
    }

    bool IsColliderInCone(Vector2 origin, Vector2 direction, float radius, float halfAngle, Collider2D col) {
        Bounds bounds = col.bounds;
        Vector2[] points = {
            bounds.center,
            new Vector2(bounds.min.x, bounds.min.y),
            new Vector2(bounds.max.x, bounds.max.y),
            new Vector2(bounds.min.x, bounds.max.y),
            new Vector2(bounds.max.x, bounds.min.y)
        };

        foreach (Vector2 point in points) {
            Vector2 toPoint = point - origin;
            if (toPoint.magnitude > radius) continue;

            float angle = Vector2.Angle(direction, toPoint.normalized);
            if (angle <= halfAngle) return true;
        }
        return false;
    }

    void OnDrawGizmos() {
        if (weapons == null || weapons.Count == 0 || !Application.isPlaying) return;

        MeleeWeapon weapon = weapons[currentWeaponIndex];
        Vector2 origin = transform.position;
        Vector2 forward = attackDirection.normalized * weapon.attackDistance;
        Vector2 left = Quaternion.Euler(0, 0, weapon.coneAngle * 0.5f) * forward;
        Vector2 right = Quaternion.Euler(0, 0, -weapon.coneAngle * 0.5f) * forward;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(origin, origin + forward);
        Gizmos.DrawLine(origin, origin + left);
        Gizmos.DrawLine(origin, origin + right);
        Gizmos.DrawLine(origin + left, origin + forward);
        Gizmos.DrawLine(origin + right, origin + forward);
    }
}