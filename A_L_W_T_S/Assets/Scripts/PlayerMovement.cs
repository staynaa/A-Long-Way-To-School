using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{


    #region Player Unity Objects

    // Initialize RigidBody 2D Object
    private Rigidbody2D rb;

    //Initialize Animator object 
    private Animator animator;
    
    #endregion


    #region Collider Reference Variables

    [Header("Collider Reference Variables")]

    [Tooltip("Reference to a collider being used as \"standing\" collider")]
    // Reference To Player's Standing Collider
    [SerializeField] private Collider2D standingCollider; 


    [Tooltip("Reference to player's GroundCheck collider child")]
    // Reference To Player's GroundCheck Collider 
    [SerializeField] private Transform groundCheckCollider;


    [Tooltip("Reference to player's OverheadCheck collider child")]
    // Reference To Player's OverCheck Collider 
    [SerializeField] Transform overheadCheckCollider;
    

    [Tooltip("Reference to ground that player can jump on while GroundCheck collider is in contact with in")]
    // Reference LayerMask for Jumpable Ground 
    [SerializeField] private LayerMask jumpableGround;

    #endregion 


    #region Movement Variables  

    //Player's x-axis value
    private float horizontalMovement;

    [Header("Movement Variables")]
   
    /*IMPORTANT:
    If any changes are made to the horizontal or run variable values
    changes for the animations associated with that variable must be made in the unity animator
    in regards to their parameters values so animations are in sync with those movements 
    */ 

    [Tooltip("Horizontal movement speed of player")]
    //Horizontal movement speed of player;
    [SerializeField] private float speed = 2f;

    [Tooltip("Sprint speed of player")]
    //Run Speed of player
    [SerializeField] private float runSpeedModifer = 2f;

    [Tooltip("Horizontal movement speed of player while crouch")]
    // Crouch speed of player
    [SerializeField] private float crouchSpeedModifier = 0.5f;

    [Tooltip("Vertical power of player")]
    // Vertical Movement Power Of Player
    [SerializeField] private float verticalPower = 150;
    
    #endregion


    #region Constant Variables

    // GroundCheck GameOject Radius
    const float GROUND_CHECK_RADIUS = 0.2f;

    // OverheadCheck GameOject Radius
    const float OVERHEAD_CHECK_RADIUS = 0.2f;

    #endregion


    #region Boolean Variables

    //Determine If Player Is Facing Right 
    private bool isFacingRight = true;

    //Determine If Player Is Running
    private bool isRunning = false;

    //Determine If Player Is Jumping;
    private bool jump = false;

    //Determine If Player Is Grounded 
    private bool isGrounded = false;

    //Determine if Player is crouched button is pressed/held
    private bool crouchedPressed = false;

    #endregion


    // Awake is called before the start of application
    void Awake()
    {
        // Reference to player's rigidBody 2D
        rb = GetComponent<Rigidbody2D>();

        //Reference to player's Animator
        animator = GetComponent<Animator>();
    }

    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
     void Update()
    {
        //Set the yVelocity in the animator 
        animator.SetFloat("yVelocity",rb.velocity.y);
 
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


        //If jump button is pressed/helded jump is enable
        if (Input.GetButtonDown("Jump"))
        {
            jump  = true;
             animator.SetBool("Jump",true);
        }

        //If jump button is released jump is disable
        else if (Input.GetButtonUp("Jump"))
        {
            jump = false;
        }


        //If Crouch button is pressed/helded Crouch is enable
        if (Input.GetButtonDown("Crouch"))
        {
             crouchedPressed = true;
        }

        //If Crouch button is released Crouch is disable
        else if (Input.GetButtonUp("Crouch"))
        {
            crouchedPressed = false;
        }
    }


    // FixedUpdate is called once every time step is settled
    void FixedUpdate() 
    {
        GroundCheck();
        move(horizontalMovement,jump,crouchedPressed);
    }



/* 
    Method Name: groundCheck() 
    Description: Check If The GroundCheckObject Is Colliding With Other, 
    2D Collider That Are In The "Ground" Layer If Yes (isGrounded true) Else (isGrounded false)
*/  
    
    void GroundCheck()

     {
        isGrounded = false;


        // Detect if player is touching the ground
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckCollider.position,GROUND_CHECK_RADIUS,jumpableGround);
        if(colliders.Length > 0)
        {
            isGrounded = true;
        }

        //As long as we are grounded the "Jump" Bool;
        //In the animator is disable 
        animator.SetBool("Jump",!isGrounded);
     } 

/* 
    Method Name: move()
    Parameter: 
    float dir - Value associated with direction of player in regards to x-axis 
    bool jumpFlag - Value assocaited with weather player is able to Jump or Not
    bool crouchFlag - Value assocaited with Weather Player is crouching or not
    Description: Manipulates Movement For Player
*/     
    void move(float dir, bool jumpFlag, bool crouchFlag)
    {

        #region Jump & Crouch




        //If we are crouching and disable crouching
        //check overhead for collision with ground items
        //If there are any, remain crouch, otherwise un-Crouch
        if(!crouchFlag)
        {
            //Detect if player has anything overhead
            if(Physics2D.OverlapCircle(overheadCheckCollider.position,OVERHEAD_CHECK_RADIUS,jumpableGround))
            {
                crouchFlag = true;
            }
    
        }

        //If Crouch Is Pressed Disable The Standing Collider + Enable animate crouching + Reduce Speed
        //If Crouch Is Released Resume Orginial Speed + Enable The Standing Collider + Enable Crouch Animation
        if(isGrounded)
        {
            // Set StandingCollider to Negation Of CrouchFlag
            standingCollider.enabled = !crouchFlag;
    
            //If The Player Is Grounded And Pressed `space` Jump
            if(jumpFlag)
            {
                
                jumpFlag = false;
                //Add Jump Force
                rb.AddForce(new Vector2(0f,verticalPower));
            }
        }
         
        animator.SetBool("Crouch", crouchFlag);
     
        #endregion
     

        #region Horizontal Movement & Run
        
        // Horizontal movement for Player

        //Set value of player x-axis using dir and speed
        float xVal = dir * speed * 100 * Time.fixedDeltaTime;

        // If player is running multiply with running modifier
        if(isRunning)
        {
            xVal *= runSpeedModifer;
        }


        //If crouching mulitply with the crouch modifier
        if(crouchFlag)
        {
            xVal *= crouchSpeedModifier;
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
        
        #endregion
    
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


