/*
 * This script is on all of the objects that will be able to be picked up by the player  
 */

 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ThrowObject : MonoBehaviour
{
    public Transform player;
    public Transform playerCam;
    public Transform theDest;
    
    public bool hasPackMedium;                          // what am i holding? In this case a PackageMedium
    public float throwForce = 10;                       // If object thrown, this is the force to move it.
    public float dist;                                  // How far are we away from the object in question
    public bool playerWithinPickupDistance = false;     // 
    public bool beingCarried = false;                   // If the object is being carried
    public bool touched = false;                        // Used to see if we are touching another object - but i do not think this works
    //public TMP_Text ForceDisplay;                       // This is the text object dragged into the inspecter
    //public TMP_Text ScoreDisplay;                       // This is the text object dragged into the inspecter
    public float num = 1;                               //public float tempScore;
    public int emptyHands;                              // Check if you are carrying anything, if you are you cannot carry more. 
    public float throwingScore;                         // When you throw something, this variable will count the force that it was thrown at
    
    // Start is called before the first frame update
    void Start()
    {
        //countUp = true;
        //counter.text = "";
        throwingScore = PlayerPrefs.GetFloat("BoxThrowingScore");
        //ScoreDisplay.text = throwingScore.ToString("f2");
    }

    // Update is called once per frame
    void Update()
    {
        num = 0;
        num = Mathf.PingPong((Time.time) * 5, 10);
        //ForceDisplay.text = num.ToString("f2");
        
    }

    // This method is used in the ShootBox.cs script
    public void Fling()
    {
        GetComponent<Rigidbody>().AddForce(playerCam.forward * throwForce * (num / 5));
    }

    private void OnTriggerEnter(Collider other)
    {
        print("Printed from OnTriggerEnter");

        if (other.gameObject.tag == "Book")
        {
            print("You have handled a book");
            throwingScore = PlayerPrefs.GetFloat("BoxThrowingScore");
            throwingScore = throwingScore + num;
            Debug.Log("Num is " + num);
            PlayerPrefs.SetFloat("BoxThrowingScore", throwingScore);
            throwingScore = PlayerPrefs.GetFloat("BoxThrowingScore");
            //ScoreDisplay.text = throwingScore.ToString("f2");
        }
    }
}
