using Unity.VisualScripting;
using UnityEngine;

namespace Puzzle
{
    public interface IPuzzle
    {
        public void OnInteraction();
        public void Activate();
    }
}