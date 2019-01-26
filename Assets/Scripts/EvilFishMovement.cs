using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilFishMovement : MonoBehaviour
{
    private Rigidbody2D body;
    Vector2 UP;
    Vector2 DOWN;
    Vector2 LEFT;
    Vector2 RIGHT;
    public float SPEED;
    public float TOP_SPEED;
    Vector2 velocity;
    Vector2 relative_point;
    float mag;
    float t;
    Vector2 change_v;

    // Start is called before the first frame update
    void Start()
    {   

        TOP_SPEED = 4.0f;
        SPEED = 1.0f;
        t = 0.0f;

        relative_point = new Vector2(0.0f, 0.0f);

        body = GetComponent<Rigidbody2D>();
        UP = new Vector2(0.0f, SPEED);
        DOWN = new Vector2(0.0f, -SPEED);
        LEFT = new Vector2(-SPEED, 0.0f);
        RIGHT = new Vector2(SPEED, 0.0f);
        /* TODO: change boost so it adds a force in the direction that Fish is headed. 
        This is a requirement if fish rotates */
    }

    // Update is called once per frame
    void Update()
    {
        velocity = body.GetRelativePointVelocity(relative_point);
        mag = velocity.magnitude;
            if (mag > TOP_SPEED)
            {
                body.velocity = velocity.normalized * TOP_SPEED;
            }
        // print(Mathf.Sin(t + (body.GetRelativePoint(relative_point).x)));
        
        change_v = new Vector2 (LEFT.x, Mathf.Sin(t + (body.GetRelativePoint(relative_point).x)));
        body.velocity = change_v;
        t += Time.deltaTime / 2;
    }
}
