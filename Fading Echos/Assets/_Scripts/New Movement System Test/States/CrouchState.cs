using UnityEngine;

public class CrouchState : MovementState
{
    public override void EnterState(TMovementManager TMovementStateManager)
    {
        TMovementStateManager.TAnimator.SetBool("Crouching", true);
    }

    public override void UpdateState(TMovementManager TMovementStateManager)
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            ExitState(TMovementStateManager, TMovementStateManager.TRun);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            if (TMovementStateManager.dir.magnitude < 0.1f)
            {
                ExitState(TMovementStateManager, TMovementStateManager.TIdle);
            }
            else
            {
                ExitState(TMovementStateManager, TMovementStateManager.TWalk);
            }
        }

        if (TMovementStateManager.vInput < 0)
        {
            TMovementStateManager.currentMoveSpeed = TMovementStateManager.walkBackSpeed;
        }
        else
        {
            TMovementStateManager.currentMoveSpeed = TMovementStateManager.walkSpeed;
        }
    }

    void ExitState(TMovementManager TMovementStateManager, MovementState TState)
    {
        TMovementStateManager.TAnimator.SetBool("Crouching", false);
        TMovementStateManager.SwitchState(TState);
    }
}
