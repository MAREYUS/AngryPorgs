using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineMovement : MonoBehaviour {

    Vector2 original;

    public Vector2 strength;
    public Vector2 speed;

    void Start()
    {
        this.original.x = this.transform.position.x;
        this.original.y = this.transform.position.y;
    }

    void Update()
    {
        transform.position = new Vector3(original.x + ((float)Mathf.Sin(Time.time*speed.x) * strength.x),
            original.y + ((float)Mathf.Cos(Time.time*speed.y) * strength.y),
            transform.position.z);
    }
}
