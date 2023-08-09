using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunManager : MonoBehaviour{
    public static RunManager rm;

    private void Awake(){
        rm = this;
    }

    private void Start(){
        SceneManager.LoadScene(Scenum.MainMenu);
    }

    public CharacterSO cso;
    public WeaponSO wso;
}
