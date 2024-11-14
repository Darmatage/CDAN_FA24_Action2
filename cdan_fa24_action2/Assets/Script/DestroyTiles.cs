using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DestroyTiles : MonoBehaviour{

       public Tilemap destructableTilemap;
       private List<Vector3> tileWorldLocations;
       public float rangeDestroy = 2f;
       public bool canExplode = true;
       //public GameObject boomFX;

	   public Transform digPointUp; 
	   public Transform digPointDown; 

       void Start(){
              TileMapInit();
       }

       void Update(){
		if (canExplode == true)
              if (Input.GetKeyDown("e")){
                     destroyTileAreaUp();
              }
			  if (Input.GetKeyDown("r")){
                     destroyTileAreaDown();
              }
       }

       void TileMapInit(){
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
                     if (Vector2.Distance(tile, digPointUp.position) <= rangeDestroy){
                            //Debug.Log("in range");
                            Vector3Int localPlace = destructableTilemap.WorldToCell(tile);
                            if (destructableTilemap.HasTile(localPlace)){
                                   //StartCoroutine(BoomVFX(tile));
                                   destructableTilemap.SetTile(destructableTilemap.WorldToCell(tile), null);
                            }
                     //tileWorldLocations.Remove(tile);
                     }
              }
       }

       void destroyTileAreaDown(){
             foreach (Vector3 tile in tileWorldLocations){
                     if (Vector2.Distance(tile, digPointDown.position) <= rangeDestroy){
                            //Debug.Log("in range");
                            Vector3Int localPlace = destructableTilemap.WorldToCell(tile);
                            if (destructableTilemap.HasTile(localPlace)){
                                   //StartCoroutine(BoomVFX(tile));
                                   destructableTilemap.SetTile(destructableTilemap.WorldToCell(tile), null);
                            }
                     //tileWorldLocations.Remove(tile);
                     }
              }
       }

       //IEnumerator BoomVFX(Vector3 tilePos){
              //GameObject tempVFX = Instantiate(boomFX, tilePos, Quaternion.identity);
              //yield return new WaitForSeconds(.5f);
              //Destroy(tempVFX);
       //}

       //NOTE: To help see the attack sphere in editor:
       void OnDrawGizmosSelected(){
              Gizmos.DrawWireSphere(digPointUp.position, rangeDestroy);
			  Gizmos.DrawWireSphere(digPointDown.position, rangeDestroy);
       }
}

