using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectUI : MonoBehaviour{
    public CharacterStatsUI csui;

    public void StartRun(){
        if (csui.selected){
            RunManager.rm.cso = csui.selected.cso;
            RunManager.rm.wso = csui.selected.cso.weapon;
            SceneManager.LoadScene(Scenum.Game);
        }
    }
}
