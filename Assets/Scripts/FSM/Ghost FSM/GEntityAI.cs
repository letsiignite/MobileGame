using UnityEngine;
using UnityEngine.AI;
using Game;
using GhostFSM;

public class GEntityAI : MonoBehaviour
{
    public GameObject playerRef;
    public float chaseTime = 2f;
    public float alertTime = 4f;

    [HideInInspector] public GameManager gameManager;
    [HideInInspector] public Vector3 startPosition;
    [HideInInspector] public NavMeshAgent agent;
    [HideInInspector] public LineOfSight los;
    [HideInInspector] public bool playerIsNearby;
    private GEState currentState;
    private bool isPaused = false;

    public WanderState wanderState = new();
    public AlertState alertState = new();
    public ChaseState chaseState = new();
    public InfectState infectState = new();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPosition = transform.position;
        agent = GetComponent<NavMeshAgent>();
        los = GetComponent<LineOfSight>();
        currentState = wanderState;
        currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(currentState.name);
        currentState.UpdateState(this);
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
    }
}
