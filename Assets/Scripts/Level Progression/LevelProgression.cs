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
            GameManager._instance.GetPlayerReference().GetComponent<SaveLoadData>().enabled = true;
            GameManager._instance.GetPlayerReference().GetComponent<SaveLoadData>().levelIndex++;
            SaveLoadData currData = GameManager._instance.GetPlayerReference().GetComponent<SaveLoadData>();
            SaveSystem.SavePlayer(currData);
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            GetComponent<SaveLoadData>().enabled = true;
            PlayerData loadedData = SaveSystem.LoadPlayer();
            if (loadedData == null)
            {
                GetComponent<SaveLoadData>().SavePlayer();
                GetComponent<SaveLoadData>().LoadPlayer();
            }
            else
            {
                GetComponent<SaveLoadData>().LoadPlayer();
            }
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}