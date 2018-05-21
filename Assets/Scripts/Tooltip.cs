using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tooltip : MonoBehaviour {

    private void Update()
    {
        if(Input.GetMouseButton(1))
        {
            Destroy(gameObject);
        }
    }

}
