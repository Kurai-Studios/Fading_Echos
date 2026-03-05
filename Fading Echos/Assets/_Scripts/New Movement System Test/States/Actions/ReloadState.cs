using UnityEngine;

public class ReloadState : ActionBaseState
{
    public override void EnterState(TActionStateManager actions)
    {
        actions.rHandAim.weight = 0;
        actions.lHandIK.weight = 0;
        actions.anim.SetTrigger("Reload");
    }

    public override void UpdateState(TActionStateManager actions)
    {

    }
}
