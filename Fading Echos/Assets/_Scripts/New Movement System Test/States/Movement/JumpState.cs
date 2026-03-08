using UnityEngine;

public class JumpState : MovementState
{
    public override void EnterState(TMovementManager TMovementStateManager)
    {
       if (TMovementStateManager.previousState == TMovementStateManager.TIdle)
       {
            TMovementStateManager.TAnimator.SetTrigger("Jump");
       }
       else if (TMovementStateManager.previousState == TMovementStateManager.TWalk ||
            TMovementStateManager.previousState == TMovementStateManager.TRun)
       {
            TMovementStateManager.TAnimator.SetTrigger("RunJump");
       }
    }

    public override void UpdateState(TMovementManager TMovementStateManager)
    {
        if (TMovementStateManager.jumped && TMovementStateManager.IsGrounded())
        {
            TMovementStateManager.jumped = false;

            if (TMovementStateManager.hzInput == 0 && TMovementStateManager.vInput == 0)
            {
                TMovementStateManager.SwitchState(TMovementStateManager.TIdle);
            }
            else if (Input.GetKey(KeyCode.LeftShift))
            {
                TMovementStateManager.SwitchState(TMovementStateManager.TRun);
            }
            else
            {
                TMovementStateManager.SwitchState(TMovementStateManager.TWalk);
            }
        }
    }
}
