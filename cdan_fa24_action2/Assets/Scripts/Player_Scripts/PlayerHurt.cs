using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerHurt: MonoBehaviour{

      private Animator anim;
      private Rigidbody2D rb2D;
	  public AudioSource hurtSFX;

//Color change:
	  private SpriteRenderer rend;
	  private Color rendColor;
	  public float timeToShowHurt = 0.5f;

    void Start(){
           anim = gameObject.GetComponentInChildren<Animator>();
           rb2D = transform.GetComponent<Rigidbody2D>();
		   rend = GetComponentInChildren<SpriteRenderer>();
		   rendColor = rend.color;    
      }

	public void playerHit(){
		anim.SetTrigger ("Hurt");
		hurtSFX.Play();
		rend.material.color = new Color(2.4f, 0.9f, 0.9f, 1f);
		StartCoroutine(ResetColor());
	}

	public void playerDead(){
		rb2D.isKinematic = true;
		anim.SetTrigger ("Dead");
		anim.SetBool ("isDead", true);
	}

	IEnumerator ResetColor(){
		yield return new WaitForSeconds(timeToShowHurt);
		rend.material.color = rendColor;
	}

}