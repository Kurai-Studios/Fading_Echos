using UnityEngine;

public class RunState : MovementState
{
    public override void EnterState(TMovementManager TMovementStateManager)
    {
        TMovementStateManager.TAnimator.SetBool("Running", true);
        TMovementStateManager.TAnimator.SetBool("Aiming", true);
    }

    public override void UpdateState(TMovementManager TMovementStateManager)
    {
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            ExitState(TMovementStateManager, TMovementStateManager.TWalk);
        }
        else if (TMovementStateManager.dir.magnitude < 0.1f)
        {
            ExitState(TMovementStateManager, TMovementStateManager.TIdle);
        }

        if (TMovementStateManager.vInput < 0)
        {
            TMovementStateManager.currentMoveSpeed = TMovementStateManager.runBackSpeed;
        }
        else
        {
            TMovementStateManager.currentMoveSpeed = TMovementStateManager.runSpeed;
        }
    }

    void ExitState(TMovementManager TMovementStateManager, MovementState TState)
    {
        TMovementStateManager.TAnimator.SetBool("Running", false);
        TMovementStateManager.TAnimator.SetBool("Aiming", false);
        TMovementStateManager.SwitchState(TState);
    }
}
