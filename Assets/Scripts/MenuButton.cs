﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Spacebar Pressed in Start scence");
            StartGame();

        }

    }

    public void StartGame()
    {
        Debug.Log("Go to Controls has started");
    }

}
