using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DirectorInfo : MonoBehaviour{
    public bool open;
    public CanvasGroup cg;
    public GameManager gm;

    public TMP_Text credits;
    public TMP_Text creditIncome;
    public TMP_Text difficulty;
    public TMP_Text slimes;
    public TMP_Text nextID;
    public TMP_Text multiplier;
    

    private void Awake(){
        open = false;
        cg.alpha = 0;
    }

    void Update()
    {
        cg.alpha = Mathf.Lerp(cg.alpha, open ? 1 : 0, Time.unscaledDeltaTime * 10f);
        cg.interactable = open;
        cg.blocksRaycasts = open;
        
        credits.text = "Credits: " + gm.currentCredits;
        creditIncome.text = "Income: " + gm.creditIncome;
        difficulty.text = "Difficulty: " + gm.difficulty;
        slimes.text = "Slimes: " + gm.currentSlimes;
        nextID.text = "Next ID: " + GameManager.nextId;
        multiplier.text = "Multiplier: " + gm.statsMult;
        
        
        if (Input.GetKeyDown(KeyCode.Insert)){
            open = !open;
        }
    }

   
}