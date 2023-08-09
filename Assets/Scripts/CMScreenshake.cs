using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMScreenshake : MonoBehaviour{
    public static CMScreenshake cmss;

    private void Awake(){
        cmss = this;
    }
}
