using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerMove : MonoBehaviour{

    private Animator animator;
    private Rigidbody2D rb2D;
    public bool FaceRight = false; // determine which way player is facing.
    public static float runSpeed = 10f;
    public float startSpeed = 10f;
    public bool isAlive = true;
    //public AudioSource WalkSFX;
    private Vector3 hMove;

	public GameObject dashCloudVFX;
	public Transform thoughtBubbleCtrl;
	private Vector3 thoughtBubblePos; 

    void Start(){	
        animator = gameObject.GetComponentInChildren<Animator>();
        rb2D = transform.GetComponent<Rigidbody2D>();
		runSpeed = startSpeed;
		dashCloudVFX.SetActive(false);
		thoughtBubblePos = thoughtBubbleCtrl.localPosition;
    }

    void Update(){
        //NOTE: Horizontal axis: [a] / left arrow is -1, [d] / right arrow is 1
        hMove = new Vector3(Input.GetAxis("Horizontal"), 0.0f, 0.0f);
        if (isAlive == true){
            transform.position = transform.position + hMove * runSpeed * Time.deltaTime;

            if (Input.GetAxis("Horizontal") != 0){
                       animator.SetBool ("Walk", true);
                //       if (!WalkSFX.isPlaying){
                //             WalkSFX.Play();
                //      }
            }
            else{
                     animator.SetBool ("Walk", false);
                //      WalkSFX.Stop();
            }

            // Turning: Reverse if input is moving the Player right and Player faces left
            if ((hMove.x < 0 && !FaceRight) || (hMove.x > 0 && FaceRight)){
                playerTurn();
            }

			//DASHING:
			if ((Input.GetKey("left shift"))||(Input.GetKey("right shift"))){
				Debug.Log("I am trying to dash");
				runSpeed = startSpeed *2;
				GameObject.FindWithTag("GameHandler").GetComponent<DigEnergyMeter>().isDashing=true;
				dashCloudVFX.SetActive(true);
			}
			if ((Input.GetKeyUp("left shift"))||(Input.GetKeyUp("right shift"))){
				runSpeed = startSpeed;
				GameObject.FindWithTag("GameHandler").GetComponent<DigEnergyMeter>().isDashing=false;
				dashCloudVFX.SetActive(false);
			}
        }
    }

    void FixedUpdate()
    {
        //slow down on hills / stops sliding from velocity
        if (hMove.x == 0)
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x / 1.1f, rb2D.velocity.y);
        }
    }

    private void playerTurn()
    {
        // NOTE: Switch player facing label
        FaceRight = !FaceRight;

        // NOTE: Multiply player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;

		//thought bubble always stay in one direction:
		if (FaceRight){
			thoughtBubbleCtrl.localScale = new Vector3(-1,1,1);
			thoughtBubbleCtrl.localPosition = new Vector3(thoughtBubblePos.x * -1, thoughtBubblePos.y, 0);
		} else {
			thoughtBubbleCtrl.localScale = new Vector3(1,1,1);
			thoughtBubbleCtrl.localPosition = thoughtBubblePos;
		}
    }
}