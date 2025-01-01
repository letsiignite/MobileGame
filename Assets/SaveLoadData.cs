using UnityEngine;

public class SaveLoadData : MonoBehaviour
{
    // this is test script
    public int sanityMeter = 1;
    public float timeTaken = 1.0f;
    public string checkPointName = "LetsIgnite";

    #region UI Methods
    public void ChangeLevel(int amount)
    {
        sanityMeter += amount;
    }
    public void ChangeHealth(float amount)
    {
        timeTaken += amount;
    }
    #endregion

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }
    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        sanityMeter = data.sanityMeter;
        timeTaken = data.timeTaken;
        checkPointName = data.checkpointName;

    }
}
