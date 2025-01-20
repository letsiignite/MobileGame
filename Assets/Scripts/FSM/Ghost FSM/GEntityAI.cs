using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;
using Game;
using Puzzle;
using GhostFSM;

public class GEntityAI : MonoBehaviour
{
    private GEState currentState;
    private bool isPaused = false;
    private bool isDistracted = false;
    private float timer;
    private GameObject distractObject;

    [Header("Dependency")]
    public GameObject playerRef;
    public float stayDistractTime = 500f;
    public List<GameObject> distractObjectList;

    [Header("State Data")]
    public float chaseTime = 2f;
    public float alertTime = 4f;
    public List<Transform> waypoints;

    [HideInInspector] public GameManager gameManager;
    [HideInInspector] public Vector3 startPosition;
    [HideInInspector] public NavMeshAgent agent;
    [HideInInspector] public LineOfSight los;
    [HideInInspector] public bool playerIsNearby;

    public WanderState wanderState = new();
    public AlertState alertState = new();
    public ChaseState chaseState = new();
    public InfectState infectState = new();
    
    public void SetDistractedObject(GameObject distract)
    {
        distractObject = distract;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPosition = transform.position;
        agent = GetComponent<NavMeshAgent>();
        los = GetComponent<LineOfSight>();
        currentState = wanderState;
        currentState.EnterState(this);
        
        foreach(var distract in distractObjectList)
        {
            distract.GetComponent<PuzzleObject>().AddPuzzleListener(OnDistracted);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPaused && !isDistracted)
        {
            //Debug.Log("Pass : " + currentState.name);
            currentState.UpdateState(this);
        }
        else
        {
            timer += Time.deltaTime;
        }
        if((isDistracted && los.visibleEnemy.Contains(playerRef)) || (timer >= stayDistractTime && Vector3.Distance(gameObject.transform.position, agent.destination) <= agent.stoppingDistance))
        {
            isDistracted = false;
            timer = 0f;
        }
    }

    public void SwitchState(GEState nextState)
    {
        currentState = nextState;
        currentState.EnterState(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == playerRef)
        {
            playerIsNearby = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == playerRef)
        {
            playerIsNearby = false;
        }
    }

    public void Init(GameManager manager)
    { 
        gameManager = manager;
        gameManager.AddPauseListners(OnPause);
    }

    public void OnPause()
    {
        //Debug.Log(" Villagers On Pause");

        isPaused = true;
        agent.isStopped = true;
        Debug.Log("Passed-GEAI");
        /* stop the speed of animations
        anim.speed = 0f;*/
    }

    public void OnDistracted()
    {
        isDistracted = true;
        agent.SetDestination(distractObject.transform.position);
    }
}
