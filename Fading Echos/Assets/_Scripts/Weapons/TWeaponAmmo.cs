using UnityEngine;

public class TWeaponAmmo : MonoBehaviour
{
    public int clipSize;
    public int extraAmmo;
    public int currentAmmo;

    void Start()
    {
        currentAmmo = clipSize;
    }

    public void Reload()
    {
        if (extraAmmo >= clipSize)
        {
            int ammoToReload = clipSize - currentAmmo;
            extraAmmo -= ammoToReload;
            currentAmmo += ammoToReload;
        }
        else if (extraAmmo > 0)
        {
            if (extraAmmo + currentAmmo > clipSize)
            {
                int leftOverAmmon = extraAmmo + currentAmmo - clipSize;
                extraAmmo = leftOverAmmon;
                currentAmmo = leftOverAmmon;
            }
            else
            {
                currentAmmo += extraAmmo;
                extraAmmo = 0;
            }
        }
    }
}
