using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void StandStill( Vector3 pos); 


public class Movement : MonoBehaviour{
    public static event StandStill OnStandStill;

    
    public Player player;
    //Animation Stuff;
    public Animator am;
    
    
    public float moveSpeed = 5.5f;
    private float moveAMT;
    private Rigidbody2D rb;

    private float distanceTraveled;

    public Shooting _shooting;
    private void Awake(){
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    public AudioSource src;
    public AudioClip walk;

    private Vector3 startPos; //picks new pos every 3 seconds, moving from this position will produce footsteps
    private float elapsed;
    void Update(){
        elapsed += Time.deltaTime;
        if (elapsed > 2f){
            elapsed = 0;
            startPos = transform.position;
        }

        if (Vector2.Distance(startPos, transform.position) >1.2f){
            startPos = transform.position;
            src.PlayOneShot(walk,1f );
        }

        moveAMT = _shooting.shooting ? moveSpeed*player.finalStats[Stats.Statstype.ShootingSpeedMultiplier] : moveSpeed;
        
        float xAmt = 0;
        float yAmt = 0;
        if (Input.GetKey(KeyCode.A)){
            xAmt -= 1; //moveAMT * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D)){
            xAmt += 1; // moveAMT * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.W)){
            yAmt += 1; //moveAMT * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.S)){
            yAmt -= 1;//moveAMT * Time.deltaTime;
        }

        Vector2 moveDir = new Vector2(xAmt, yAmt).normalized;
        moveDir *= moveAMT * Time.deltaTime;
        
        if (player.debuffs[Debuffs.DebuffTypes.Slow])
        {
            
            moveDir *= 0.5f;
        }

        //transform.position += new Vector3(xAmt, yAmt, 0);
        rb.AddForce(moveDir * player.finalStats[Stats.Statstype.MoveSpeed]);
        
        am.SetFloat("Speed", rb.velocity.magnitude);

        if (rb.velocity.magnitude < 0.1f){
            OnStandStill?.Invoke(transform.position);
        }
    }
}