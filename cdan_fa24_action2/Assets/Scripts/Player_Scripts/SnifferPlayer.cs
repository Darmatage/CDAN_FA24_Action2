using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnifferPlayer : MonoBehaviour{
	private GameObject[] familyMember;
	public GameObject scentPrefab;
	public GameObject thoughtBubble, bubble1, bubble2, bubble3;
	public SpriteRenderer thoughtSprite;

	//auto scent:
	public bool scentTimerOn = true;
	float theTimer = 0f;
	public float timeToNextScent = 5f;

	void Start() {
		familyMember = GameObject.FindGameObjectsWithTag("Family");
		//thoughtBubble.SetActive(true);
		//thoughtSprite.sprite= familyMember.GetComponentInChildren<SpriteRenderer>().sprite;
		//Debug.Log("The Sprite: " + thoughtSprite.sprite);
		thoughtBubble.SetActive(false);
		bubble1.SetActive(false);
		bubble2.SetActive(false);
		bubble3.SetActive(false);
	}

	//Player input for sniffing:
	void Update (){
		//change this to an occasional appearance?
		if (Input.GetButtonDown("Sniff")){
			Debug.Log("I hit sniff");
			SniffTime();
		}
	}

	//Automatic scene: 
	void FixedUpdate(){
		if (scentTimerOn){
			theTimer += 0.01f;
			if (theTimer >= timeToNextScent){
				theTimer = 0;
				SniffTime();
			}
		}
	}

//Spawn the scent animation:
	public void SniffTime(){
		StartCoroutine(SniffSpawn());
	}

	IEnumerator SniffSpawn(){
		float cameraSize = GameObject.FindWithTag("MainCamera").GetComponent<Camera>().orthographicSize;
		float scentDistance = (cameraSize*2.5f)/100;
		//float scentDistance = 0.08f;
		for (int i=0; i < familyMember.Length; i++){
			yield return new WaitForSeconds((float)i * 2);
			GameObject theScent = Instantiate(scentPrefab, transform.position, Quaternion.identity);

		//Send scent player position and family number:
			theScent.GetComponent<SniffScent>().destination = transform.position;
			theScent.GetComponent<SniffScent>().SetFamilyNumber(i);
		//POSITION: get the Lerp at a certain distance from player along the vector
			theScent.transform.position = Vector3.Lerp(transform.position, familyMember[i].transform.position, scentDistance);
		//ROTATION: Change orientation to the direction of the player position 
			Vector2 direction = (Vector2)familyMember[i].transform.position - (Vector2)transform.position;
			theScent.transform.rotation = Quaternion.FromToRotation(Vector3.up *-1, direction);
		}
	}

//Display thought bubble on trigger contact with scent animation:
	public void ShowThoughts(int famNum){
		StartCoroutine(ThoughtsDisplay(famNum));
	}

	IEnumerator ThoughtsDisplay(int famNum){

		//show everything:
		bubble1.SetActive(true);
		yield return new WaitForSeconds(0.1f);
		bubble2.SetActive(true);
		yield return new WaitForSeconds(0.1f);
		bubble3.SetActive(true);
		yield return new WaitForSeconds(0.1f);
		thoughtBubble.SetActive(true);
		thoughtSprite.sprite= familyMember[famNum].GetComponentInChildren<SpriteRenderer>().sprite;

		//full display time:
		yield return new WaitForSeconds(4f);

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