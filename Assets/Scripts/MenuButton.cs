using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
    private Animator anim;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        anim.SetBool("IsFlushing", false);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Spacebar Pressed in Start scence");
            anim.SetBool("IsFlushing", true);
            StartCoroutine(FlushingAnimationDelay());

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

    ///coroutine to handle the animation of the fllush
    IEnumerator FlushingAnimationDelay()
    {
        yield return new WaitForSeconds(2);
        StartGame();
    }
}
