using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniffScent : MonoBehaviour{

	private SnifferPlayer sniffPlayer;
	private float timeToExist = 3f;
	public Vector3 destination;

	//fade away variables:
	bool fadeaway = false;
	SpriteRenderer spriteRend;
	Color spriteColor;
	float spriteOpacity;

	void Start(){
		sniffPlayer = GameObject.FindWithTag("Player").GetComponent<SnifferPlayer>();
		spriteRend = GetComponentInChildren<SpriteRenderer>();
		spriteColor = spriteRend.color;
		spriteOpacity = spriteColor.a;
		StartCoroutine(DeleteScent());
    }

	//Head toward player a bit
    void FixedUpdate(){
		//head slowly toward previous player position, destination
		transform.position = Vector3.MoveTowards(transform.position, destination, 1f * Time.fixedDeltaTime);

		//fade-away:
		if (fadeaway){
			spriteOpacity -= 0.01f;
			spriteRend.color = new Color(spriteColor.r, spriteColor.g, spriteColor.b, spriteOpacity);
		}
    }

	//wait for player to run into it, and display thought bubble with family member image in mask?
	public void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Player"){
			sniffPlayer.ShowThoughts();
		}
	}

	IEnumerator DeleteScent(){
		yield return new WaitForSeconds (timeToExist);
		fadeaway = true;
		yield return new WaitForSeconds (0.65f);
		Destroy(gameObject);
	}
}