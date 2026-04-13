using TMPro;
using UnityEngine;

public class TWeaponUI : MonoBehaviour
{
    [Header("Ammo Display")]
    public TextMeshProUGUI ammoText;
    public string ammoFormat = "{0} / {1}";

    void Start()
    {
        if (ammoText != null)
            ammoText.text = string.Format(ammoFormat, "0", "0");
    }

    public void UpdateAmmoDisplay(int currentAmmo, int extraAmmo)
    {
        if (ammoText != null)
            ammoText.text = string.Format(ammoFormat, currentAmmo.ToString(), extraAmmo.ToString());
    }
}
