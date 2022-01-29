/*
 * This script is on 'First Person Player' -> 'Main Camera'  
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;   // Simply used as a multiplier for sensitivity
    public Transform playerBody;            // the players body will be dropped in here to gather data from that object
    float xRotation = 0f;   // Look up and down
    public float clamp1 = -90f;
    public float clamp2 = 56.5f;
    public Transform playerCam;
    RaycastHit hit; // Make a raycast so i can more efficently hit the exact object that i want to hit

    // Update is called once per frame
    void Update()
    {
        // Gather mouse axis data and multiply it by sensitivity
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // xRotation uses axis of Mouse Y and the clamp prevents you from flipping your head around
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, clamp1, clamp2);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        int currentPlayerState = PlayerPrefs.GetInt("PlayerState");

        if (currentPlayerState == 1) // If we are in First Person Mode
        {
            // vector3.up is the Y Axis - Look left and right
            playerBody.Rotate(Vector3.up * mouseX);
        }

        // Cast ray to see if i am hitting object
        /*if (Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hit, 2.5f))
        {
            if (hit.transform.name == "PackageMedium")
            {
                Debug.Log("You have Raycast against " + hit.transform.name);
                //hasPackMedium = true;
            }
            else
            {
                //hasPackMedium = false;
            }
        }*/

    }

   
}
