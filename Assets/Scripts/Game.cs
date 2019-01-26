using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class Game : MonoBehaviour
{
    public static Game Instance = null;
    public Player player;

    private int prevSec;

    public int ScorePerSec;

    private void Awake()
    {
        LoadTestData();
        DontDestroyOnLoad(this.gameObject);
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

        player = new Player();

    }

    void Update()
    {
        int thisSec = Mathf.FloorToInt(Time.time);
        if (thisSec > prevSec)
        {
            prevSec = thisSec;
            player.Score += ScorePerSec;
        }
    }

    public Player ReturnPlayerObj()
    {
        return player;
    }

    public void LoadTestData()
    {
        Player saveData = new Player("Jane", 123);
        player = new Player("Isfandyar", 2343);
    }

}
