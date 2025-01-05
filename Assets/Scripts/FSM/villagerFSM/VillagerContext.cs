using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class VillagerContext : MonoBehaviour
{
    private IVillagerState _currentState;
    public List<Transform> points = new List<Transform>();
    public NavMeshAgent agent;
    public bool isMoving = true;
    public int currentTargetIndex = 0;
    public float pathDeviation = 10.0f;
    public Animator Animator { get; private set; } // Reference to the Animator

    public IdleState idleState = new();
    public RunningState runningState = new();

    void Start()
    {
        Animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        // Initialize with the Running state
        SetState(runningState);
    }
    public void SetPoints(List<Transform> spawnPoints)
    {
        points = spawnPoints;
    }
    void Update()
    {
        //Debug.Log(currentState.name);
        _currentState.UpdateState(this);
    }
    public void SetState(IVillagerState newState)
    {
        _currentState = newState;
        _currentState.HandleState(this);
    }
    public void StopMovement()
    {
        Debug.Log("Stopping villager movement....");
        // Stop the villager from moving
        if (isMoving == true)
        {
            isMoving = false;
        }
    }
}
