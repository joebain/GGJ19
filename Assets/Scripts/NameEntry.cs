using Rewired;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NameEntry : MonoBehaviour
{
    public InputField input;

    public Text Score;
    private Rewired.Player player;

    // Start is called before the first frame update
    void Start()
    {
        input.onEndEdit.AddListener(ListenToSubmit);

        Score.text = Game.Instance.player.Score.ToString();

        player = ReInput.players.GetPlayer(RewiredConsts.Player.ONE);
    }

    void Update()
    {
        if (player.GetButtonDown(RewiredConsts.Action.SWIM))
        {
            retry();
        }
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

    public void retry() {
        if (input.text != null && input.text.Length > 0)
        {
            Game.Instance.player.Name = input.text;
            Game.Instance.LoadAndSavePlayerData();
            Game.Instance.player = new Player();
        }
        SceneManager.LoadScene("ActualGame");
    }


}
