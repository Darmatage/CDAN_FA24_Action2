using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerHurt: MonoBehaviour{

      public Animator anim;
      public Rigidbody2D rb2D;
	  private SpriteRenderer rend;
	  private Color rendColor;

      void Start(){
           anim = gameObject.GetComponentInChildren<Animator>();
           rb2D = transform.GetComponent<Rigidbody2D>();
		   rend = GetComponentInChildren<SpriteRenderer>();
		   rendColor = rend.color;    
      }

	public void playerHit(){
		anim.SetTrigger ("Hurt");
		rend.material.color = new Color(2.4f, 0.9f, 0.9f, 1f);
		StartCoroutine(ResetColor());
	}

	public void playerDead(){
		rb2D.isKinematic = true;
		//anim.SetTrigger ("Dead");
	}

	IEnumerator ResetColor(){
		yield return new WaitForSeconds(0.5f);
		rend.material.color = rendColor;
	}

}