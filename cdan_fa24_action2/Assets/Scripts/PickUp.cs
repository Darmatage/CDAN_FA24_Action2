using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PickUp : MonoBehaviour{

      private GameHandler gameHandler;
      //public playerVFX playerPowerupVFX;
      public bool isHealthPickUp = true;
      public bool isEnergyPickUp = false;
	  public bool isSword = false;
      public AudioSource sfx_pickupbeep;
      public int healthBoost = 25;
	  public float energyBoost = 2.5f;
      //public float speedBoost = 2f;
      //public float speedTime = 2f;

      void Start(){
            gameHandler = GameObject.FindWithTag("GameHandler").GetComponent<GameHandler>();
            //playerPowerupVFX = GameObject.FindWithTag("Player").GetComponent<playerVFX>();
      }

      public void OnTriggerEnter2D (Collider2D other){
            if (other.gameObject.tag == "Player"){
                  GetComponent<Collider2D>().enabled = false;
                  //GetComponent< AudioSource>().Play();
                  StartCoroutine(DestroyThis());

                  if (isHealthPickUp == true) {
                        gameHandler.playerGetHit(healthBoost * -1);
                //playerPowerupVFX.powerup();
                         sfx_pickupbeep.Play();
            }

				if (isEnergyPickUp == true) {
                        GameObject.FindWithTag("GameHandler").GetComponent<DigEnergyMeter>().AddEnergy(energyBoost);
                //playerPowerupVFX.powerup();
                         sfx_pickupbeep.Play();

            }

            if (isSword == true) {
                        GameObject.FindWithTag("GameHandler").GetComponent<SwordManager>().PlayerGetSword();
                        sfx_pickupbeep.Play();
                //playerPowerupVFX.powerup();
            }

        }
      }

      IEnumerator DestroyThis(){
            yield return new WaitForSeconds(0.3f);
            Destroy(gameObject);
      }

}