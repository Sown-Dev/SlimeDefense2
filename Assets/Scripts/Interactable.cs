using UnityEngine;

public class Interactable : MonoBehaviour{
    [HideInInspector] public GameObject player;
    public FadeIn eKey;
    [HideInInspector] public Material m;
    [HideInInspector] public Color toColor;
    [HideInInspector] public bool interactable = true;
    [HideInInspector] public SpriteRenderer sr;
    public Sprite postInteract;
    void Start(){
        sr = gameObject.GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");
        m = gameObject.GetComponent<SpriteRenderer>().material;
        toColor = Color.black;
        m.SetColor("_Color", toColor);
    }

    void Update(){
        //m.SetFloat("_Thickness",Mathf.Lerp(m.GetFloat("_Thickness"), toTh, 5*Time.deltaTime));
        m.SetColor("_Color", toColor);
        if (interactable){
            if (Vector2.Distance(player.transform.position, transform.position) < 2.5f){
                //show E
                eKey.enable();
                toColor = Color.white;

                if (Input.GetKeyDown("e")){
                    Interact();
                }
            }
            else{
                eKey.disable();
                toColor = Color.black;
            }
        }
        else{
            eKey.disable();
            toColor = Color.black;
        }
    }

    public virtual void Interact(){
        //Do nothing
    }
}