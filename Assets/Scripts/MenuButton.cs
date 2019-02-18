using Rewired;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
    private Animator[] anims; //There are multiple animators attached to the Handle and Flushing
    private Rewired.Player player;

    private void Start()
    {
        anims = GetComponentsInChildren<Animator>();
        anims[0].SetBool("IsFlushing", false);
        anims[1].SetBool("IsHandleMoving", false);

        player = ReInput.players.GetPlayer(RewiredConsts.Player.ONE);

    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetButtonDown(RewiredConsts.Action.SWIM))
        {
            Debug.Log("Spacebar Pressed in Start scence");
            StartGame(); 
        }

    }

    public void StartGame()
    {
        StartCoroutine(DoStartGame());
    }

    public IEnumerator DoStartGame()
    {
        anims[0].SetBool("IsFlushing", true);
        anims[1].SetBool("IsHandleMoving", true);
        AkSoundEngine.PostEvent("StartGame", gameObject);

        ///coroutine to handle the animation of the fllush
        yield return new WaitForSeconds(2);

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
