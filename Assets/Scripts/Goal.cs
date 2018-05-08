using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour {

	// A static field visible from anywhere
	public static bool goalMet = false;

    public delegate void GoalHit();
    public static event GoalHit GoalHitEvent;

    

	void OnTriggerEnter2D(Collider2D other) {
		// Check if the hit comes from a projectile
		if(other.gameObject.tag == "Missile") {
			goalMet = true;
            print("Goal Hit");
            // Set alpha to higher opacity
            Color c = GetComponent<SpriteRenderer>().material.color;
            c.a = 1.0f;
            GetComponent<SpriteRenderer>().material.color = c;
            GetComponent<Collider2D>().enabled = false;

            if (GoalHitEvent != null)
                GoalHitEvent();
        }
	}
}
