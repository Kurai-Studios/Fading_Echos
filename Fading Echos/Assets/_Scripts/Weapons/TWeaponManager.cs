using UnityEngine;

public class TWeaponManager : MonoBehaviour
{
    [Header("Fire Rate")]
    [SerializeField] float fireRate;
    [SerializeField] bool semiAuto;
    float fireRateTimer;

    [Header("Bullet Properties")]
    [SerializeField] GameObject bullet;
    [SerializeField] Transform barrelPos;
    [SerializeField] float bulletVelocity;
    [SerializeField] int bulletPerShot;
    TAimManager aim;

    void Start()
    {
        aim = GetComponentInParent<TAimManager>();
        fireRateTimer = fireRate;
    }

    void Update()
    {
        if (ShouldFire())
        {
            Fire();
        }
    }

    bool ShouldFire()
    {
        fireRateTimer += Time.deltaTime;

        if (fireRateTimer < fireRate)
        {
            return false;
        }
        if (semiAuto && Input.GetKeyDown(KeyCode.Mouse0))
        {
            return true;
        }
        if (!semiAuto && Input.GetKey(KeyCode.Mouse0))
        {
            return true;
        }

        return false;
    }

    void Fire()
    {
        fireRateTimer = 0;

        barrelPos.LookAt(aim.aimPos);

        for(int i = 0; i < bulletPerShot; i++)
        {
            GameObject currentBullet = Instantiate(bullet, barrelPos.position, barrelPos.rotation);
            Rigidbody rb = currentBullet.GetComponent<Rigidbody>();
            rb.AddForce(barrelPos.forward * bulletVelocity, ForceMode.Impulse);
        }
    }
}
