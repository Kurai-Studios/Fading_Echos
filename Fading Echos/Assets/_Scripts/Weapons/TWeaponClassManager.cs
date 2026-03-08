using UnityEngine;
using UnityEngine.Animations.Rigging;

public class TWeaponClassManager : MonoBehaviour
{
    [SerializeField] TwoBoneIKConstraint leftHandIK;
    public Transform recoilFollowPos;
    TActionStateManager actions;

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
}
