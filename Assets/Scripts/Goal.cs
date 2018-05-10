using UnityEngine;
using System.Collections;
using System;


public class Goal : MonoBehaviour {

    
	// A static field visible from anywhere
    
    public static event EventHandler GoalHit;

    


    void OnTriggerEnter2D(Collider2D other) {
		// Check if the hit comes from a projectile
		if(other.gameObject.tag == "Missile") {
            print("Goal Hit (Goal)");
            // Set alpha to higher opacity
            Color c = GetComponent<SpriteRenderer>().material.color;
            c = new Color(0f, 1f, 0f, 1f);
            GetComponent<SpriteRenderer>().material.color = c;
            GetComponent<Collider2D>().enabled = false;

            OnGoalHit();
        }
	}

    public virtual void OnGoalHit()
    {
        if (GoalHit != null)
            GoalHit(this, EventArgs.Empty);
    }

}
