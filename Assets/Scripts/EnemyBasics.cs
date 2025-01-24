using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static EnemyBasics;
using static RangedWeaponSystem;

public class EnemyBasics : MonoBehaviour
{
    [System.Serializable]
    public class EnemyType
    {
        public string enemyName;
        public int damage;
        public float speed;
        public float cooldown;
        public float maxViewDistance;
        public float rotationSpeed;
    }

    public List<EnemyType> Enemy = new List<EnemyType>();
    [SerializeField] private int currentEnemyIndex;

    [SerializeField] GameObject player;
    NavMeshAgent agent;

    private float lastSwitchTime;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update()
    {
        EnemyType enemyType = Enemy[currentEnemyIndex];
        agent.speed = enemyType.speed;

        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        
        if (distanceToPlayer <= enemyType.maxViewDistance)
        {
            agent.SetDestination(player.transform.position);
        } else
        {
            agent.ResetPath();
        }

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        HealthSystem healthSystem = collision.gameObject.GetComponent<HealthSystem>();
        EnemyType enemyType = Enemy[currentEnemyIndex];

        if (Time.time < lastSwitchTime + enemyType.cooldown) return;
        lastSwitchTime = Time.time;

        if (healthSystem != null)
        {
            healthSystem.TakeDamage(enemyType.damage);
        }
    }
}
