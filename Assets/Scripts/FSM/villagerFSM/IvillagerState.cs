using UnityEngine;

public interface IVillagerState
{
    void HandleState(VillagerContext context);
    void UpdateState(VillagerContext context);
    
}
