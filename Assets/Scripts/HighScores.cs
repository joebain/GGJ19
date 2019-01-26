using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HighScores : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //PlayerPrefs --  can be used to save the player score
    public void GoBack()
    {
        Debug.Log("Go backing to MENU");
        SceneManager.LoadScene("Menu");

    }
}
