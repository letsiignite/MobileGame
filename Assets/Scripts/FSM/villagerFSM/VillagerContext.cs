using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UI;
using Game;

public class VillagerContext : MonoBehaviour
{
    private IVillagerState _currentState;
    public List<Transform> points = new List<Transform>();
    public NavMeshAgent agent;
    public bool isNotInfected = true;
    public bool isPaused = false;
    public int currentTargetIndex = 0;
    public float pathDeviation = 10.0f;
    private GameManager gameManager;
    public Animator Animator { get; private set; } // Reference to the Animator

    public InfectedState infectedState = new();
    public RunningState runningState = new();
    public PausedState pausedState = new();

    

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
        //Debug.Log(_currentState);
        _currentState.UpdateState(this);
    }
    public void SetState(IVillagerState newState)
    {
        _currentState = newState;
        _currentState.HandleState(this);
    }
    public void InfectVillager()
    {
        //Debug.Log("Stopping villager movement....");
        // Stop the villager from moving
        if (isNotInfected == true)
        {
            isNotInfected = false;
        }
    }

    public void OnPause()
    {
        //Debug.Log(" Villagers On Pause");
        isPaused = true;
    }

    public void Init(GameManager manager)
    {
        gameManager = manager;
        gameManager.AddPauseListners(OnPause);
    }
}
