using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveSystem : MonoBehaviour
{
    public static void SaveLevel()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/levelnum.dat";
        FileStream stream = new FileStream(path, FileMode.Create);

        int data = PlayerData.level;

        formatter.Serialize(stream, data);
        stream.Close();
    }


    public static int LoadLevel()
    {
        string path = Application.persistentDataPath + "/levelnum.dat";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            int data = (int)formatter.Deserialize(stream);

            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return 1;
        }
    }
}
