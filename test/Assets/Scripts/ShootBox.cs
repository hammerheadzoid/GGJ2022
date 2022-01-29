/*
 * This is from Brackeys video - https://www.youtube.com/watch?v=THnivyG0Mvo
 * This script is on the Main Camera. Main Camera is a child of First Person Player  
 * 
 * Throwable
 * =========
 * If you want an object to be able to be throwable, just set the tag to throwable. 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootBox : MonoBehaviour
{
    public float damage = 10f;
    public float range = 2;
    public float num = 1;
    RaycastHit hit;
    public Camera fpsCam;
    public Transform theDest;
    public Transform playerCam;
    public float throwForce = 10;   // If object thrown, this is the force to move it.
    public Text counter;        // This is the text object dragged into the inspecter
    public bool holdingItem;
    public GameObject ShowHideForce;
    

    private void Start()
    {
        holdingItem = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Count();

        if (Input.GetMouseButtonDown(0) && holdingItem == false)
        {
            SelectObject();
        }
        else if (Input.GetMouseButtonDown(0) && holdingItem == true)
        {
            CastObject();
        }

        if (Input.GetMouseButtonDown(2) && holdingItem == true)
        {
            ReleaseObject();
        }
    }

    void SelectObject()
    {
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            if (hit.transform.tag == "Throwable")
            {
                Debug.Log("You have Raycast against a " + hit.transform.name);

                // isKinematic means it is not effected by gravity
                hit.rigidbody.isKinematic = true;

                // Turn off the box collider.
                //hit.transform.GetComponent<BoxCollider>().enabled = false;

                // Make the position of box equal to the position of theDest
                hit.transform.position = theDest.position;

                // Make the object we just picked up a child of my position
                hit.transform.parent = GameObject.Find("BoxHolderTransform").transform;

                holdingItem = true;

                // Show the Forcebar
                ShowHideForce.SetActive(true);

            }

            // Set computer on and then the rest of the code is dealt with in the InitializationScript
            if (hit.transform.tag == "Computer")
            {
                Debug.Log("Computer is turned on here...");
                PlayerPrefs.SetInt("ComputerOn",58);  // 0 = Off, 58 = On
            }
        }
    }

    void CastObject()
    {
        if(hit.transform.tag == "Throwable")
        {
            // isKinematic means it is not effected by gravity
            hit.rigidbody.isKinematic = false;

            // Release the parenthood from the object we were holding
            hit.transform.parent = null;

            ThrowObject throwObject = hit.transform.GetComponent<ThrowObject>();
            throwObject.Fling();

            holdingItem = false;

            // Stop showing the Forcebar
            ShowHideForce.SetActive(false);

        }
    }

    void ReleaseObject()
    {
        // isKinematic means it is not effected by gravity
        hit.rigidbody.isKinematic = false;

        // Release the parenthood from the object we were holding
        hit.transform.parent = null;

        holdingItem = false;

        // Stop showing the Forcebar
        ShowHideForce.SetActive(false);

    }

    private void Count()
    {
        num = 0;
        num = Mathf.PingPong((Time.time) * 1, 100);
        counter.text = num.ToString("f2");
    }
}
