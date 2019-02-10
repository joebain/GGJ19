using UnityEngine;

public class Shrimp : MonoBehaviour
{
    private Rigidbody2D body;
    private Animator animator;
    float swimTimer = 0;
    private Collider2D[] colliders;
    public float MinSwimInterval = 1f, MaxSwimInterval = 2f;
    public float SwimForce = 1f;

    enum States { Idle, Catching, Caught, Fired, Dead }
    States state = States.Idle;
    private Vector3 shootDirection;
    private float shotAt;
    private PlayerMovement fish;

    private Vector2 targetPosition;

    public float OrbitSpeed = 1;
    public float OrbitDistance = 1;
    public float LerpTime = 0.5f;
    public float ShootSpeed = 1f;
    public float ShootTime = 1f;
    private float caughtAt;
    private Vector3 caughtPos;

    public float FallSpeed = 3f;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        swimTimer = MinSwimInterval;
        colliders = GetComponents<Collider2D>();
    }

    void Update()
    {
        if (state == States.Idle)
        {
            if (swimTimer < 0)
            {
                swimTimer = Random.Range(MinSwimInterval, MaxSwimInterval);
                body.AddForce(Vector2.up * SwimForce);
            }
            else
            {
                swimTimer -= Time.deltaTime;
            }
        }
        else if (state == States.Catching || state == States.Caught)
        {
            targetPosition = fish.transform.position +
                new Vector3(
                    Mathf.Sin((Time.time + fish.GetPrawnIndex(this)) * OrbitSpeed),
                    Mathf.Cos((Time.time + fish.GetPrawnIndex(this)) * OrbitSpeed)
                    ) * OrbitDistance;

            if (state == States.Catching)
            {
                transform.position = Vector3.Lerp(caughtPos, targetPosition, (Time.time - caughtAt) / LerpTime);
                if (Time.time - caughtAt > LerpTime)
                {
                    state = States.Caught;
                    animator.SetTrigger("Caught");
                }
            }
            else if (state == States.Caught)
            {
                transform.position = targetPosition;
            }
        }
        else if (state == States.Fired)
        {
            body.velocity = shootDirection * ShootSpeed;
            if (Time.time - shotAt > ShootTime)
            {
                BeIdle();
            }
        }
    }

    private void BeIdle()
    {
        state = States.Idle;
        animator.SetTrigger("Idle");
        gameObject.layer = LayerMask.NameToLayer("Enemies");
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (state == States.Idle)
        {
            var fish = collider.GetComponent<PlayerMovement>();
            if (fish != null && fish.CanCatchPrawn())
            {
                this.fish = fish;
                fish.CaughtPrawn(this);
                state = States.Catching;
                body.isKinematic = true;
                caughtAt = Time.time;
                caughtPos = transform.position;
                foreach (Collider2D c in colliders)
                {
                    c.enabled = false;
                }
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (state == States.Fired)
        {
            var mort = collision.collider.GetComponent<Mortality>();
            if (mort != null)
            {
                //Die(); - let's see about reusing prawns
                BeIdle();
                mort.Die();
            }
        }
    }

    public void Shoot(Vector3 position, Vector3 direction)
    {
        transform.position = position;
        animator.SetTrigger("Shoot");
        foreach (Collider2D c in colliders)
        {
            c.enabled = true;
        }
        body.isKinematic = false;
        state = States.Fired;
        shootDirection = direction;
        shotAt = Time.time;
        gameObject.layer = LayerMask.NameToLayer("Default");
    }

    public void Die()
    {
        animator.SetTrigger("Dead");
        state = States.Dead;
        foreach (Collider2D c in colliders)
        {
            c.enabled = false;
        }
        body.AddForce(Vector2.down * FallSpeed);
    }
}
