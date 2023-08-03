using UnityEngine;

public class SpawnItemScript: MonoBehaviour{
    [HideInInspector] public Player p;

    public void Init(Player _p){
        p=_p;
    }
}