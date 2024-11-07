using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveShoot : MonoBehaviour
{
public float speed =2f;
public float stoppingDistance = 4f; // when enemy stops moving towards player
public float retreatDistance = 3f; // when enemy moves away from approaching player
private float startTimeBtwShots = 2;
public GameObject projectile;

private Rigidbody2D rb;
private Transform player;
private vector2 PlayerVect;

public int EnemyLive = 30;
private Renderer rend;
// private GameHandler gameHandler;

public float attackRange = 10;
public bool isAttacking = false;
private float scaleX;

    // Start is called before the first frame update
    void Start()
    {
        Physics2D.queriesStartinColliders = false;
        scaleX = gameObject.transform.localScale.x;

        rb = GetComponent<RigidBody2D> ();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
