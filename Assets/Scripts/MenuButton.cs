using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
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
        Debug.Log("Go to game");
        SceneManager.LoadScene("ActualGame");
    }

    public void ViewHighScores()
    {
        Debug.Log("Loading HighScores");
        SceneManager.LoadScene("HighScores");

    }

    public void Quit()
    {
        Application.Quit();
    }

}
