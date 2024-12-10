using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolSwarm : MonoBehaviour{

	public GameObject antPrefab;
	public Transform spawnPoint;
	public Transform[] movePoints;

	//for testing:
	public bool justOne = true; 

	//timer:
	private float theTimer = 0;
	private float timeToSpawn = 0.8f;

	void Start(){
		spawnPoint = movePoints[0];
		//antPrefab.transform.position = spawnPoint.position;

	}

//timer
    void FixedUpdate(){
		theTimer += 0.01f;
		if (theTimer >= timeToSpawn){
			theTimer = 0;
			//if (justOne == true){
				SpawnAnt();
			//	justOne = false; 
			//}
		}
    }

	public void SpawnAnt(){
		GameObject newAnt = Instantiate(antPrefab, spawnPoint.position, Quaternion.identity);
		newAnt.GetComponent<NPC_PatrolSequencePoints>().isAnt = true;
		newAnt.GetComponent<NPC_PatrolSequencePoints>().moveSpots = movePoints;
	}

}
