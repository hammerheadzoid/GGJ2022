/*
 * This script is not used anywhere at the moment.  
 */

 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpMesh : MonoBehaviour
{
    public Transform theDest;

    void OnMouseDown()
    {
        // Turn off the box collider.
        //GetComponent<MeshCollider>().enabled = false;
        GetComponent<Rigidbody>().freezeRotation = true;

        // For the object that we are holding - turn off gravity
        GetComponent<Rigidbody>().useGravity = false;

        // Make the position of box equal to the position of theDest
        this.transform.position = theDest.position;

        // Make the object we just picked up a child of my position
        this.transform.parent = GameObject.Find("BoxHolderTransform").transform;
    }

    void OnMouseUp()
    {
        // Release the parenthood from the object we were holding
        this.transform.parent = null;

        // Turn on the box collider.
        //GetComponent<MeshCollider>().enabled = true;
        GetComponent<Rigidbody>().freezeRotation = false;

        // For the object that we are holding - turn on gravity again so it will fall
        GetComponent<Rigidbody>().useGravity = true;
    }

}
