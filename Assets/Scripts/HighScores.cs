using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Linq;

public class HighScores : MonoBehaviour
{
    private string gameDataFileName = "save.json";
    private Player player;

    private void Awake()
    {
        //testings purposes
        player = new Player("Isfand", 3242);
       
    }


    public void GoBack()
    {
        Debug.Log("Go backing to MENU");
        SceneManager.LoadScene("Menu");

        //Saving the player score
        Debug.Log("TEST: " + player.Name + " score " + player.Score);
        SavePlayerData();
    }

    public void LoadPlayerSaveData()
    {
        string path = Application.dataPath + "/SaveData/save.json";

        if (File.Exists(path))
        {
            string dataAsJson = File.ReadAllText(path);

            //Load as Array THEN
            Player[] _tempLoadListData = JsonHelper.FromJson<Player>(dataAsJson);
            //Convert to a List
            List<Player> loadListData = _tempLoadListData.OfType<Player>().ToList();
            for (int i = 0; i < loadListData.Count; i++)
            {
                Debug.Log("Name: " + loadListData[i].Name);
                Debug.Log("Scores: " + loadListData[i].Score);
            }
        }
        else
        {
            Debug.LogError("Cannot load game data!");
        }
    }

    public void SavePlayerData()
    {
        //Save as a list

        List<Player> saveListData = new List<Player>();
        Player saveData = new Player("Jane", 123);

        //calling Singleton GAME payer object
        Player playerFromSingleton = Game.Instance.ReturnPlayerObj();

        saveListData.Add(player);
        saveListData.Add(saveData);
        saveListData.Add(playerFromSingleton);


        Debug.Log("TEST (1): " + player.Name + " score " + player.Score);
        Debug.Log("TEST (2): " + saveData.Name + " score " + saveData.Score);
        Debug.Log("TEST (3): " + playerFromSingleton.Name + " score " + playerFromSingleton.Score);

        //Must be saved as JSON list
        string jsonToSave = JsonHelper.ToJson(saveListData.ToArray());
        string path = Application.dataPath + "/SaveData/save.json";
        //string dataAsJson = JsonUtility.ToJson(player);

        //string filePath = Application.dataPath + path;
        File.WriteAllText(path, jsonToSave);
    }


}
