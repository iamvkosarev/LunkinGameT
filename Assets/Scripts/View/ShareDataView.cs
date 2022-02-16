using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class ShareDataView : MonoBehaviour
{
    public Save CreateSave()
    {
        Save save = new Save();

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save");
        bf.Serialize(file, save);
        file.Close();

        Debug.Log("Create new Saved");
        return save;
    }
    public Save LoadSave()
    {
        if (File.Exists(Application.persistentDataPath + "/gamesave.save"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
            Save save = (Save)bf.Deserialize(file);
            file.Close();
            //Debug.Log("Save Loaded");
            return save;

        }
        else
        {
            return null;
        }
    }


    protected void Save(Save save)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save");
        bf.Serialize(file, save);
        file.Close();
    }

}
