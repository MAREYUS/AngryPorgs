using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using UnityEngine.UI;
using TMPro;


[System.Serializable]
public class Ammo
{
    public GameObject missile;
    public int ammo;
}


public class Slingshot : MonoBehaviour {

    // set in Inspector
    public List<Ammo> ammo;
    public float velocityMult = 10.0f;  // Mulitplier for projectile velocity
    public int activeWeapon = 0;

    // Events
    public UnityEvent SlingshotFiredEvent;
    public UnityEvent RanOutOfAmmoEvent;

    // set dynamically
    private GameObject launchPoint;
    private GameObject projectile;
    private GameObject sling;
    private Image missilePreview;
    private TextMeshProUGUI missileAmmoTxt;

    private bool aimingMode;
    private Vector3 launchPos;

    private void Awake()
    {
        aimingMode = false;
        launchPoint = GameObject.Find("LaunchPoint");
        sling = GameObject.Find("Sling");
        missilePreview = GameObject.Find("MissilePreview").GetComponent<Image>();
        missileAmmoTxt = GameObject.Find("MissileAmmoTxt").GetComponent<TextMeshProUGUI>();
        launchPoint.SetActive(false);
        launchPos = launchPoint.transform.position;

        // missile Preview in GUI
        missilePreview.sprite = ammo[activeWeapon].missile.GetComponent<SpriteRenderer>().sprite;

    }

    private void OnMouseEnter()
    {
        launchPoint.SetActive(true);
    }

    private void OnMouseExit()
    {
        if (!aimingMode)
            launchPoint.SetActive(false);
    }

    private void OnMouseDown()
    {
        if(ammo[activeWeapon].ammo > 0)
        {
            // Player pressed mouse while over Slingshot
            aimingMode = true;
            projectile = Instantiate(ammo[activeWeapon].missile); // Instantiate a projectile
            projectile.transform.position = launchPos;  // Start it at launch position
            projectile.GetComponent<Rigidbody2D>().isKinematic = true; // Set it to kinematic for now

            ammo[activeWeapon].ammo--;
        }
        
       

    }
    

    private void Update()
    {
        missileAmmoTxt.text = ammo[activeWeapon].ammo.ToString();
        missilePreview.sprite = ammo[activeWeapon].missile.GetComponent<SpriteRenderer>().sprite;

        // change Weapon
        if (Input.GetMouseButtonUp(1))
        {
            ChangeWeapon();
        }

        // If the Slingshot is not in aiming mode, don't run this code
        if (!aimingMode)
        {
            sling.SetActive(false); // disable Sling (LineRenderer)

            // if Mouse is clicked while not in aiming mode: set Camera back to Origin
            if (Input.GetMouseButton(0))
            {
                FollowCam.S.poi = launchPoint;
            }

            

            return;
        }

        else
        {
            // Get the current mouse position in 2D screen coordinates
            Vector3 mousePos = Input.mousePosition;

            // Convert the mouse position to 3D world coordinates
            mousePos.z = -Camera.main.transform.position.z;
            Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos);

            // Find the delta from launch position to 3D mouse position
            Vector3 mouseDelta = mousePos3D - launchPos;

            // Limit mouseDelta to the radius of the Slingshot SphereCollider
            float maxMagnitude = GetComponent<CircleCollider2D>().radius;
            mouseDelta = Vector3.ClampMagnitude(mouseDelta, maxMagnitude);

            // Now move the projectile to this new position
            projectile.transform.position = launchPos + mouseDelta;

            // Create Sling
            sling.SetActive(true);
            CreateSling(launchPos, mousePos3D, maxMagnitude);


            if (Input.GetMouseButtonUp(0))
            {
                // The mouse has been released
                aimingMode = false;

                // Fire off the projectile with given velocity
                Vector3 velocity = -mouseDelta * velocityMult;
                projectile.GetComponent<Missile>().Launch(velocity);
                SlingshotFiredEvent.Invoke();

                // Set the Followcam's target to our projectile
                FollowCam.S.poi = projectile;

                // Set the reference to the projectile to null as early as possible
                projectile = null;

                // remove MissileType from List if all ammo used
                if(ammo[activeWeapon].ammo == 0)
                {
                    if(ammo.Count > 1)
                    {
                        print(ammo.Count);
                        ammo.RemoveAt(activeWeapon);
                        activeWeapon = 0;
                    }
                    else
                    {
                        RanOutOfAmmoEvent.Invoke();
                    }
                    
                }
            }

        }
    }

    void ChangeWeapon()
    {
        if(activeWeapon < ammo.Count-1)
        {
            activeWeapon++;
        }
        else
        {
            activeWeapon = 0;
        }
    }

    // Draws Sling (Line) between MousePosition and StartPosition
    private void CreateSling(Vector3 p1, Vector3 p2, float radius)
    {
        Vector3[] points = new Vector3[2];  // array of points for LineRenderer
        points[0] = p1; // startPos
        points[1] = p2; // mousePos

        // limit vector to length of radius
        Vector3 deltaSling = p2 - p1;
        deltaSling = Vector3.ClampMagnitude(deltaSling, radius);
        points[1] = points[0] + deltaSling;

        sling.GetComponent<LineRenderer>().SetPositions(points);    // create Sling
    }
    
}
