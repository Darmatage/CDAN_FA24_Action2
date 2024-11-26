using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordManager : MonoBehaviour{
	GameObject player;

	public static bool hasSword = false;
	public int swordLife = 5;

	 void Start(){
		if (GameObject.FindWithTag("Player") != null){
			player = GameObject.FindWithTag("Player");
	 	}
	 }

//manage sword aquisition
	public void PlayerGetSword(){
		hasSword = true;
		swordLife = 5;
		player.GetComponentInChildren<Animator>().SetBool("hasSword", true);
	}

//manage sword damage (random chance on each hit of cause=ing sword damage)
	public void PlayerSwordHit(){
		int randNum = Random.Range(0,10);
		if (randNum > 4){
			swordLife -= 1;
			Debug.Log("Your sword got 1 pt damage.");
			if (swordLife <= 0){
				PlayerLoseSword();
				
			} 
		}
		else {
			Debug.Log("Your sword sustained no damage!");
		}
	}

//manage sword loss:
	public void PlayerLoseSword(){
		hasSword = false;
		swordLife = 0;
		player.GetComponentInChildren<Animator>().SetBool("hasSword", false);
	}


}
