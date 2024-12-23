using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class GEntityAI : MonoBehaviour
{
    public GameObject playerRef;

    [HideInInspector] public Vector3 startPosition;
    [HideInInspector] public NavMeshAgent agent;
    [HideInInspector] public LineOfSight los;
    private GEState currentState;

    public WanderState wanderState = new();

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
        Debug.Log(currentState.name);
        currentState.UpdateState(this);
    }

    public void SwitchState(GEState nextState)
    {
        currentState = nextState;
        currentState.EnterState(this);
    }
}
