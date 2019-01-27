using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D body;
    public Vector2 UP;
    public Vector2 DOWN;
    public Vector2 LEFT;
    public Vector2 RIGHT;
    public Vector2 BOOST;
    public float FORCE_APPLIED;
    public float TOP_SPEED;
    public bool KEY_HOLD_FLAG;
    public bool KEY_TAP_FLAG;
    public bool FORWARD_BOOST_FLAG;   
    Vector2 velocity;
    Vector2 relative_point;
    float mag;

    // Start is called before the first frame update
    void Start()
    {
        // set this so the player can hold a key rather than continually press
        KEY_HOLD_FLAG = true;
        KEY_TAP_FLAG = false;
        FORWARD_BOOST_FLAG = true;

        TOP_SPEED = 4.0f;
        FORCE_APPLIED = 1.0f;

        relative_point = new Vector2(0.0f, 0.0f);

        body = GetComponent<Rigidbody2D>();
        UP = new Vector2(0.0f, FORCE_APPLIED);
        DOWN = new Vector2(0.0f, -FORCE_APPLIED);
        LEFT = new Vector2(-FORCE_APPLIED, 0.0f);
        RIGHT = new Vector2(FORCE_APPLIED, 0.0f);
        /* TODO: change boost so it adds a force in the direction that Fish is headed. 
        This is a requirement if fish rotates */
        BOOST = new Vector2(5.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        velocity = body.GetRelativePointVelocity(relative_point);
        mag = velocity.magnitude;
<<<<<<< HEAD
        // print(velocity);
        // print(mag);
=======
        //print(velocity);
        //print(mag);
>>>>>>> b8e55a7028ca2e8824c2c9f6af6d563ed44c9af2

        if (KEY_HOLD_FLAG)
        {
            if (mag > TOP_SPEED)
            {
                body.velocity = velocity.normalized * TOP_SPEED;
            }
            if (Input.GetKey(KeyCode.W))
            {
                body.AddForce(UP);
<<<<<<< HEAD
                // print("W pressed");
=======
                //print("W pressed");
>>>>>>> b8e55a7028ca2e8824c2c9f6af6d563ed44c9af2
            }
            if (Input.GetKey(KeyCode.A))
            {
                body.AddForce(LEFT);
<<<<<<< HEAD
                // print("A pressed");
=======
                //print("A pressed");
>>>>>>> b8e55a7028ca2e8824c2c9f6af6d563ed44c9af2
            }
            if (Input.GetKey(KeyCode.S))
            {
                body.AddForce(DOWN);
<<<<<<< HEAD
                // print("S pressed");
=======
                //print("S pressed");
>>>>>>> b8e55a7028ca2e8824c2c9f6af6d563ed44c9af2
            }
            if (Input.GetKey(KeyCode.D))
            {
                body.AddForce(RIGHT);
<<<<<<< HEAD
                // print("D pressed");
=======
                //print("D pressed");
>>>>>>> b8e55a7028ca2e8824c2c9f6af6d563ed44c9af2
            }
            if (Input.GetKeyDown(KeyCode.Space) && FORWARD_BOOST_FLAG)
            {
                body.AddForce(BOOST);
<<<<<<< HEAD
                // print("Space pressed");
=======
                //print("Space pressed");
>>>>>>> b8e55a7028ca2e8824c2c9f6af6d563ed44c9af2
            }
        }
        else if (KEY_TAP_FLAG && mag < 4)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                body.AddForce(UP);
<<<<<<< HEAD
                // print("W pressed");
=======
                //print("W pressed");
>>>>>>> b8e55a7028ca2e8824c2c9f6af6d563ed44c9af2
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                body.AddForce(LEFT);
<<<<<<< HEAD
                // print("A pressed");
=======
                //print("A pressed");
>>>>>>> b8e55a7028ca2e8824c2c9f6af6d563ed44c9af2
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                body.AddForce(DOWN);
<<<<<<< HEAD
                // print("S pressed");
=======
                //print("S pressed");
>>>>>>> b8e55a7028ca2e8824c2c9f6af6d563ed44c9af2
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                body.AddForce(RIGHT);
<<<<<<< HEAD
                // print("D pressed");
=======
                //print("D pressed");
>>>>>>> b8e55a7028ca2e8824c2c9f6af6d563ed44c9af2
            }
            if (Input.GetKeyDown(KeyCode.Space) && FORWARD_BOOST_FLAG)
            {
                body.AddForce(BOOST);
<<<<<<< HEAD
                // print("BOOSTING!!!!");
=======
                //print("BOOSTING!!!!");
>>>>>>> b8e55a7028ca2e8824c2c9f6af6d563ed44c9af2
            }
        }
    }
}
