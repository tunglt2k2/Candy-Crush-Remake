using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


[Serializable]
public class SaveData{
    [Serializable] 
    public struct Attribute
    {
        public bool isActive;
        public bool isPassed;
        public int highScore;
        public int starts;
    }
    public Attribute[] attributes;
    
}

public class GameData : MonoBehaviour
{
    public static GameData gameData;
    public SaveData saveData;

    void Awake()
    {
        if (gameData == null)
        {
            DontDestroyOnLoad(this.gameObject);
            gameData = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        Load();
    }

    private void Start()
    {

    }

    public void Save()
    {
        //Create a binary formatted which can read binary file
        BinaryFormatter formatter = new BinaryFormatter();
        //Create a route from the program to the file
        FileStream file = File.Open(Application.persistentDataPath + "/player.dat", FileMode.Create);
        //Create a copy of save data
        SaveData data = new SaveData();
        data = saveData;
        //Actually save the data in the file
        formatter.Serialize(file, data);
        //Close the data stream
        file.Close();
    }

    public void Load()
    {
        //Check if the save game file exists
        if (File.Exists(Application.persistentDataPath + "/player.dat"))
        {
            //Create a Binary Formatter
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/player.dat", FileMode.Open);
            saveData = formatter.Deserialize(file) as SaveData;
            file.Close();
        }
        else
        {
            saveData = new SaveData();
            saveData.attributes = new SaveData.Attribute[100];
            saveData.attributes[0].isActive = true;
        }
    }

    private void OnApplicationQuit()
    {
        Save();
    }
    private void OnDisable()
    {
        Save();
    }

    private void OnApplicationPause()
    {
        Save();
    }
}
