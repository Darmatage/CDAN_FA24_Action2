using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniffScent : MonoBehaviour{

	private float timeToExist = 3.0f;
	public float moveSpeed = 1f;
	public Vector3 destination;
	public int familyNumber;

	//fade away variables:
	bool fadeaway = false;
	SpriteRenderer spriteRend;
	Color spriteColor;
	float spriteOpacity;

	void Start(){
		spriteRend = GetComponentInChildren<SpriteRenderer>();
		spriteColor = spriteRend.color;
		spriteOpacity = spriteColor.a;
		StartCoroutine(DeleteScent());
    }

	//Head toward player a bit
    void FixedUpdate(){
		//head slowly toward previous player position, destination
		transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.fixedDeltaTime);

		//fade-away:
		if (fadeaway){
			spriteOpacity -= 0.01f;
			spriteRend.color = new Color(spriteColor.r, spriteColor.g, spriteColor.b, spriteOpacity);
		}
    }

	//wait for player to run into it, and display thought bubble with family member image in mask?
	public void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Player"){
			other.gameObject.GetComponent<SnifferPlayer>().ShowThoughts(familyNumber);
		}
	}

	IEnumerator DeleteScent(){
		yield return new WaitForSeconds (timeToExist);
		fadeaway = true;
		yield return new WaitForSeconds (0.65f);
		Destroy(gameObject);
	}

	public void SetFamilyNumber(int familyNum){
		familyNumber = familyNum;
		if (familyNum > 0){
			spriteColor.r = 2.0f + Random.Range(0, 0.5f);
			spriteColor.g = 2.0f + Random.Range(0, 0.5f);
			spriteColor.b = 2.0f + Random.Range(0, 0.5f);
		}
	}

}