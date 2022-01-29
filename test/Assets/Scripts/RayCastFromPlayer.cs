/*
 * This script is on 'First Person Player' -> 'Main Camera'  
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastFromPlayer : MonoBehaviour
{
    public LayerMask mask;  // The mask is a list of things I hit and only want to be recorded
    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);

        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, 100, mask))
        {
            // If I hit something show red
            Debug.DrawLine(ray.origin, hitInfo.point, Color.red);
            print(hitInfo.transform.name + ". It is " + hitInfo.distance + " away from you.");
        }
        else
        {
            // If I am hitting nothing show green
            Debug.DrawLine(ray.origin, ray.origin + ray.direction * 100, Color.green);
        }

        
    }
}
