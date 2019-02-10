using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    private PlayerMovement movement;

    void Awake()
    {
        movement = GetComponent<PlayerMovement>();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Deadly"))
        {
            if (movement.HasPrawns())
            {
                movement.KillPrawn();
                var mort = collision.collider.GetComponent<Mortality>();
                if (mort != null)
                {
                    mort.Die();
                }
            }
            else
            {
                Debug.Log("dying");
                AkSoundEngine.PostEvent("FishDeath", gameObject);
                HUDController.Instance.ShowGameOver();
                Time.timeScale = 0;
                StartCoroutine(GoToEndScreen());
            }
        }
    }

    private IEnumerator GoToEndScreen()
    {
        yield return new WaitForSecondsRealtime(2f);
        Time.timeScale = 1;
        SceneManager.LoadScene("PlayerNameEntry");
    }
}
