
using System;
using UnityEngine;

public class GoldDrop : MonoBehaviour
{
    public float amount;

    public bool PickedUp;
    public Transform Goto;

    public Vector2 startPos;

    private void Start(){
        startPos = transform.position;
        transform.localScale *= Mathf.Sqrt(amount)/2f;
    }


    void Update()
    {
        if (PickedUp){
            Vector3 normalizedDir = (Goto.position - transform.position).normalized * 0.4f;
            
            transform.position = Vector2.Lerp(transform.position, Goto.position + normalizedDir,
                Time.deltaTime * 7 + 0.02f);
            if (Vector2.Distance(transform.position, Goto.position) < 0.3f)
            {
                Destroy(gameObject);
            }
        }
    }
}