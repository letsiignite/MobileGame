using UnityEngine;

public class VillagerContext : MonoBehaviour
{
    private IVillagerState _currentState;
    public Animator Animator { get; private set; } // Reference to the Animator

    void Start()
    {
        Animator = GetComponent<Animator>();

        // Initialize with the Running state
        SetState(new RunningState());
    }

    public void SetState(IVillagerState newState)
    {
        _currentState = newState;
        _currentState.HandleState(this);
    }
}
