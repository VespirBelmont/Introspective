using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveFunction
{
    public static void SaveTheGame(int runCount)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/kd_savedata";
        FileStream stream = new FileStream(path, FileMode.Create);

        SaveData data = new SaveData(runCount);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static SaveData LoadTheGame()
    {
        string path = Application.persistentDataPath + "/kd_savedata";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            SaveData data = formatter.Deserialize(stream) as SaveData;
            stream.Close();

            return data;
        }

        else
        {
            SaveTheGame(0);
            return null;
        }
    }

}
