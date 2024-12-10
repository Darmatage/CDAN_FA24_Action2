using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DestroyTiles : MonoBehaviour{
	public Animator anim;
	private GameObject gameHandlerObj;
	public Tilemap destructableTilemap;
	private List<Vector3> tileWorldLocations;
	public float rangeDestroyUp = 1f;
	public float rangeDestroyDown = 2f;
	   
	public bool canExplode = true;
	public GameObject digParticlesFX;

	public Transform digPointUp; 
	public Transform digPointDown; 

	void Start(){
		TileMapInit();
		gameHandlerObj = GameObject.FindWithTag("GameHandler");
		anim = GetComponentInChildren<Animator>();
	}

	void Update(){
		if (gameHandlerObj.GetComponent<DigEnergyMeter>().canDig==true){
			if (Input.GetButtonDown("DigUp")){
				destroyTileAreaUp();
				StopCoroutine(PauseMove());
				StartCoroutine(PauseMove());
			}
			if (Input.GetButtonDown("DigDown")){
				destroyTileAreaDown();
				StopCoroutine(PauseMove());
				StartCoroutine(PauseMove());
			}
		}
	}

       public void TileMapInit(){
              tileWorldLocations = new List<Vector3>();

              foreach (var pos in destructableTilemap.cellBounds.allPositionsWithin){
                     Vector3Int localPlace = new Vector3Int(pos.x, pos.y, pos.z);
                     Vector3 place = destructableTilemap.CellToWorld(localPlace) + new Vector3(0.5f, 0.5f, 0f);

                     if (destructableTilemap.HasTile(localPlace)){
                            tileWorldLocations.Add(place);
                     }
              }
       }

       void destroyTileAreaUp(){
             foreach (Vector3 tile in tileWorldLocations){
                     if (Vector2.Distance(tile, digPointUp.position) <= rangeDestroyUp){
                            //Debug.Log("in range");
                            Vector3Int localPlace = destructableTilemap.WorldToCell(tile);
                            if (destructableTilemap.HasTile(localPlace)){
								//anim.SetTrigger(digUp);
                                   StartCoroutine(DigVFX(tile));
								   gameHandlerObj.GetComponent<DigEnergyMeter>().ReduceEnergyDig();
                                   destructableTilemap.SetTile(destructableTilemap.WorldToCell(tile), null);
                            }
                     //tileWorldLocations.Remove(tile);
                     }
              }
       }

       void destroyTileAreaDown(){
             foreach (Vector3 tile in tileWorldLocations){
                     if (Vector2.Distance(tile, digPointDown.position) <= rangeDestroyDown){
                            //Debug.Log("in range");
                            Vector3Int localPlace = destructableTilemap.WorldToCell(tile);
                            if (destructableTilemap.HasTile(localPlace)){
								//anim.SetTrigger(digDown);
                                   StartCoroutine(DigVFX(tile));
								   gameHandlerObj.GetComponent<DigEnergyMeter>().ReduceEnergyDig();
                                   destructableTilemap.SetTile(destructableTilemap.WorldToCell(tile), null);
                            }
                     //tileWorldLocations.Remove(tile);
                     }
              }
       }

		IEnumerator PauseMove(){
			gameObject.GetComponent<PlayerMove>().isDigging = true;
			yield return new WaitForSeconds(0.2f);
			gameObject.GetComponent<PlayerMove>().isDigging = false;
		}

       IEnumerator DigVFX(Vector3 tilePos){
			Vector3 tilePosPlus = new Vector3(tilePos.x, tilePos.y, tilePos.z -0.5f);
              GameObject tempVFX = Instantiate(digParticlesFX, tilePosPlus, Quaternion.identity);
              yield return new WaitForSeconds(1f);
              Destroy(tempVFX);
       }

       //NOTE: To help see the attack sphere in editor:
       void OnDrawGizmosSelected(){
              Gizmos.DrawWireSphere(digPointUp.position, rangeDestroyUp);
			  Gizmos.DrawWireSphere(digPointDown.position, rangeDestroyDown);
       }
}

