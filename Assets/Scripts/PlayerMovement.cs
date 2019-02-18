using Rewired;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D body;
    private Rewired.Player player;
    public Vector2 BOOST;
    public float FORCE_APPLIED = 4f;
    public float TOP_SPEED = 1f;
    public float BOOST_FORCE = 10f;
    public bool FORWARD_BOOST_FLAG;
    public float BOOST_DURATION = 0.3f;
    public float BOOST_COOLDOWN = 2f;
    public float CONSTANT_SCROLL_FORCE = 2f;
    Vector2 velocity;
    Vector2 relative_point;
    float mag;
    private float boostTime;

    public ParticleSystem bubbleTrail;

    List<Shrimp> prawns = new List<Shrimp>();

    public int MaxPrawns = 3;

    public Shrimp PrawnPrefab;

    // Start is called before the first frame update
    void Start()
    {
        relative_point = new Vector2(0.0f, 0.0f);

        body = GetComponent<Rigidbody2D>();

        player = ReInput.players.GetPlayer(RewiredConsts.Player.ONE);
        
        if (Game.Instance != null && Game.Instance.player != null) {
            int shrimps = Game.Instance.player.ShrimpCount;
            Debug.Log("starting with " + shrimps + " shrimps");
            for (int s = 0; s < shrimps; s++)
            {
                var prawn = Instantiate(PrawnPrefab);
                prawn.transform.position = transform.position;
                prawn.Catch(this);
            }
        }
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

        body.AddForce(Vector2.up*FORCE_APPLIED * player.GetAxis(RewiredConsts.Action.MOVEVERTICAL));
        body.AddForce(Vector2.right*FORCE_APPLIED * player.GetAxis(RewiredConsts.Action.MOVEHORIZONTAL));
        body.AddForce(Vector2.right * CONSTANT_SCROLL_FORCE);


        if (player.GetButtonDown(RewiredConsts.Action.SWIM) && Time.time - boostTime > BOOST_COOLDOWN)
        {
            boostTime = Time.time;
            AkSoundEngine.PostEvent("FishSpeedBurst", gameObject);
        }
        if (player.GetButton(RewiredConsts.Action.SWIM) && Time.time - boostTime < BOOST_DURATION)
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
        if (player.GetButtonDown(RewiredConsts.Action.FIRE))
        {
            if (prawns.Count > 0)
            {
                prawns[prawns.Count - 1].Shoot(transform.position+Vector3.right*0.1f, Vector3.right);
                prawns.RemoveAt(prawns.Count - 1);
                Game.Instance.player.ShrimpCount = prawns.Count;
                AkSoundEngine.PostEvent("ShrimpCanon", gameObject);
            }
        }
    }

    public bool CanCatchPrawn()
    {
        return prawns.Count < MaxPrawns;
    }

    public void CaughtPrawn(Shrimp prawn)
    {
        if (prawns.Contains(prawn)) return;
        prawns.Add(prawn);
        Game.Instance.player.ShrimpCount = prawns.Count;
    }

    public int GetPrawnIndex(Shrimp prawn)
    {
        return prawns.IndexOf(prawn);
    }

    public void KillPrawn()
    {
        prawns[prawns.Count - 1].Die();
        prawns.RemoveAt(prawns.Count - 1);
        Game.Instance.player.ShrimpCount = prawns.Count;
    }

    public bool HasPrawns()
    {
        return prawns.Count > 0;
    }
}
