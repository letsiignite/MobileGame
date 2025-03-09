using System.Collections.Generic;
using UnityEngine;
using Game;

namespace LevelProgression
{
    public class LevelProgression : MonoBehaviour
    {
        public List<LevelProgressionObject> levelObjects;
        public static LevelProgression levelProgressionInstance;

        public void SavePlayerData()
        {
            GameManager._instance.GetPlayerReference().GetComponent<SaveLoadData>().levelIndex++;
            SaveLoadData currData = GameManager._instance.GetPlayerReference().GetComponent<SaveLoadData>();
            SaveSystem.SavePlayer(currData);
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}