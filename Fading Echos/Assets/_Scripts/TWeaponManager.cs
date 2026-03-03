using UnityEngine;

public class TWeaponManager : MonoBehaviour
{
    [SerializeField] float fireRate;
    [SerializeField] bool semiAuto;
    float fireRateTimer;

    void Start()
    {
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

        Debug.Log("Fire");
    }
}
