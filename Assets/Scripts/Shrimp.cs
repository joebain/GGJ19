using UnityEngine;

public class Shrimp : MonoBehaviour
{
    private Rigidbody2D body;

    float swimTimer = 0;
    public float MinSwimInterval = 1f, MaxSwimInterval = 2f;
    public float SwimForce = 1f;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        swimTimer = MinSwimInterval;
    }

    void Update()
    {
        if (swimTimer < 0)
        {
            swimTimer = Random.Range(MinSwimInterval, MaxSwimInterval);
            body.AddForce(Vector2.up * SwimForce);
            Debug.Log("Swim");
        }
        else
        {
            swimTimer -= Time.deltaTime;
        }
    }
}
