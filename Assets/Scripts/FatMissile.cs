using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FatMissile : Missile {


    public override void Launch(Vector3 vel)
    {
        GetComponent<Rigidbody2D>().isKinematic = false;
        GetComponent<Rigidbody2D>().velocity = vel * 0.5f;
    }
}
