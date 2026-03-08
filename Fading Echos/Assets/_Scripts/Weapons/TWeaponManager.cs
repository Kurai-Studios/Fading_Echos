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
    public float rifleDamage = 20;
    TAimManager aim;

    TWeaponAmmo ammo;
    TActionStateManager actions;
    TWeaponRecoil recoil;

    void Start()
    {
        aim = GetComponentInParent<TAimManager>();
        ammo = GetComponent<TWeaponAmmo>();
        recoil = GetComponent<TWeaponRecoil>();
        actions = GetComponentInParent<TActionStateManager>();
        fireRateTimer = fireRate;
    }

    void Update()
    {
        if (ShouldFire())
        {
            Fire();
        }
        //Debug.Log(ammo.currentAmmo);
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
        recoil.TriggerRecoil();
        ammo.currentAmmo--;

        for(int i = 0; i < bulletPerShot; i++)
        {
            GameObject currentBullet = Instantiate(bullet, barrelPos.position, barrelPos.rotation);
            TBullet bulletScript = currentBullet.GetComponent<TBullet>();
            bulletScript.weaponM = this;
            Rigidbody rb = currentBullet.GetComponent<Rigidbody>();
            rb.AddForce(barrelPos.forward * bulletVelocity, ForceMode.Impulse);
        }
    }
}
