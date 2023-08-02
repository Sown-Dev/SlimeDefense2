using System;
using UnityEngine;

    public class StatusEffectUI : MonoBehaviour{
        public Transform list;
        
        public GameObject StatusEffectPrefab;

        [HideInInspector] public Debuffs debuffs;
        
        public void Start(){
            debuffs = Player.p.debuffs;
            UpdateSEs();
        }
        
        int debuffCount = 0;
        private void Update(){
            if(debuffs.debuffs.Count!= debuffCount){
                debuffCount = debuffs.debuffs.Count;
                UpdateSEs();
            }
            debuffCount = debuffs.debuffs.Count;
        }

        public void UpdateSEs(){
            foreach (Transform child in list){
                Destroy(child.gameObject);
            }
            foreach (StatusEffect se in debuffs.debuffs){
                AddSE(se);
            }
        }

        public void AddSE(StatusEffect se){
            GameObject go = Instantiate(StatusEffectPrefab, list);
            go.GetComponent<StatusEffectUIInstance>().mySE = se;
        }
    }
