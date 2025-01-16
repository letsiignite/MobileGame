namespace GhostFSM
{
    public abstract class GEState
    {
        public string name;

        /// <summary>
        /// This method is called when State Changes.
        /// </summary>
        /// <param name="geAI"></param>
        public abstract void EnterState(GEntityAI geAI);

        /// <summary>
        /// This method will be called at each frame for that state. Just like Update() method.
        /// </summary>
        /// <param name="geAI"></param>
        public abstract void UpdateState(GEntityAI geAI);
    }
}