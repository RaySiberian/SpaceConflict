using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveSystem
{
    public static void SaveCurrentData(Data data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/data.save";
        FileStream stream = new FileStream(path, FileMode.Create);
        SavedData savedData = new SavedData(data);
        formatter.Serialize(stream,savedData);
        stream.Close();
    }

    public static SavedData LoadData()
    {
        string path = Application.persistentDataPath + "/data.save";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            SavedData savedData = formatter.Deserialize(stream) as SavedData;
            stream.Close();
            return savedData;
        }
        else
        {
            Debug.Log("Not fond file");
            return null;
        }
    }
}
