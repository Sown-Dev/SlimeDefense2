using System;
using UnityEngine;

public class CharacterStatsUI: MonoBehaviour{
    public static CharacterStatsUI csui;
    
    public CharacterObjectUI selected;

    private void Awake(){
        csui = this;
    }

    public void SelectNew(CharacterObjectUI c){
        if (selected != null){
            selected.Selected = false;
        }

        c.Selected = true;
        selected = c;
    }
}