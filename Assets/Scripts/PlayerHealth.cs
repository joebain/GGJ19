using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Deadly"))
        {
            Debug.Log("dying");
            HUDController.Instance.ShowGameOver();
            Time.timeScale = 0;
            StartCoroutine(GoToEndScreen());
        }
    }

    private IEnumerator GoToEndScreen()
    {
        yield return new WaitForSecondsRealtime(2f);
        Time.timeScale = 1;
        SceneManager.LoadScene("PlayerNameEntry");
    }
}
