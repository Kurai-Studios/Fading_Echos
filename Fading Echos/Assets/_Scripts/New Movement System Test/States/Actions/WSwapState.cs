using UnityEngine;

public class WSwapState : ActionBaseState
{
    public override void EnterState(TActionStateManager actions)
    {
        actions.anim.SetTrigger("SwapWeapon");
        actions.lHandIK.weight = 0;
        actions.rHandAim.weight = 0;
    }

    public override void UpdateState(TActionStateManager actions)
    {
        
    }
}
