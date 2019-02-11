using UnityEngine;

public class Mortality : MonoBehaviour
{
    private Collider2D[] colliders;
    private Rigidbody2D body;
    private EnemyMovement enemyMovement;
    
    private Animator animator;
    private bool isDead;

    public float FallForce = 1f;

    public string SoundEvent = "";

    void Awake() {
        colliders = GetComponents<Collider2D>();
        body = GetComponent<Rigidbody2D>();

        enemyMovement = GetComponent<EnemyMovement>();

        animator = GetComponent<Animator>();
    }

    public void Die()
    {
        foreach (Collider2D col in colliders) {
            col.enabled = false;
        }

        isDead = true;

        if (enemyMovement != null)
        {
            enemyMovement.enabled = false;
        }
        if (animator != null)
        {
            animator.SetTrigger("Dead");
        }
        if (SoundEvent != null && SoundEvent != "")
        {
            AkSoundEngine.PostEvent(SoundEvent, gameObject);
        }
    }

    void Update()
    {
        if (isDead)
        {
            body.AddForce(Vector2.down * FallForce);
        }
    }
}
