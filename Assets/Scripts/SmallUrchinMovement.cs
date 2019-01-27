using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallUrchinMovement : MonoBehaviour
{
    private Rigidbody2D body;
    private bool collided_top;
    private bool collided_bottom;
    Vector2 UP;
    Vector2 DOWN;
    Vector2 LEFT;
    Vector2 RIGHT;
    public float SPEED;
    public float TOP_SPEED;
    Vector2 velocity;
    Vector2 relative_point;
    float start_point;
    float mag;
    float t;
    float previous_point;
    Vector2 change_v;

    // Start is called before the first frame update
    void Start()
    {

        TOP_SPEED = 4.0f;
        SPEED = 1.0f;
        t = 0.0f;

        relative_point = new Vector2(0.0f, 0.0f);
        body = GetComponent<Rigidbody2D>();

        start_point = body.GetRelativePoint(relative_point).y;
        UP = new Vector2(0.0f, SPEED);
        DOWN = new Vector2(0.0f, -SPEED);
        LEFT = new Vector2(-SPEED, 0.0f);
        RIGHT = new Vector2(SPEED, 0.0f);

        collided_top = false;
        collided_bottom = false;
    }

    // Update is called once per frame
    void Update()
    {
        // print(velocity);
        // print(mag);

        /*
        if (mag > TOP_SPEED)
        {
            body.velocity = velocity.normalized * TOP_SPEED;
        }
        */

        // if velocity is positive
        // move slowly up
        // when you collide with wall
        // high velocity down
        body.velocity = new Vector2(0.0f, 2.0f);

        if (collided_top == true)
        {
            body.velocity = new Vector2(0.0f, -6.0f);
            // wait couple of seconds
            // print("changed velocity");
        }
        else if (collided_bottom == true)
        {
            // print("floating up");
            body.velocity = new Vector2(0.0f, 2.0f);
        }


        // change_v = new Vector2(0.0f, Mathf.Sin(t + (body.GetRelativePoint(relative_point).y)));
        // body.velocity = change_v;
        // t += Time.deltaTime / 2;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        float current_point = body.GetRelativePoint(relative_point).y;
        if (collision.collider.CompareTag("Walls") && current_point > previous_point && current_point > start_point)
        {
            previous_point = body.GetRelativePoint(body.position).y;
            print("Has collided with top");
            collided_bottom = false;
            collided_top = true;
        }
        
        if (collision.collider.CompareTag("Walls") && current_point < start_point && current_point < previous_point)
        {
            previous_point = body.GetRelativePoint(body.position).y;
            print("Has collided with bottom");
            collided_top = false;
            collided_bottom = true;
        }
    }
}