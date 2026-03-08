using UnityEngine;

public class TBullet : MonoBehaviour
{
    [SerializeField] float delayDestroy;
    [HideInInspector] public TWeaponManager weaponM;
    [HideInInspector] public Vector3 dir;

    void Start()
    {
        Destroy(this.gameObject, delayDestroy);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponentInParent<EnemyHealth>())
        {
            EnemyHealth enemyHealth = collision.gameObject.GetComponentInParent<EnemyHealth>();
            enemyHealth.TakeDamage(weaponM.rifleDamage);

            if (enemyHealth.health <= 0)
            {
                Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
                rb.AddForce(dir * weaponM.enemyKickbackForce, ForceMode.Impulse);
            }
        }

        Destroy(this.gameObject);
    }
}
