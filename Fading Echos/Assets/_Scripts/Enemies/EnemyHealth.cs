using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] public float health;
    RagdollManager ragdollManager;
    [SerializeField] float delayTimer = 5f;

    private void Start()
    {
        ragdollManager = GetComponent<RagdollManager>();
    }
    public void TakeDamage(float damage)
    {
        if (health > 0)
        {
            health -= damage;

            if (health <= 0)
            {
                EnemyDeath();
            }

            Debug.Log("Hit");
        }
    }
        

    void EnemyDeath()
    {
        ragdollManager.TriggerRagdoll();
        Debug.Log("Enemy Dead!");
        StartCoroutine(DespawnEnemy());
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
