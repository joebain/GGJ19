using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Collections;



[Serializable]
public class Player
{
    public string Name;
    public int Score;
    [SerializeField]
    private int score
    {
        get
        {
            return Score;
        }
        set
        {
            Score = value;
        }
    }
    [SerializeField]
    private string name
    {
        get
        {
            return Name;
        }
        set
        {
            Name = value;
        }
    }

    public Player(string aName, int aScore)
    {
        Name = aName;
        Score = aScore;
    }

    public string LoadSaveFromJSON()
    {
        Debug.Log("Error downloading from web FAILED");
        string path = Application.dataPath + "/SaveData/save.json";
        string jsonString = File.ReadAllText(path);

        return jsonString;

    }


}