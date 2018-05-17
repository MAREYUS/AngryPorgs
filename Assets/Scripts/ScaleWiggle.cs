using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleWiggle : MonoBehaviour
{
    
    public float speed = 1.0f;
    public float strength = 1.0f;
    public float turnOfAfterSeconds = 1;

    float time = 0.0f;
    private Vector3 originalScale;

    private void Awake()
    {
        originalScale = transform.localScale;   // save original scale
    }

    private void OnEnable()
    {
        time = 0;
    }

    void Update ()
    {
        time += Time.deltaTime; // calculates time in seconds
        // Damping
        float newScale = (Mathf.Exp(-time) * Mathf.Sin(2 * Mathf.PI * time)) * strength;
        transform.localScale = new Vector3(originalScale.x + newScale, originalScale.y + newScale, originalScale.z + newScale);
    }
    

    void Enable(float time)
    {
        enabled = true;
        CancelInvoke("Disable");    // reset timer if called multiple times
        Invoke("Disable", time);
    }

    void Disable()
    {
        enabled = false;
    }

    public void Wiggle()
    {
        Enable(turnOfAfterSeconds);
    }
}
