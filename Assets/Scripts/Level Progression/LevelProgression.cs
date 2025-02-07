using System.Collections.Generic;
using UnityEngine;

namespace LevelProgression
{
    public class LevelProgression : MonoBehaviour
    {
        public List<LevelProgressionObject> levelObjects;
        public static LevelProgression levelProgressionInstance;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            levelProgressionInstance = this;

            if (SaveLoadData.saveDatainstance != null)
            {
                PlayerData playerData = SaveSystem.LoadPlayer();
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}