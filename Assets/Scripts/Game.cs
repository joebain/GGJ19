using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game Instance = null;
    private Player player;

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

        CreatePlayer();
    }

    public void CreatePlayer()
    {
        //creating player for testting purposes
        player = new Player("Isfandyar", 2343);
    }

    public Player ReturnPlayerObj()
    {
        return player;
    }
}
