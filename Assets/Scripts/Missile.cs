using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Missile : MonoBehaviour {

    public float speedMultiplier = 1;

    public abstract void Launch(Vector3 vel);
}
