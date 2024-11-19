using System.Collections.Generic; 
using System.Collections;
using UnityEngine;

public class JoystickMouseAiming : MonoBehaviour { 
	//AIMING: 
	private Rigidbody2D rb; 
	private Camera cam; 
	private Vector2 mousePos;
	private Vector2 mousePosLast; //to test for mouse psoition change

	//web SHOOTING: 
	public GameObject projectilePrefab; 
	public Transform shootPoint; 
	public Transform shootBase; 
	public float projectileSpeed = 10f; 

	//Turning off MouseControl when not in use:
	public bool mouseOn = true;

	void Awake(){ 
	//assign rigidbody2D and camera to variables for AIMING: 
		rb = GetComponent<Rigidbody2D>(); 
		cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>() as Camera; 
	} 

	void Update(){
	//mouse location for AIMING
		mousePos = cam.ScreenToWorldPoint (Input.mousePosition); 
		
	//enable mouse if used
		if (mousePos != mousePosLast){mouseOn = true;} 
		mousePosLast = mousePos;

	//add a player Input listener for SHOOTING:
		if (Input.GetButtonUp("Fire1")){
			Shoot();
		}
	}

//for AIMING: 
	void FixedUpdate(){ 
	//joystick variables: 
		float horizAxis = Input.GetAxis("HorizontalRightStick"); 
		float vertAxis = Input.GetAxis("VerticalRightStick"); 
		float directionMultiplier = 1;
	//if JOYSTICK values are non-zero, rotate using joystick:
		if (horizAxis != 0f || vertAxis != 0f){
			mouseOn = false;
		//see if we are facing right or left:
			if (gameObject.GetComponent<PlayerMove>().FaceRight){
				directionMultiplier = 1;
			}
			else {directionMultiplier = -1;}
		//rotate base by joystick input:
			//if(horizAxis > 0.1f || vertAxis > 0.1f){
			float angle = Mathf.Atan2(horizAxis *directionMultiplier, vertAxis *-1) * Mathf.Rad2Deg;

			//Vector2 lookDir = new Vector2(Input.GetAxis("HorizontalRightStick"), Input.GetAxis("VerticalRightStick")); 
			//if (lookDir.sqrMagnitude > 0.1f){
				shootBase.transform.localEulerAngles = new Vector3(0f, 0f, angle); 
			//}
		} 
	//otherwise, rotate to MOUSE Position:
		else {
			if (mouseOn){
		//actual rotation uses vector math to calculate angle, then rotates firepoint to mouse 
			Vector2 lookDir = mousePos - rb.position;
			float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
		//rb.rotation = angle;
			shootBase.rotation = Quaternion.FromToRotation(Vector3.up, lookDir);
			}
		}
	}


//for SHOOTING:
	void Shoot(){
		Vector2 fwd = (shootPoint.position - shootBase.position).normalized;
		GameObject bullet = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);
		bullet.GetComponent<Rigidbody2D>().AddForce(fwd * projectileSpeed, ForceMode2D.Impulse);
	} 

} 