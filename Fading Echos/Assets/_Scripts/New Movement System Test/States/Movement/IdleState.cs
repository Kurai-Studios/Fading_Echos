using UnityEngine;

public class IdleState : MovementState
{
    public override void EnterState(TMovementManager TMovementStateManager)
    {

    }

    public override void UpdateState(TMovementManager TMovementStateManager)
    {
        if (TMovementStateManager.dir.magnitude > 0.1f)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                TMovementStateManager.SwitchState(TMovementStateManager.TRun);
            }
            else
            {
                TMovementStateManager.SwitchState(TMovementStateManager.TWalk);
            }
        }


        if (Input.GetKeyDown(KeyCode.C))
        {
            TMovementStateManager.SwitchState(TMovementStateManager.TCrouch);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            TMovementStateManager.previousState = this;
            TMovementStateManager.SwitchState(TMovementStateManager.TJump);
        }
    }
}
