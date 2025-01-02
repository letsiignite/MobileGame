using UnityEngine;

public class IdleState : IVillagerState
{
    public void HandleState(VillagerContext context)
    {
        Debug.Log("Character is now Idle.");
        context.Animator.SetTrigger("Idle");
    }

    public void UpdateState(VillagerContext context)
    {
        Debug.Log("Character is idle...");
        context.SetState(new RunningState());
    }
}
