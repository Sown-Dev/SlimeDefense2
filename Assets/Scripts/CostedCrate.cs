using System;
using TMPro;
using UnityEngine;

public class CostedCrate : Interactable{
    public float cost;
    public TextMesh costTXT;

    public void FixedUpdate(){
       

        costTXT.color = inRange ? Color.white : Color.clear;
        if (inRange && interactable){
            costTXT.text = cost.ToString();
        }
    }

    public override void Interact(){
        if (Player.p.SpendGold(cost)){
            interactable = false;
            firstInteract = false;
            Player.p.RemoveGold(cost);
            base.Interact();
        }
    }
}