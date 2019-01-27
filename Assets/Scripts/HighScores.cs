using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Linq;
using UnityEngine.UI;

public class HighScores : MonoBehaviour
{
    [SerializeField]
    private Text[] scoresTexts;
    [SerializeField]
    private Text[] playerNamesTexts;


    private void Awake()
    {

        LoadPlayerSaveData();
    }

    public void GoBack()
    {
        Debug.Log("Go backing to MENU");
        SceneManager.LoadScene("Menu");
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
            loadListData.Sort((e1, e2) => e2.Score.CompareTo(e1.Score));

            for (int i = 0; i < loadListData.Count; i++)
            {
                //only 3 scores allowed, anymore loop out of bounds
                if (i > 2)
                {
                    break;
                }
                else
                {

                    Debug.Log("Name: " + loadListData[i].Name);
                    Debug.Log("Scores: " + loadListData[i].Score);
                    scoresTexts[i].text = loadListData[i].Score.ToString();
                    playerNamesTexts[i].text = loadListData[i].Name;
                }
            }
        }
        else
        {
            Debug.LogError("Cannot load game data!");
        }
    }


    public void SavePlayerData()
    {
        //ALt save data list
        //List<Player> saveListData = Game.Instance.LoadTestData();

        //Save as a list
        List<Player> saveListData = new List<Player>();

        //calling Singleton GAME payer object
        Player playerFromSingleton = Game.Instance.ReturnPlayerObj();
        saveListData.Add(playerFromSingleton);
       
        Debug.Log("TEST (1): " + playerFromSingleton.Name + " score " + playerFromSingleton.Score);

        //Must be saved as JSON list
        string jsonToSave = JsonHelper.ToJson(saveListData.ToArray());
        string path = Application.dataPath + "/SaveData/save.json";
        //string dataAsJson = JsonUtility.ToJson(player);

        //string filePath = Application.dataPath + path;
        File.WriteAllText(path, jsonToSave);

    }

    public void Restart()
    {
        Debug.Log("Go to game");
        SceneManager.LoadScene("ActualGame");
    }

}
