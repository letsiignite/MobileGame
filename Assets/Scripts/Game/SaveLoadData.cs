using UnityEngine;

public class SaveLoadData : MonoBehaviour
{
    public static SaveLoadData saveDatainstance;
    public int sanityMeter = 1;
    public float timeTaken = 1.0f;
    public string checkPointName = "LetsIgnite";

    private void Start()
    {
        saveDatainstance = this;
    }

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
