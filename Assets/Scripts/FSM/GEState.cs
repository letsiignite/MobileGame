using UnityEngine;

public abstract class GEState
{
    public string name;
    public abstract void EnterState(GEntityAI geAI);
    public abstract void UpdateState(GEntityAI geAI);
}
