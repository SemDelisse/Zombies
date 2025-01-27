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
    public class EnemyType {
        public string enemyName;
        public int damage;
        public float speed;
        public float cooldown;
        public float maxViewDistance;
        public float rotationSpeed;
    }

    public List<EnemyType> Enemy = new List<EnemyType>();
    [SerializeField] private int currentEnemyIndex;

    private PlayerControls _PlayerControls;
    private HealthSystem _HealthSystem;
    private bool playerHit = false;

    [SerializeField] GameObject player;
    NavMeshAgent agent;

    private float lastSwitchTime;

    // Start is called before the first frame update
    void Start() {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        _PlayerControls = FindObjectOfType<PlayerControls>();
    }

    // Update is called once per frame
    void Update() {
        EnemyType enemyType = Enemy[currentEnemyIndex];
        agent.speed = enemyType.speed;

        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        
        if (distanceToPlayer <= enemyType.maxViewDistance && !_PlayerControls.inMenu) {
            agent.SetDestination(player.transform.position);
        } else {
            agent.ResetPath();
        }

        if (playerHit && !_PlayerControls.inMenu) {
            if (Time.time < lastSwitchTime + enemyType.cooldown) return;
            lastSwitchTime = Time.time;
            _HealthSystem.TakeDamage(enemyType.damage);
        }
    }

    void OnTriggerEnter2D(Collider2D collision) {
        _HealthSystem = collision.gameObject.GetComponent<HealthSystem>();

        if (_HealthSystem != null && collision.gameObject.CompareTag("Player")) {
            playerHit = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            playerHit = false;
        }
    }
}
