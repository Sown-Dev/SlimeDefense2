using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Console : Interactable
{
    public override void Interact(){
        GameManager.gm.CallHeli();
        interactable = false;
    }
}
