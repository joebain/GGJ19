using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NameEntry : MonoBehaviour
{

    public InputField input;

    // Start is called before the first frame update
    void Start()
    {
        input.onEndEdit.AddListener(ListenToSubmit);
    }


    public void ListenToSubmit(string playerName)
    {
        save();
    }

    public void save()
    {
        Debug.Log("TEST: " + input.text);
        Game.Instance.player.Name = input.text;
        Game.Instance.LoadAndSavePlayerData();
        Game.Instance.player = new Player();
        SceneManager.LoadScene("HighScores");
    }


}
