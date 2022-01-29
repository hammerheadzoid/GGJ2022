using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public CharacterController controller;
    //  movement variables are set to private so they would update in unity -- Oscar
    private float walk = 4f;
    private float run = 10f;
    private float gravity = -9.81f;
    private float standStill = 0;
    private float jumpHeight = 1;

    // The following is used to check if the player is on the ground so we can jump and land. -- Donal
    public Transform groundCheck; // This box  is a child of the player 
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;

    Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        //rb = GetComponent<Rigidbody>();
    }

    

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Why Transform.right?
        Vector3 move = transform.right * x + transform.forward * z;

        // notes: changed if statement; so that default speed is run, and while grounded,
        // holding [Left Shift] while grounded will change run to a walk,
        // this means jumps are at running speed -- Oscar


        standStill = move.magnitude;
        if (standStill < 0.01f)
        {
            //animator.SetFloat("speedPercent", 0);
        }

        // we are walking with shift -- Donal
        if (isGrounded && Input.GetKey(KeyCode.LeftShift))
        {

            // if they are not running they are walking
            controller.Move(move * walk * Time.deltaTime);
            //PlayerAnimationController.AnimationWalk();
            //animator.SetFloat("speedPercent", 0.5f);
        }
        else
        {
            if (standStill < 0.01f)
            {
                //Debug.Log("Stand Still");
                //PlayerAnimationController.AnimationWalk();
                //animator.SetFloat("speedPercent", 0);
            }
            else
            {

                //Debug.Log("Run");
                controller.Move(move * run * Time.deltaTime);
                //animator.SetFloat("speedPercent", 1);
                //Debug.Log("Walk");
            }
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Time.deltaTime is used twice because this is the physics of a free fall
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
