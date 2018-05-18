using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Events;


public class Goal : MonoBehaviour {
    
    public UnityEvent GoalHitEvent; 
    
 //   void OnTriggerEnter2D(Collider2D other) {
	//	// Check if the hit comes from a projectile
	//	if(other.gameObject.tag == "Missile") {
 //           print("Goal Hit (Goal)");
 //           // Set alpha to higher opacity
 //           Color c = GetComponent<SpriteRenderer>().material.color;
 //           c = new Color(0f, 1f, 0f, 1f);
 //           GetComponent<SpriteRenderer>().material.color = c;
 //           GetComponent<Collider2D>().enabled = false;
           
 //           GoalHitEvent.Invoke();
 //       }
	//}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Missile")
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

}
