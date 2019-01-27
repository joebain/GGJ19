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
    }

    private void FixedUpdate()
    {
        transform.Translate( -1 * moveSpeed * Time.deltaTime, 0, 0);

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        anim.SetBool("IsAttacking", true);
        Debug.Log("Attaccking mode actiavted");

        if (other.gameObject.tag == "Player")
        {
            Debug.Log("PLAYER ATTACK");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        anim.SetBool("IsAttacking", false);
        Debug.Log("Deactivate mode actiavted");

    }
}
