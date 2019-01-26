using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatEnemy : MonoBehaviour
{

    public float moveSpeed = 0.1f;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("IsAttacking", false);

    }

    private void FixedUpdate()
    {
        transform.Translate( -1 * moveSpeed * Time.deltaTime, 0, 0);

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("TESTING tirggers mode actiavted");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("PLAYER ATTACK");
            anim.SetBool("IsAttacking", true);
        }
        else
        {
            anim.SetBool("IsAttacking", false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        anim.SetBool("IsAttacking", false);
        Debug.Log("Deactivate mode actiavted");
    }
}
