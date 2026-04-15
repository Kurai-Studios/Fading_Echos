using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    Animator anim;
    NavMeshAgent Agent;

    [SerializeField] public float health;
    [SerializeField] float delayTimer = 5f;

    public Transform Player;
    public float detectionRange = 10f;
    public float attackDistance = 3f;
    public float attackInterval = 2f;

    public bool isDead = false;
    bool isAttacking = false;

    private void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        if (Player == null)
        {
            Player = GameObject.FindGameObjectWithTag("Player").transform;
        }

    }

    void Update()
    {

        if (isDead)
        {
            return;
        }

        float Distance = Vector3.Distance(transform.position, Player.position);

        if (Distance <= detectionRange)
        {
            Agent.SetDestination(Player.position);
            anim.SetBool("isWalking", true);

            if (Distance <= attackDistance && !isAttacking)
            {
                StartCoroutine(PlayAttackingAnimation());
            }
        }
        else
        {
            Agent.ResetPath();
            anim.SetBool("isWalking", false);
        }
    }

    public void TakeDamage(float damage)
    {
        if (health > 0)
        {
            isDead = false;
            health -= damage;

            if (health <= 0)
            {
                EnemyDeath();
                isDead = true;
            }

            Debug.Log("Hit");
        }
    }

    public void DamagePlayer()
    {
        Player.GetComponent<THealthManager>().TakeDamage(5f);
    }

    void EnemyDeath()
    {
        isDead = true;
        Agent.isStopped = true;
        anim.SetBool("isWalking", false);
        anim.SetTrigger("dead");
        Debug.Log("Enemy Dead!");

        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            TWeaponAmmo weaponAmmo = player.GetComponentInChildren<TWeaponAmmo>();

            if (weaponAmmo != null)
            {
                weaponAmmo.extraAmmo += 30;
                Debug.Log("Added 30 ammo from enemy death!");
            }
        }

        StartCoroutine(DespawnEnemy());
    }

    IEnumerator PlayAttackingAnimation()
    {
        isAttacking = true;
        Agent.isStopped = true;
        anim.SetTrigger("Attack");
        DamagePlayer();

        yield return new WaitForSeconds(attackInterval);

        Agent.isStopped = false;
        isAttacking = false;
    }

    IEnumerator DespawnEnemy()
    {

        Debug.Log($"Coroutine started - waiting {delayTimer} seconds");
        yield return new WaitForSeconds(delayTimer);
        
        if (gameObject != null)
        {
            Destroy(gameObject);
        }
    }
}
