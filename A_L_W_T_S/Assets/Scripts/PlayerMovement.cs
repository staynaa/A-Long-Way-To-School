using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    // Initialize RigidBody 2D Object
    private Rigidbody2D rb;

    //Initialize Animator object 
     private Animator animator;
    
    //Player's x-axis value
    private float horizontalMovement;

    //Horizontal movement speed of player;
   [SerializeField] private float speed = 2f;

    private float runSpeedModifer = 2f;


    //Determine If Player Is Facing Right 
    private bool isFacingRight = true;

    //Determine If Player Is Running
    private bool isRunning = false;


    // Awake is called before the start of application
    void Awake()
    {
         // Reference to player's rigidBody 2D
        rb = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();


        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       // Store value given by the key presses of button associated horinzontal movement
       horizontalMovement = Input.GetAxisRaw("Horizontal");


        // If left-shift is clicked and held down enable isRunning 
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            isRunning = true;
        }

        // If left-shift is released disable isRunning 
        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            isRunning = false;
        }

    }


    // FixedUpdate is called once every time step is settled
    void FixedUpdate() 
    {
        move(horizontalMovement);
    }




/* 
    Method Name: move()
    Parameter: 
    float dir - Value associated with direction of player in regards to x-axis 
    Description: Manipulates Movement For Player
*/     
     void move(float dir)
    {
        // Horizontal movement for Player

        //Set value of player x-axis using dir and speed
        float xVal = dir * speed * 100 * Time.deltaTime;

        // If player is running multiply with running modifier
        if(isRunning){
            xVal *= runSpeedModifer;
        }

        //Create Vector2 object for the player velocity
        Vector2 targetVelocity = new Vector2(xVal, rb.velocity.y);

        //Set the player's velocity
        rb.velocity = targetVelocity;


        //Facing direction for player sprite

        // If looking right and clicked right (flip to the left)
       if(dir < 0 && isFacingRight)
        {
            flip();
        }
       
       // If looking left and clicked left (flip to the right)
        else if (dir > 0 && !isFacingRight)
        {
            flip();
        }

        Debug.Log(rb.velocity.x);

        // 0 idle, 4 walking , 8 running
        //Set the flot xVelocity accroding to the x value of the RigidBody2D Velocity
        animator.SetFloat("xVelocity", Mathf.Abs(rb.velocity.x));
    }

  /* 
    Method Name: flip()
    Description: Changes the value of (boolean) "isFacingRight" from true/false 
    depending where player sprite if facing and rotates the player sprite to face 
    left or right
*/  
    private void flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0,180f,0f);
    }
}  



