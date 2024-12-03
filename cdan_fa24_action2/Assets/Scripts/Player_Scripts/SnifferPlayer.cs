using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnifferPlayer : MonoBehaviour{
	private GameObject familyMember;
	public GameObject scentPrefab;
	public GameObject thoughtBubble, bubble1, bubble2, bubble3;
	public SpriteRenderer thoughtSprite;

	void Start() {
		familyMember = GameObject.FindWithTag("Family");
		thoughtBubble.SetActive(true);
		thoughtSprite.sprite= familyMember.GetComponentInChildren<SpriteRenderer>().sprite;
		Debug.Log("The Sprite: " + thoughtSprite.sprite);
		thoughtBubble.SetActive(false);
		bubble1.SetActive(false);
		bubble2.SetActive(false);
		bubble3.SetActive(false);
	}

	void Update (){
		//change this to an occasional appearance?
		if (Input.GetButtonDown("Sniff")){
			Debug.Log("I hit sniff");
			SniffTime();
		}
	}

	public void SniffTime(){
		float cameraSize = GameObject.FindWithTag("MainCamera").GetComponent<Camera>().orthographicSize;
		float scentDistance = (cameraSize*2.5f)/100;
		//float scentDistance = 0.08f;
		GameObject theScent = Instantiate(scentPrefab, transform.position, Quaternion.identity);

		//Send scent player position
		theScent.GetComponent<SniffScent>().destination = transform.position;
		//POSITION: get the Lerp at a certain distance from player along the vector
		theScent.transform.position = Vector3.Lerp(transform.position, familyMember.transform.position, scentDistance);
		//ROTATION: Change orientation to the direction of the player position 
		Vector2 direction = (Vector2)familyMember.transform.position - (Vector2)transform.position;
        theScent.transform.rotation = Quaternion.FromToRotation(Vector3.up, direction);
	}

//Display thought bubble on trigger contact with scent animation:
	public void ShowThoughts(){
		StartCoroutine(ThoughtsDisplay());
	}

	IEnumerator ThoughtsDisplay(){

		//show everything:
		bubble1.SetActive(true);
		yield return new WaitForSeconds(0.1f);
		bubble2.SetActive(true);
		yield return new WaitForSeconds(0.1f);
		bubble3.SetActive(true);
		yield return new WaitForSeconds(0.1f);
		thoughtBubble.SetActive(true);

		//full display time:
		yield return new WaitForSeconds(2f);

		//hide everything:
		thoughtBubble.SetActive(false);
		yield return new WaitForSeconds(0.1f);
		bubble3.SetActive(false);
		yield return new WaitForSeconds(0.1f);
		bubble2.SetActive(false);
		yield return new WaitForSeconds(0.1f);
		bubble1.SetActive(false);
	}

}