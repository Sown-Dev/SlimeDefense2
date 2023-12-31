﻿using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatusEffectUIInstance : MonoBehaviour{
        public StatusEffect mySE;
        public Image Icon;
        public Image Fill;
        public TMP_Text TimeLeft;
        
        public void Start(){
            Icon.sprite = mySE.icon;
        }

        private void Update(){
            Fill.fillAmount = mySE.duration / mySE.maxTime;
            TimeLeft.text = Math.Round(mySE.duration, 1) +"";
            Fill.fillAmount = mySE.duration / mySE.maxTime;
        }
    }
