using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_FoundFamily : MonoBehaviour{

	public DoorOpenable door;

    void Start(){
        door = GameObject.FindWithTag("Door").GetComponent<DoorOpenable>();
    }

    public void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.tag == "Player"){
			door.OpenDoor();
			//Destroy(gameObject);
		}
    }
}
