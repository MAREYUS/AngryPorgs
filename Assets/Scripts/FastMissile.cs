using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FastMissile : Missile
{

    public override void Launch(Vector3 vel)
    {
        GetComponent<Rigidbody2D>().isKinematic = false;
        GetComponent<Rigidbody2D>().velocity = vel * speedMultiplier;
    }
}
