using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CreateTiles : MonoBehaviour{
	public Animator anim;
	private GameObject gameHandlerObj;
       public Transform paintPoint;
	   public Tilemap paintTilemap; //a (potentially) empty map to be painted
       public Tilemap rangeTilemap; // the entire space that can be painted
       public RuleTile newTile;
       private List<Vector3> tileWorldLocations;
       public float rangePaint = 0.5f;
       public AudioSource sfx_piling;
       //public bool canPaint = true;
       //public GameObject paintFX;

	void Start(){
		TileMapInit();
		gameHandlerObj = GameObject.FindWithTag("GameHandler");
		anim = GetComponentInChildren<Animator>();
	}

	void Update(){
		if (gameHandlerObj.GetComponent<DigEnergyMeter>().canPile==true){
			if (Input.GetKeyDown("s")){
				CreateTileArea();
                sfx_piling.Play();
            }
		}
	}

       void TileMapInit(){
              tileWorldLocations = new List<Vector3> ();

              foreach (var pos in rangeTilemap.cellBounds.allPositionsWithin){
                     Vector3Int localPlace = new Vector3Int(pos.x, pos.y, pos.z);
                     Vector3 place = rangeTilemap.CellToWorld(localPlace) + new Vector3(0.5f, 0.5f, 0f);

                     if (rangeTilemap.HasTile(localPlace)){
                            tileWorldLocations.Add(place);
                     }
              }
       }

       void CreateTileArea(){
             foreach (Vector3 tile in tileWorldLocations){
                     if (Vector2.Distance(tile, paintPoint.position) <= rangePaint){
                            //Debug.Log("in range");
                            //StartCoroutine(PaintVFX(tile));
							anim.SetTrigger("placeDirt");
                            paintTilemap.SetTile(paintTilemap.WorldToCell(tile), newTile);
							gameHandlerObj.GetComponent<DigEnergyMeter>().ReduceEnergyPile();
							//re-init the destroy tiles script, to include the new tile:
							gameObject.GetComponent<DestroyTiles>().TileMapInit(); 
                     }
              }
       }

       //IEnumerator PaintVFX(Vector3 tilePos){
              //GameObject tempVFX = Instantiate(paintFX, tilePos, Quaternion.identity);
              //yield return new WaitForSeconds(.5f);
              //Destroy(tempVFX);
       //}

       //NOTE: To help see the attack sphere in editor:
       void OnDrawGizmosSelected(){
              Gizmos.DrawWireSphere(paintPoint.position, rangePaint);
       }
}