using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class HighScores : MonoBehaviour
{

    private string gameDataFileName = "save.json";
    private Player player;

    private void Awake()
    {
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
            // Read the json from the file into a string
            string dataAsJson = File.ReadAllText(path);
            // Pass the json to JsonUtility, and tell it to create a GameData object from it
            Player playerLoaded = JsonUtility.FromJson<Player>(dataAsJson);
            // Retrieve spefic loadedData
            Debug.Log("LOADED: " + playerLoaded.Name + " score " + playerLoaded.Score);

            //allRoundData = loadedData.allRoundData;
        }
        else
        {
            Debug.LogError("Cannot load game data!");
        }
    }

    public void SavePlayerData()
    {

        Debug.Log("TEST: " + player.Name + " score " + player.Score);

        string path = Application.dataPath + "/SaveData/save.json";
        string dataAsJson = JsonUtility.ToJson(player);

        //string filePath = Application.dataPath + path;
        File.WriteAllText(path, dataAsJson);
    }


}
