using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class DataManager 
{

    public static void SavePlayer(Player player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/Piñya";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerProfile data = new PlayerProfile(player);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerProfile LoadPlayer()
    {
        string path = Application.persistentDataPath + "/Piñya";
        if(File.Exists(path))
        {
            BinaryFormatter formatter= new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

           
            PlayerProfile data = formatter.Deserialize(stream) as PlayerProfile;
            stream.Close();

            return data;
        }
        else
        {
            Debug.Log("Save not found: " + path);
            return null;
        }
    }


    //public static bool Save(string name, object data)
    //{
    //    BinaryFormatter formatter = GetBinaryFormatter();
    //    if (!Directory.Exists(Application.persistentDataPath + "/saves"))
    //    {
    //        Directory.CreateDirectory(Application.persistentDataPath + " /saves");
    //    }

    //    string path = Application.persistentDataPath + "/saves/" + name + " .save";

    //    FileStream file = File.Create(path);
    //    formatter.Serialize(file, data);
    //    file.Close();
    //    return true;
    //}

    //public static object Load(string path)
    //{
    //    if(!File.Exists(path))
    //    {
    //        return null; 
    //    }

    //    BinaryFormatter formatter = GetBinaryFormatter();

    //    FileStream file = File.Open(path, FileMode.Open);

    //    try
    //    {
    //        object save = formatter.Deserialize(file);
    //        file.Close();
    //        return save;
    //    }
    //    catch
    //    {
    //        Debug.LogErrorFormat("Fail", path);
    //        file.Close();
    //        return null;
    //    }
    //}

    //public static BinaryFormatter GetBinaryFormatter()
    //{
    //    BinaryFormatter formatter = new BinaryFormatter();
    //    return formatter;
    //}
}
