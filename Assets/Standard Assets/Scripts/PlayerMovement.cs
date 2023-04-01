using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Vector2 moveInput;
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    float gravityScaleAtStart;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeetCollider;
    [SerializeField] int speed = 5;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] Vector2 deathKick = new Vector2 (5.0f, 8.0f);
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;
    bool isAlive = true;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = myRigidbody.gravityScale;
    }

    void Update()
    {
        if (!isAlive) {return;}
        Run();
        FlipSprite();
        ClimbLadder();
        Die();
    }

    void OnFire(InputValue value)
    {
        if (!isAlive) {return;}
        Instantiate(bullet, gun.position, transform.rotation);
    }
    void OnMove(InputValue value)
    {
        if (!isAlive) {return;}
        moveInput = value.Get<Vector2>();
        // Debug.Log(moveInput);
    }

    void OnJump(InputValue value)
    {
        if(!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) {return;}
        
        if(value.isPressed)
        {
           myRigidbody.velocity += new Vector2(0f, jumpSpeed);
        }

    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * speed, myRigidbody.velocity.y);
        //Debug.Log(myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;

        myAnimator.SetBool("isRunning", playerHasHorizontalSpeed);


        

    }

    void FlipSprite()
    {
        //Mathf.Abs = Absolute value , Epsilon = A tiny flaoting value[to compare to 0 and -1,1]
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;

        if (playerHasHorizontalSpeed)
        {
            //Sign = returns the sign of value
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x), 1f);

        }

    }

    void ClimbLadder()
    {
        
        if(!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing"))) 
        {
            myRigidbody.gravityScale = gravityScaleAtStart;
            myAnimator.SetBool("isClimbing", false);
            return;
        }

        Vector2 climbVelocity = new Vector2(myRigidbody.velocity.x, moveInput.y * climbSpeed);

        //(moveInput.x * speed, myRigidbody.velocity.y)
        //Debug.Log(myRigidbody.velocity.y);
        myRigidbody.velocity = climbVelocity;
        myRigidbody.gravityScale = 0f;
        
        bool playerHasVerticalSpeed = Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("isClimbing", playerHasVerticalSpeed);

    }

    void Die() 
    {
        //if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemies", "Hazards")))
       if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemies")))
       {
            isAlive = false;
            myAnimator.SetTrigger("Dying");
            myRigidbody.velocity += deathKick;
            FindObjectOfType<GameSession2>().ProcessPlayerDeath();
       }

        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Hazards")))
       {
            isAlive = false;
            myAnimator.SetTrigger("Dying");
            myRigidbody.velocity += deathKick;
           FindObjectOfType<GameSession2>().ProcessPlayerDeath();

       }  
    }

}
