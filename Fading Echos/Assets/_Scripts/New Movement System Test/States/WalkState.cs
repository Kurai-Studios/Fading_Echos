using UnityEngine;

public class WalkState : MovementState
{
    public override void EnterState(TMovementManager TMovementStateManager)
    {
        TMovementStateManager.TAnimator.SetBool("Walking", true);
    }

    public override void UpdateState(TMovementManager TMovementStateManager)
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            ExitState(TMovementStateManager, TMovementStateManager.TRun);
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            ExitState(TMovementStateManager, TMovementStateManager.TCrouch);
        }
        else if (TMovementStateManager.dir.magnitude < 0.1f)
        {
            ExitState(TMovementStateManager, TMovementStateManager.TIdle);
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
        TMovementStateManager.TAnimator.SetBool("Walking", false);
        TMovementStateManager.SwitchState(TState);
    }
}
