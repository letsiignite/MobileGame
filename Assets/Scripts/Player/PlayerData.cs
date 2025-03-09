using UnityEngine;
using UnityEngine.UIElements;
using LevelProgression;

[System.Serializable]
public class PlayerData
{
    public int sanityMeter;
    public float timeTaken;
    public int levelIndex;

    public float rspnX;
    public float rspnY;
    public float rspnZ;

    public float ghstX;
    public float ghstY;
    public float ghstZ;

    public PlayerData(SaveLoadData player) //Ex for save system,SaveLoadData script have a save method and load method
    {
        sanityMeter = player.sanityMeter;
        timeTaken = player.timeTaken;
        levelIndex = player.levelIndex;

        rspnX = player.gameObject.GetComponent<LevelProgression.LevelProgression>().levelObjects[levelIndex].RespawnPoint.transform.position.x;
        rspnY = player.gameObject.GetComponent<LevelProgression.LevelProgression>().levelObjects[levelIndex].RespawnPoint.transform.position.y;
        rspnZ = player.gameObject.GetComponent<LevelProgression.LevelProgression>().levelObjects[levelIndex].RespawnPoint.transform.position.z;

        ghstX = player.gameObject.GetComponent<LevelProgression.LevelProgression>().levelObjects[levelIndex].GhostWarpPosition.transform.position.x;
        ghstY = player.gameObject.GetComponent<LevelProgression.LevelProgression>().levelObjects[levelIndex].GhostWarpPosition.transform.position.y;
        ghstZ = player.gameObject.GetComponent<LevelProgression.LevelProgression>().levelObjects[levelIndex].GhostWarpPosition.transform.position.z;
    }
}
