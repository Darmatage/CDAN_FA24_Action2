using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerJump : MonoBehaviour{

    private Animator anim;
    public Rigidbody2D rb;
    public float jumpForce = 20f;
    public Transform feet;
    public LayerMask groundLayer;
    public LayerMask enemyLayer;
    public bool canJump = false;
    public int jumpTimes = 0;
    public bool isAlive = true;
    public AudioSource sfx_jump;

	public bool isGroundedCheck = false;

	public bool isUnderground = true;
	private float depthY;
	private bool beenGrounded = false;

    void Start()
    {
        anim = gameObject.GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
		if (SceneManager.GetActiveScene().name == "Level1"){
			isUnderground = false;
		}
    }

    void Update()
    {
        if ((IsGrounded()) || (jumpTimes <= 1))
        {
            // if ((IsGrounded()) && (jumpTimes <= 1)){ // for single jump only
            canJump = true;
        }
        else if (jumpTimes > 1)
        {
            // else { // for single jump only
            canJump = false;
        }

        if ((Input.GetButtonDown("Jump")) && (canJump) && (isAlive == true))
        {
            Jump();
        }

		//set velocity =0 if on the groudn and not intending to move:
		else if ((IsGrounded()) && (Input.GetAxis("Horizontal") == 0)){
			rb.velocity = new Vector2(0,rb.velocity.y);
		}
    }

    public void Jump(){
        jumpTimes += 1;
        rb.velocity = Vector2.up * jumpForce;
		//to decide between climbing and jumping
		if (isUnderground){anim.SetBool("isUnderground", true);}
		else {anim.SetBool("isUnderground", false);}

		if (Input.GetAxis("Horizontal") == 0){
			anim.SetBool("isUpOnly", true);
		}
		else {
			anim.SetBool("isUpOnly", false);
		}

		anim.SetTrigger("Jump");
         sfx_jump.Play();

        //Vector2 movement = new Vector2(rb.velocity.x, jumpForce);
        //rb.velocity = movement;
    }

    public bool IsGrounded()
    {
        Collider2D groundCheck = Physics2D.OverlapCircle(feet.position, 0.1f, groundLayer);
        Collider2D enemyCheck = Physics2D.OverlapCircle(feet.position, 0.1f, enemyLayer);
        if ((groundCheck != null) || (enemyCheck != null))
        {
            //Debug.Log("I am trouching ground!");
            jumpTimes = 0;
			isGroundedCheck = true;

			if (!beenGrounded){
				beenGrounded=true;
				depthY=transform.position.y;
			}
			if (transform.position.y < (depthY-1)){
				isUnderground = true;
			}

            return true;
        }
		isGroundedCheck = false;
        return false;
    }

	
    //NOTE: to help see the attack sphere in editor:
    void OnDrawGizmosSelected()
    {
        if (feet == null) { return; }
        Gizmos.DrawWireSphere(feet.position, 0.1f);
    }
}