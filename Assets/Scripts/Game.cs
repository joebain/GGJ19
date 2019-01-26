using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class Game : MonoBehaviour
{
    public static Game Instance = null;
    public Player player;
    private float prevSec;

    private void Awake()
    {
        LoadTestData();
    }

    // Start is called before the first frame update
    void Start()
    {

        //Singleton patter, allowing access to game and its proeprties anywhere
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

    }

    void Update()
    {
        if (Mathf.Floor(Time.time) > prevSec)
        {
            prevSec = Mathf.Floor(Time.time);
            player.Score++;
        }
    }

    public Player ReturnPlayerObj()
    {
        return player;
    }

    public List<Player> LoadTestData()
    {
        Player saveData = new Player("Jane", 123);
        Player saveData_2 = new Player("Maria", 1453);
        Player saveData_3 = new Player("Jo", 524);

        player = new Player("Isfandyar", 2343);

        List<Player> saveListData = new List<Player>();

        saveListData.Add(saveData);
        saveListData.Add(saveData_2);
        saveListData.Add(saveData_3);
        saveListData.Add(player);

        return saveListData;
    }

    public void LoadAndSavePlayerData()
    {
        string path = Application.dataPath + "/SaveData/save.json";

        if (File.Exists(path))
        {
            string dataAsJson = File.ReadAllText(path);

            //Load as Array THEN
            Player[] _tempLoadListData = JsonHelper.FromJson<Player>(dataAsJson);
            //Convert to a List
            List<Player> loadListData = _tempLoadListData.OfType<Player>().ToList();
            SavePlayerData(loadListData);
        }
        else
        {
            Debug.LogError("Cannot load game data!");
        }
    }

    public void SavePlayerData(List<Player> saveListData)
    {
        //calling Singleton GAME payer object
        saveListData.Add(Game.Instance.player);

        //Must be saved as JSON list
        string jsonToSave = JsonHelper.ToJson(saveListData.ToArray());
        string path = Application.dataPath + "/SaveData/save.json";
        //string dataAsJson = JsonUtility.ToJson(player);

        //string filePath = Application.dataPath + path;
        File.WriteAllText(path, jsonToSave);
    }

}
