using TMPro;
using UnityEngine;

public class TWeaponAmmo : MonoBehaviour
{
    public int clipSize;
    public int extraAmmo;
    public int currentAmmo;

    [Header("UI")]
    public TextMeshProUGUI ammoText;

    void Start()
    {
        currentAmmo = clipSize;
        UpdateAmmoUI();
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
                currentAmmo = clipSize;
            }
            else
            {
                currentAmmo += extraAmmo;
                extraAmmo = 0;
            }
        }

        UpdateAmmoUI();
    }

    public void UpdateAmmoUI()
    {
        if (ammoText != null)
            ammoText.text = $"{currentAmmo} / {extraAmmo}";

        TWeaponManager weaponManager = GetComponent<TWeaponManager>();

        if (weaponManager != null && weaponManager.weaponUI != null)
            weaponManager.weaponUI.UpdateAmmoDisplay(currentAmmo, extraAmmo);
    }
}
