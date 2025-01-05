using UnityEngine;
using UnityEngine.UIElements;

[System.Serializable]
public class PlayerData
{
    public int sanityMeter;
    public float timeTaken;
    public string checkpointName;

    public PlayerData (SaveLoadData player) //Ex for save system,SaveLoadData script have a save method and load method
    {
        sanityMeter = player.sanityMeter;
        timeTaken = player.timeTaken;
        checkpointName = player.checkPointName;
    }
}
