using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_FoundFamily : MonoBehaviour{

	public DoorOpenable door;

    void Start(){
        door = GameObject.FindWithTag("Door").GetComponent<DoorOpenable>();
    }

/*
//moved to SnifferPlayer, to accomodate multiple family members in a level
    public void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.tag == "Player"){
			door.OpenDoor();
			//Destroy(gameObject);
		}
    }
*/

}
