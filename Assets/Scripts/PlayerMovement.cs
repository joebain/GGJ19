using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D body;

    public Vector2 BOOST;
    public float FORCE_APPLIED = 4f;
    public float TOP_SPEED = 1f;
    public float BOOST_FORCE = 10f;
    public bool FORWARD_BOOST_FLAG;
    public float BOOST_DURATION = 0.3f;
    public float BOOST_COOLDOWN = 2f;
    Vector2 velocity;
    Vector2 relative_point;
    float mag;
    private float boostTime;

    public ParticleSystem bubbleTrail;

    // Start is called before the first frame update
    void Start()
    {
        relative_point = new Vector2(0.0f, 0.0f);

        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        velocity = body.GetRelativePointVelocity(relative_point);
        mag = velocity.magnitude;

        float topSpeed = FORWARD_BOOST_FLAG ? TOP_SPEED * 2f : TOP_SPEED;
        if (mag > topSpeed)
        {
            body.velocity = velocity.normalized * topSpeed;
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            body.AddForce(Vector2.up*FORCE_APPLIED);
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            body.AddForce(Vector2.left*FORCE_APPLIED);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            body.AddForce(Vector2.down*FORCE_APPLIED);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            body.AddForce(Vector2.right*FORCE_APPLIED);
        }
        if (Input.GetKeyDown(KeyCode.Space) && Time.time - boostTime > BOOST_COOLDOWN)
        {
            boostTime = Time.time;
            AkSoundEngine.PostEvent("FishSpeedBurst", gameObject);
        }
        if (Input.GetKey(KeyCode.Space) && Time.time - boostTime < BOOST_DURATION)
        {
            body.AddForce(Vector2.right * BOOST_FORCE);
            FORWARD_BOOST_FLAG = true;
            var emission = bubbleTrail.emission;
            emission.enabled = true;
        }
        else
        {
            FORWARD_BOOST_FLAG = false;
            var emission = bubbleTrail.emission;
            emission.enabled = false;
        }
    }
}
