using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SavePlayer (DontDestroyOnLoad dontDestroyOnLoad)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/player.savedata";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(dontDestroyOnLoad);
        
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.savedata";
        
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            
            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found");
            return null;
        }
    }

    public static void DeletePlayer()
    {
        string path = Application.persistentDataPath + "/player.savedata";

        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }
}
