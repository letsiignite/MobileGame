using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem 
{
    public static void SavePlayer (SaveLoadData player) 
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.savedata";
        Debug.Log(path);
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(player);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPlayer() 
    {
        string path = Application.persistentDataPath + "/player.savedata";
        PlayerData data = null;
        if (File.Exists(path)) 
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream (path, FileMode.Open);
            Debug.Log(" stream = "+ stream.Length);
            if (stream.Length > 0)
            {
                data = formatter.Deserialize(stream) as PlayerData;
            }
            
            stream.Close();
            return data;
        }
        else 
        {
            Debug.LogError("Save File not found in" + path);
            return null;
        }
    }
}
