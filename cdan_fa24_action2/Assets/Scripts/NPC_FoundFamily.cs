using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_FoundFamily : MonoBehaviour
{
	public DoorOpenable door;


    // Start is called before the first frame update
    void Start(){
        door = GameObject.FindWithTag("Door").GetComponent<DoorOpenable>();
    }

    // Update is called once per frame
    public void OnCollisionEnter2D(Collision2D other){
        if (other.gameObject.tag == "Player"){
			door.OpenDoor();
			Destroy(gameObject);
		}
    }
}
