using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NPCMonologueManager : MonoBehaviour {

	public GameObject monologueBox;
	public TMP_Text monologueText;
	public string[] monologue;
	public int counter = 0;
	public int monologueLength;
	
	//code to let the family member exit
	public NPCMonologue familyMember;

	void Start(){
		monologueBox.SetActive(false);
		monologueLength = monologue.Length; //allows us test dialogue without an NPC
	}

	void Update(){
		//temporary testing before NPC is created
		if (Input.GetKeyDown("o")){
			monologueBox.SetActive(true);
		}
		if (Input.GetKeyDown("p")){
			monologueBox.SetActive(false);
			monologueText.text = "..."; //reset text
			counter = 0; //reset counter
		}
	}

	public void OpenMonologue(){
		monologueBox.SetActive(true);
 
		//auto-loads the first line of monologue
		monologueText.text = monologue[0];
		counter = 1;
	}

	public void CloseMonologue(){
		monologueBox.SetActive(false);
		monologueText.text = "..."; //reset text
		counter = 0; //reset counter
	}

	public void LoadMonologueArray(string[] NPCscript, int scriptLength){
		monologue = NPCscript;
		monologueLength = scriptLength;
	}

        //function for the button to display next line of dialogue
	public void MonologueNext(){
		if (counter < monologueLength){
			monologueText.text = monologue[counter];
			counter +=1;
		}
		//when lines are complete:
		else { 
			CloseMonologue();
			familyMember.DigActivate();
			
		}
	}

}