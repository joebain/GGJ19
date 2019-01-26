using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class Game : MonoBehaviour
{
    public static Game Instance = null;
    public Player player;

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

}
