using UnityEngine;

public class TBullet : MonoBehaviour
{
    [SerializeField] float delayDestroy;
    [HideInInspector] public TWeaponManager weaponM;

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
        }

        Destroy(this.gameObject);
    }
}
