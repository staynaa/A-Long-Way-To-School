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
    [SerializeField] private float verticalPower = 6;

    [Header("Stamina Variables")]

    
    [Tooltip("Maximum Stamina")]
    //Maximum stamina value
    [SerializeField] private float maxStamina = 100;

    [Tooltip("Current Stamina")]
    //Player current stamina
    [SerializeField] private float currentStamina;


    [Tooltip("Stamina Use Multiplier")]
    //Value at which player stamina depletes at when in use  
    [SerializeField] private float staminaUseMultiplier = 5;

    [Tooltip("Stamina Regen Start Timer")]
    //Time before stamina regeneration starts
    [SerializeField] private float timeBeforeStaminaRegenStarts = 5;


    [Tooltip("Stamina Regen Value")]
    //Value at which player player stamina regenerate at when not in use
    [SerializeField] private float staminaValueIncrement = 2;


    [Tooltip("Stamina Regen per frame delay timer")]
    //Time delay between each frame at which stamina regenerate at
    [SerializeField] private float staminaTimeIncrement = 0.1f;
    
    [SerializeField] private Coroutine regeneratingStamina;

    #endregion

    #region Sound Effects
    //Jump Sound
    [SerializeField] private AudioSource jumpSoundEffect;
    //Running Sound
    [SerializeField] private AudioSource runSoundEffect;
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

    //Determine If Player Is Grounded 
    private bool isGrounded = false;

    //Determine If Player Is Coyote Jumping
    bool coyoteJump;

    //Determine if Player is crouched button is pressed/held
    private bool crouchedPressed = false;

    //Determine if player is using stamina 
    private bool useStamina = true;

    //Determine if player is able to run
    private bool canRun = true;

    #endregion


    // Awake is called before the start of application
    void Awake()
    {
        // Reference to player's rigidBody 2D
        rb = GetComponent<Rigidbody2D>();

        //Reference to player's Animator
        animator = GetComponent<Animator>();

        //Initialize player starting stamina
        currentStamina = maxStamina;
    }


    // Start is called before the first frame update
    void Start()
    {

    }


    // Update is called once per frame
     void Update()
    {
        //Locks players movement 
        if (CanMove() == false)
        {
            return;
        }

        //Set the yVelocity in the animator 
        animator.SetFloat("yVelocity",rb.velocity.y);
 
        // Store value given by the key presses of button associated horinzontal movement
        horizontalMovement = Input.GetAxisRaw("Horizontal");


        if(useStamina)
        {
            HandleStamina();
        }

        
        // If left-shift is clicked and held down enable isRunning 
        if(Input.GetKeyDown(KeyCode.LeftShift) && canRun)
        {
            isRunning = true;
            runSoundEffect.Play();
            
        }

        // If left-shift is released disable isRunning 
        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            isRunning = false;
            runSoundEffect.Stop();
        }

        //If jump button is pressed/helded jump is enable
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
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
        move(horizontalMovement,crouchedPressed);
    }
    
    /* 
    Method Name: CanMove() 
    Description: Checks if player should be able to move or not.
    */ 
    bool CanMove()
    {
        bool can = true;
        if(FindObjectOfType<InteractionSystem>().isExamining)
        {
            can = false;
        }

        if(FindObjectOfType<InventorySystem>().isOpen)
        {
            can = false;
        }
        
        return can;
    }


    /* 
    Method Name: groundCheck() 
    Description: Check If The GroundCheckObject Is Colliding With Other, 
    2D Collider That Are In The "Ground" Layer If Yes (isGrounded true) Else (isGrounded false)
    */  
    
    void GroundCheck()

     {  
        // Track previous state of isGrounded Variable 
        bool wasGrounded = isGrounded;
        isGrounded = false;


        // Detect if player is touching the ground
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckCollider.position,GROUND_CHECK_RADIUS,jumpableGround);
        if(colliders.Length > 0)
        {
            isGrounded = true;

            //Detect landing of play *Double jump placeholder*
            if(!wasGrounded)
            {

            }
        }
        
        else
        {
            if(wasGrounded)
            {
                StartCoroutine(CoyoteJumpDelay()); 
            }
        }

        //As long as we are grounded the "Jump" Bool;
        //In the animator is disable 
        animator.SetBool("Jump",!isGrounded);
     } 

    
    
    // Timer for coyote jump to be enable
     IEnumerator CoyoteJumpDelay()
     {
        coyoteJump = true;
        yield return new WaitForSeconds(0.2f);
        coyoteJump = false;
     }

    
    /* 
    Method Name: Jump()
    Description: Determine if player is grounded if player is 
    grounded allow them to jump if player is not grounded disbales jumping
    */
    void Jump()
    {
        //If Crouch Is Pressed Disable The Standing Collider + Enable animate crouching + Reduce Speed
        //If Crouch Is Released Resume Orginial Speed + Enable The Standing Collider + Enable Crouch Animation
          if(isGrounded)
        {
            rb.velocity = Vector2.up * verticalPower; 
            animator.SetBool("Jump",true);
            jumpSoundEffect.Play();
        }
        else
        {
            if(coyoteJump)
            {
            rb.velocity = Vector2.up * verticalPower; 
            animator.SetBool("Jump",true);
            }
        }
    }


    /* 
    Method Name: move()
    Parameter: 
    float dir - Value associated with direction of player in regards to x-axis 
    bool crouchFlag - Value assocaited with Weather Player is crouching or not
    Description: Manipulates Movement For Player
    */     
    void move(float dir,bool crouchFlag)
    {

        #region Crouch

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
        //Set StandingCollider to Negation Of CrouchFlag
        standingCollider.enabled = !crouchFlag;

        animator.SetBool("Crouch", crouchFlag);
       
        #endregion
     

        #region Horizontal Movement & Run
        
        // Horizontal movement for Player

        //Set value of player x-axis using dir and speed
        float xVal = dir * speed * 100 * Time.fixedDeltaTime;

        // If player is running multiply with running modifier
        if(isRunning && canRun)
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


    /* 
    Method Name: HandleStamina()
    Description: Handle Stamina 
    */  
    private void HandleStamina()
    {
        //Determine if player is moving while running 
        if(isRunning && horizontalMovement != 0)
        {
            
            //Determine if player is regenerating stamina
            if(regeneratingStamina != null)
            {
                StopCoroutine(regeneratingStamina);
                regeneratingStamina = null;
            }
            //Depletes current stamina 
            currentStamina -= staminaUseMultiplier * Time.deltaTime;

            if(currentStamina < 0)
            {
                currentStamina = 0;
            }
            if(currentStamina <= 0)
            {
                canRun = false;
            }
            
        }
        //Determine if player is not running and stamina has been used
        if(!isRunning && currentStamina < maxStamina && regeneratingStamina == null)
            {
                //Start stamina regeneration process
                regeneratingStamina = StartCoroutine(RegenerateStamina());
            }
    }


    // Handle the stamina regeneration of player
    private IEnumerator RegenerateStamina()
    {
        yield return new WaitForSeconds(timeBeforeStaminaRegenStarts);
        WaitForSeconds timeToWait = new WaitForSeconds(staminaTimeIncrement);

        while(currentStamina < maxStamina)
        {
            if(currentStamina > 0)
            {
                canRun = true;
            }
            currentStamina += staminaValueIncrement * Time.deltaTime;

            if(currentStamina > maxStamina)
            {
                currentStamina = maxStamina;
            }
            yield return timeToWait;
        }
        regeneratingStamina = null;
    }
}  



