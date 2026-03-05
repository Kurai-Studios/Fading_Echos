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

    TWeaponAmmo ammo;
    TActionStateManager actions;

    void Start()
    {
        aim = GetComponentInParent<TAimManager>();
        ammo = GetComponent<TWeaponAmmo>();
        actions = GetComponentInParent<TActionStateManager>();
        fireRateTimer = fireRate;
    }

    void Update()
    {
        if (ShouldFire())
        {
            Fire();
        }
        Debug.Log(ammo.currentAmmo);
    }

    bool ShouldFire()
    {
        fireRateTimer += Time.deltaTime;

        if (fireRateTimer < fireRate || ammo.currentAmmo == 0 || actions.currentState == actions.Reload)
        {
            return false;
        }
        if (semiAuto && Input.GetKeyDown(KeyCode.Mouse0) || !semiAuto && Input.GetKey(KeyCode.Mouse0))
        {
            return true;
        }

        return false;
    }

    void Fire()
    {
        fireRateTimer = 0;

        barrelPos.LookAt(aim.aimPos);
        ammo.currentAmmo--;

        for(int i = 0; i < bulletPerShot; i++)
        {
            GameObject currentBullet = Instantiate(bullet, barrelPos.position, barrelPos.rotation);
            Rigidbody rb = currentBullet.GetComponent<Rigidbody>();
            rb.AddForce(barrelPos.forward * bulletVelocity, ForceMode.Impulse);
        }
    }
}
