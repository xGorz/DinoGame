using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private Animator animator;                      //following declared variables for collisionobjects from Player
    private SpriteRenderer sr;
    private Rigidbody2D rb;

    [Range(1,10)]                                   //following declared variables for speed customization
    public float speed;                             //x-axis
    [Range(1, 10)] 
    public float jumpSpeed;                         //y-axis

    public bool facingRight = true;                 //following declared variables for different states
    public bool isJumping = false;
    private float jumpButtonPressTime;              // How long is the jump button held
    public float  maxJumpTime = 0.2f;               //Max Jump amount

    private float rayCastLength = 0.005f;           //following declared variables for Raycastingcollision
    private float width;                            // Sprite width and height
    private float height;
    public float toggleDown;

    //without a public void Start() because we use Awake()
	
	// Update is called once per frame
	void FixedUpdate () {
        float horzMove = Input.GetAxisRaw("Horizontal");
        float vertMove = Input.GetAxis("Jump");

        //var playerObject = GameObject.Find("Player");

        Vector2 vector = rb.velocity;               //Vector of Player
        rb.velocity = new Vector2(horzMove * speed, vector.y);

        if(rb.velocity != new Vector2(0, 0))
        {
            animator.SetBool("Walking", true);
        }
        else if(rb.velocity == new Vector2(0,0))
        {
            animator.SetBool("Walking", false);
        }
        if (horzMove > 0 && !facingRight)
        {
            Flip();                            //when facing left, flip character
        }
        else if (horzMove < 0 && facingRight)
        {
            Flip();
        }        

        if (IsOnGround() && isJumping == false)
        {
            if (vertMove > 0f)
            {
                isJumping = true;
            }
        }

        //button is held pass max time set vertical move to 0
        if (jumpButtonPressTime > maxJumpTime)
        {
            vertMove = 0f;
        }

        //jumping and a valid jump press length
        if (isJumping && (jumpButtonPressTime < maxJumpTime))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed - toggleDown); //here
        }

        //moved high enough to let Player falling
        // Set Players Rigidbody 2d Gravity Scale to 2
        if (vertMove >= 1f)
        {
            jumpButtonPressTime += Time.deltaTime;
        }
        else
        {
            isJumping = false;
            jumpButtonPressTime = 0f;
        }

    }

   
    void Awake()
    {   //standard declaring
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        //Raycast declared variables
        width = GetComponent<Collider2D>().bounds.extents.x + 0.1f;
        height = GetComponent<Collider2D>().bounds.extents.y + 0.2f;
    }

    // When moving in a direction face in that direction
    void Flip()
    {
        // Flip the facing value
        facingRight = !facingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    public bool IsOnGround()
    {

        // Check if contacting the ground straight down
        bool groundCheck1 = Physics2D.Raycast(new Vector2(
                                transform.position.x,
                                transform.position.y - height),
                                -Vector2.up, rayCastLength);

        // Check if contacting ground to the right
        bool groundCheck2 = Physics2D.Raycast(new Vector2(
            transform.position.x + (width - 0.2f),
            transform.position.y - height),
            -Vector2.up, rayCastLength);

        // Check if contacting ground to the left
        bool groundCheck3 = Physics2D.Raycast(new Vector2(
            transform.position.x - (width - 0.2f),
            transform.position.y - height),
            -Vector2.up, rayCastLength);

        if (groundCheck1 || groundCheck2 || groundCheck3)
            return true;

        return false;

    }
}
