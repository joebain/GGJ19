using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEndTrigger : MonoBehaviour
{
    public Transform PlayerSuckTarget;

    public float SuckSpeed = 1f, SuckRotSpeed = 10f;
    private bool triggered = false;

    void OnTriggerEnter2D(Collider2D collider)
    {
        PlayerMovement player = collider.GetComponent<PlayerMovement>();
        if (player != null && !triggered)
        {
            triggered = true;

            StartCoroutine(SuckPlayerDownPipe(player));
        }
    }

    private IEnumerator SuckPlayerDownPipe(PlayerMovement player)
    {
        player.enabled = false;
        Rigidbody2D body = player.GetComponent<Rigidbody2D>();
        body.isKinematic = true;
        body.velocity = Vector2.zero;
        body.angularVelocity = 0;
        
        float rot = 0;
        while (true) {
            Vector3 towards = PlayerSuckTarget.position - player.transform.position;
            float distance = towards.magnitude;
            if (player.transform.position.y < PlayerSuckTarget.position.y) {
                break;
            }
            player.transform.position += towards / distance * SuckSpeed * Time.deltaTime;
            player.transform.rotation = Quaternion.Euler(0, 0, rot);
            rot += SuckRotSpeed * Time.deltaTime;
            yield return null;
        }
        player.transform.rotation = Quaternion.identity;
        player.enabled = true;
        player.GetComponent<Rigidbody2D>().isKinematic = false;
        Game.Instance.NextLevel();
        SceneManager.LoadScene("ActualGame");
    }
}
