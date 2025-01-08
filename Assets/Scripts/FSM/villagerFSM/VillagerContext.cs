using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UI;

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

    public InGameMenu pause;

    void Start()
    {
        pause = GameObject.Find("GameplayUI").GetComponent<InGameMenu>();
        Animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        // Initialize with the Running state
        SetState(runningState);
        pause.AddPauseListners(OnPause);
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

    public void OnPause()
    {
        Debug.Log(" Villagers On Pause");
        SetState(idleState);
    }
}
