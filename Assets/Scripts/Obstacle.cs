using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {

    public FloatReference maxHP;
    public FloatReference collisionForceFactor;

    private float hP;

    private void Awake()
    {
        hP = maxHP.Value;
    }

    private void Update()
    {
        if (hP <= 0)
            Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
            hP = hP - (collision.relativeVelocity.magnitude * collisionForceFactor);
        
    }
}
