using System;
using UnityEngine;

public class RescueHeli: MonoBehaviour{
    private Vector2 goToPos;
    public GameObject ladderPrefab;

    public void Init(Vector2 pos, Vector2 gotoPos){
        transform.position = pos;
        goToPos = gotoPos;
    }

    private void Update(){
        transform.position = Vector3.MoveTowards(transform.position, goToPos,100*Time.deltaTime);
        
        if(Vector2.Distance(transform.position, goToPos) < 1f){
            Instantiate(ladderPrefab, transform.position, Quaternion.identity);
        }
        else{
            
        }
    }
}