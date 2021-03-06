﻿using System;
using UnityEngine;
using System.IO;



[Serializable]
public class Player
{
    public string Name;
    public int Score;
    public int LevelNumber;
    public int ShrimpCount;
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

    public Player()
    {
        Name = "UNKNNOWN";
        Score = 00000;
        LevelNumber = 0;
        ShrimpCount = 0;
    }

    public string LoadSaveFromJSON()
    {
        Debug.Log("Error downloading from web FAILED");
        string path = Application.dataPath + "/SaveData/save.json";
        string jsonString = File.ReadAllText(path);

        return jsonString;

    }


}

// WRapper to help Player be saved as a LIST curently unsppported as Unity
public static class JsonHelper
{

    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = UnityEngine.JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }

    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return UnityEngine.JsonUtility.ToJson(wrapper);
    }

    [Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }
}