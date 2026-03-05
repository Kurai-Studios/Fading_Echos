using UnityEngine;
using UnityEngine.Animations.Rigging;

public class TActionStateManager : MonoBehaviour
{
    [HideInInspector]public ActionBaseState currentState;
    public ReloadState Reload = new ReloadState();
    public DefaultState Default = new DefaultState();

    public GameObject currentWeapon;
    [HideInInspector]public TWeaponAmmo ammo;

    [HideInInspector] public Animator anim;

    public MultiAimConstraint rHandAim;
    public TwoBoneIKConstraint lHandIK;

    void Start()
    {
        SwitchState(Default);
        ammo = currentWeapon.GetComponent<TWeaponAmmo>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(ActionBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

    public void WeaponReloaded()
    {
        ammo.Reload();
        SwitchState(Default);
    }
}
