using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Events;


public class Goal : MonoBehaviour {
    
    public UnityEvent GoalHitEvent;
    public FloatReference maxHP;
    public FloatReference collisionForceFactor;
    public FloatReference killPlane;


    private bool dead = false;
    private float hP;

    private void Awake()
    {
        hP = maxHP.Value;
    }

    private void Update()
    {
        if (hP <= 0 && !dead)
        {
            KillEnemy();
            dead = true;
        }

        if (transform.position.y <= killPlane)
            gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        hP = hP - (collision.relativeVelocity.magnitude * collisionForceFactor);

    }


    private void KillEnemy()
    {
        
        print("Goal Hit (Goal)");
        // Set alpha to higher opacity
        Color c = GetComponent<SpriteRenderer>().material.color;
        c = new Color(0f, 1f, 0f, 1f);
        GetComponent<SpriteRenderer>().material.color = c;
        if(GetComponent<SineMovement>() != null)
            GetComponent<SineMovement>().enabled = false;
        GetComponent<Rigidbody2D>().gravityScale = 1.0f;
        GetComponent<Collider2D>().enabled = false;


        GoalHitEvent.Invoke();
        
    }

}
