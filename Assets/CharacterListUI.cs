using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterListUI : MonoBehaviour{
    public GameObject listPrefab;
    
    public List<CharacterSO> characters;

    private void Awake(){
        characters = Resources.LoadAll<CharacterSO>("ScriptableObjects/Characters").ToList();
        foreach (Transform child in transform){
            Destroy(child.gameObject);
        }
        foreach (var c in characters){
            var go = Instantiate(listPrefab, transform);
            go.GetComponent<CharacterObjectUI>().cso = c;
        }
    }
}
