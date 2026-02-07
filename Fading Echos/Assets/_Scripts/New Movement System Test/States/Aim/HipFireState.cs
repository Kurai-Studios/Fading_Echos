using UnityEngine;

public class HipFireState : TAimBaseManager
{
    public override void EnterState(TAimManager aim)
    {
        aim.animator.SetBool("Aiming", false);
    }

    public override void UpdateState(TAimManager aim)
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            aim.SwitchState(aim.Aim);
        }
    }
}
