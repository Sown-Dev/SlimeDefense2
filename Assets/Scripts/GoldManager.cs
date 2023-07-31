using UnityEngine;


public class GoldManager :MonoBehaviour{
    public static GoldManager gm;
    public GameObject gold;

    void Awake(){
        gm = this;
    }
    
    public void SpawnGold(Vector2 pos, float amount)
    {
        GameObject goldGO = Instantiate(gold, pos, Quaternion.identity);
        goldGO.GetComponent<GoldDrop>().amount = amount;
        
    }
}