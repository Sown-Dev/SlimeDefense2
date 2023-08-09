using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeathScreenUI : MonoBehaviour{
    public CanvasGroup cg;
    public TMP_Text scoreTxt;
    public bool open;


    private void Update(){
        cg.alpha = Mathf.Lerp(cg.alpha, open ? 1 : 0, Time.unscaledDeltaTime * 10);
        cg.interactable = open;
        cg.blocksRaycasts = open;
        if (Input.GetKey(KeyCode.PageUp) && Input.GetKeyDown(KeyCode.PageDown)){
            Restart();
        }
    }

    private void Awake(){
        open = false;
        
    }

    public void Open(float time, int upgrades, int slimes){
        open = true;
        
        scoreTxt.text =
            $"You survived for <u>{(int)time} seconds</u>,\nKilled <u>{slimes} slimes</u>,\nand bought <u>{upgrades} upgrades</u>";
        
    }
    

    public void Restart(){
        SceneManager.LoadScene(Scenum.Game);
    }

    public void Menu(){
        SceneManager.LoadScene(Scenum.MainMenu);

    }
}
