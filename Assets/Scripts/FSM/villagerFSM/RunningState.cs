using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class RunningState : IVillagerState
{
    public void HandleState(VillagerContext context)
    {
        Debug.Log("Character is now Running.");
        context.Animator.SetTrigger("Run");
    }

    public void UpdateState(VillagerContext context)
    {
        Debug.Log("Character is running...");
        context.SetState(new IdleState());
    }
}
