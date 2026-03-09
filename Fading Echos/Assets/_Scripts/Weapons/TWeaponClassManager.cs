using UnityEngine;
using UnityEngine.Animations.Rigging;

public class TWeaponClassManager : MonoBehaviour
{
    [SerializeField] TwoBoneIKConstraint leftHandIK;
    public Transform recoilFollowPos;
    TActionStateManager actions;

    public TWeaponManager[] weapons;
    int currentWeaponIndex;

    private void Awake()
    {
        currentWeaponIndex = 0;

        for (int i = 0; i < weapons.Length; i++)
        {
            if (i == 0)
            {
                weapons[i].gameObject.SetActive(true);
            }
            else
            {
                weapons[i].gameObject.SetActive(false);
            }
        }
    }

    public void SetCurrentWeapon(TWeaponManager weapon)
    {
        if (actions == null)
        {
            actions = GetComponent<TActionStateManager>();
        }

        leftHandIK.data.target = weapon.leftHandTarget;
        leftHandIK.data.hint = weapon.leftHandHint;

        actions.SetWeapon(weapon);
    }

    public void ChangeWeapon(float direction)
    {
        weapons[currentWeaponIndex].gameObject.SetActive(false);

        if (direction < 0)
        {
            if (currentWeaponIndex == 0)
            {
                currentWeaponIndex = weapons.Length - 1;
            }
            else
            {
                currentWeaponIndex--;
            }
        }
        else
        {
            if (currentWeaponIndex == weapons.Length - 1)
            {
                currentWeaponIndex = 0;
            }
            else
            {
                currentWeaponIndex++;
            }
        }

        weapons[currentWeaponIndex].gameObject.SetActive(true);
    }

    public void WeaponPutAway()
    {
        ChangeWeapon(actions.Default.scrollDirection);
    }

    public void WeaponPulledPut()
    {
        actions.SwitchState(actions.Default);
    }
}
