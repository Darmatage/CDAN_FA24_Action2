using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DigEnergyMeter : MonoBehaviour{

	public float energyMax = 10f;       //set the number of seconds here (both floats can be static)
	public float digEnergyMinimum = 0.5f;
	public float digEnergy = 0.5f;
	
	public float pileEnergy = 2f;
	public float pileEnergyMinimum = 2f;

	private float energyTimer = 0f;
	public Image energyMeterDisplay; 

	public bool canDig = true;
	public bool canPile = true;
	public bool isDashing = false;

    void Start(){
        energyTimer = energyMax;
    }

	void FixedUpdate(){
		if (isDashing == true){
			energyTimer -= 0.01f;
			energyMeterDisplay.fillAmount = energyTimer / energyMax; 
			Debug.Log("DASHING: energy down: " + energyTimer);  //replace with the desired ability display
			                                 // time has run out
		}

		else if (energyTimer < energyMax) {
			energyTimer += 0.01f;
			energyMeterDisplay.fillAmount = energyTimer / energyMax; 
			Debug.Log("energy up: " + energyTimer);  //replace with the desired ability display
		}

		if (energyTimer >= digEnergyMinimum){
			canDig=true;
		} else {
			canDig=false;
		}

		if (energyTimer >= pileEnergyMinimum){
			canPile=true;
		} else {
			canPile=false;
		}

	}

	public void ReduceEnergyDig(){
		energyTimer -= digEnergy;
	}

	public void ReduceEnergyPile(){
		energyTimer -= pileEnergy;
	}

}
