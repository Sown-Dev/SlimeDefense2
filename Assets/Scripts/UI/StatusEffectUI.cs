using UnityEngine;

    public class StatusEffectUI : MonoBehaviour{
        public Transform list;
        
        public GameObject StatusEffectPrefab;
        
        public Debuffs debuffs;


        public void AddSE(StatusEffect se){
            GameObject go = Instantiate(StatusEffectPrefab, list);
            go.GetComponent<StatusEffectUIInstance>().mySE = se;
        }
    }
