using Game;
using UnityEngine;

public class SaveLoadData : MonoBehaviour
{
    public static SaveLoadData saveDatainstance;
    public int sanityMeter = 1;
    public float timeTaken = 1.0f;
    public int levelIndex = 0;

    private void Awake()
    {
        saveDatainstance = this;
        PlayerData loadedData = SaveSystem.LoadPlayer();
        if (loadedData == null)
        {
            SavePlayer();
        }
        else
        {
            LoadPlayer();
        }
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

    /// <summary>
    /// to be used when creating new player data
    /// </summary>
    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    /// <summary>
    /// to be used for respawn and loading player's data
    /// </summary>
    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        sanityMeter = data.sanityMeter;
        timeTaken = data.timeTaken;
        levelIndex = data.levelIndex;

        transform.position = new Vector3(data.rspnX, data.rspnY, data.rspnZ);
        foreach(var levelObj in GameManager._instance.GetLevelProgObj().levelObjects)
        {
            levelObj.gameObject.SetActive(false);
        }
        GameManager._instance.GetLevelProgObj().levelObjects[levelIndex-1].LevelCompleted();
        GameManager._instance.GetLevelProgObj().levelObjects[levelIndex].gameObject.SetActive(true);
    }
}
