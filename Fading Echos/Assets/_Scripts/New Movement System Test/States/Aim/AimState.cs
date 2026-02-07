using UnityEngine;

public class AimState : TAimBaseManager
{
    public override void EnterState(TAimManager aim)
    {
        aim.animator.SetBool("Aiming", true);
        aim.currentFov = aim.adsFov;
    }

    public override void UpdateState(TAimManager aim)
    {
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            aim.SwitchState(aim.Hip);
        }
    }
}
