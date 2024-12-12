using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class NPCMonologue : MonoBehaviour {
	private Animator anim;
	private NPCMonologueManager monologueMNGR;
	public string[] monologue; //enter monologue lines into the inspector for each NPC
	public bool playerInRange = false; //could be used to display an image: hit [e] to talk
	public int monologueLength;

	public bool digLeave = true;
	public bool isBunny = false;

	void Start(){
		anim = gameObject.GetComponentInChildren<Animator>();
		monologueLength = monologue.Length;
		if (GameObject.FindWithTag("MonologueManager")!= null){
			monologueMNGR = GameObject.FindWithTag("MonologueManager").GetComponent<NPCMonologueManager>();
		}
		anim.SetBool("isDirt", true);
	}

	private void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Player") {
			playerInRange = true;
			if (isBunny){
				other.gameObject.GetComponent<SnifferPlayer>().canSniff = true;
			}
			StartCoroutine(DigArrive());
			/*
			monologueMNGR.LoadMonologueArray(monologue, monologueLength);
			monologueMNGR.OpenMonologue();
			monologueMNGR.familyMember = this;
			if (isBunny){
				other.gameObject.GetComponent<SnifferPlayer>().canSniff = true;
			}
			*/
			//anim.SetBool("Chat", true);
			//Debug.Log("Player in range");
		}
	}

	IEnumerator DigArrive(){
		if (isBunny){
			anim.SetBool("isDirt", false);
			anim.SetTrigger("digIn");
			yield return new WaitForSeconds(1f);
		}

		monologueMNGR.LoadMonologueArray(monologue, monologueLength);
		monologueMNGR.OpenMonologue();
		monologueMNGR.familyMember = this;
		yield return new WaitForSeconds(0.1f);
		
	}

	private void OnTriggerExit2D(Collider2D other){
		if (other.gameObject.tag =="Player") {
			playerInRange = false;
			monologueMNGR.CloseMonologue();
			//anim.SetBool("Chat", false);
			//Debug.Log("Player left range");
			StartCoroutine(DigAndLeave());
		}
	}



	//family NPCs:
	public void DigActivate(){
		if (digLeave == true){
			gameObject.tag = "Untagged";
			GameObject.FindWithTag("Player").GetComponent<SnifferPlayer>().FindFamily();
			StartCoroutine(DigAndLeave());
		}
	}

	IEnumerator DigAndLeave(){
		yield return new WaitForSeconds(1f);
		anim.SetTrigger("Dig");
		anim.SetBool("isDirt", true);
		yield return new WaitForSeconds(2f);

		//Destroy(gameObject); 
	}


}