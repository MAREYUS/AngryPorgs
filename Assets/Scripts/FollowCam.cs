using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour {

    public static FollowCam S; // Singleton

    // set in Inspector
    public float easing = 0.05f;
    public Vector2 minXY; 

    // set dynamically
    public GameObject poi; // Point of Interest
    private float camZ; // Cam Z Position


    private void Awake()
    {
        S = this;
        camZ = this.transform.position.z;
    }

    private void FixedUpdate()
    {
        Vector3 destination;

        // If the point of interest is empty, set it to (0,0,0)
        if (poi == null)
        {
            destination = Vector3.zero;
        }

        else
        {
            // Otherwise, get the poi's position
            destination = poi.transform.position;
        }

        // limit x and y to minimum values
        destination.x = Mathf.Max(minXY.x, destination.x);
        destination.y = Mathf.Max(minXY.y, destination.y);

        // Interpolate between current camera position and poi
        destination = Vector3.Lerp(transform.position, destination, easing);

        // Save the camZ in this destination
        destination.z = camZ;

        // Set camera to this destination
        transform.position = destination;

        // Set OrthographicSize of camera to keep the ground in view
        this.GetComponent<Camera>().orthographicSize = 5 + destination.y;
    }
    
}
