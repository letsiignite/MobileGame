using UnityEngine;
using UnityEngine.AI;
using UI;

public class GEntityAI : MonoBehaviour
{
    public GameObject playerRef;
    public float chaseTime = 2f;
    public float alertTime = 4f;

    [HideInInspector] public Vector3 startPosition;
    [HideInInspector] public NavMeshAgent agent;
    [HideInInspector] public LineOfSight los;
    [HideInInspector] public bool playerIsNearby;
    private GEState currentState;

    public WanderState wanderState = new();
    public AlertState alertState = new();
    public ChaseState chaseState = new();
    public InfectState infectState = new();

    public InGameMenu pause;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pause = GameObject.Find("GameplayUI").GetComponent<InGameMenu>();
       startPosition = transform.position;
        agent = GetComponent<NavMeshAgent>();
        los = GetComponent<LineOfSight>();
        currentState = wanderState;
        currentState.EnterState(this);
        pause.AddPauseListners(OnPause);
       
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(currentState.name);
        currentState.UpdateState(this);
    }

    private void OnPause()
    {
        //SwitchState(freez)
    }

    public void Init(InGameMenu inGameMenu)
    { 
        pause = inGameMenu;
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
}
