using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallax : MonoBehaviour
{
    public float pMult = 0.08f;

    public RectTransform rt;
    // Update is called once per frame
    void Update(){
        Vector3 mpos = Camera.main.ScreenToWorldPoint(Input.mousePosition) * pMult;
        rt.anchoredPosition= new Vector3(mpos.x, mpos.y, 10) ;
    }
}
