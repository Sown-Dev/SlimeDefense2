using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCollector : MonoBehaviour{
    public Player p;
    
    private void OnTriggerEnter2D(Collider2D col){
        if (col.GetComponent<GoldDrop>()){
            GoldDrop gd = col.GetComponent<GoldDrop>();
            p.AddGold(gd.amount);
            gd.PickedUp = true;
            gd.Goto = p.transform;
        }
    }
}