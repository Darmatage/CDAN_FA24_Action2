using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorOpenable : MonoBehaviour{

	public string NextLevel = "Level2";
	public GameObject doorClosed;
	public GameObject doorOpen;
	
	void Start(){
		doorOpen.SetActive(false);
		doorClosed.SetActive(true);
	}

	public void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Player"){
			SceneManager.LoadScene (NextLevel);
		}
	}

	public void OpenDoor(){
		doorOpen.SetActive(true);
		doorClosed.SetActive(false);
	}


}