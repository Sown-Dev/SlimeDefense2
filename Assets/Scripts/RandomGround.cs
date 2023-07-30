using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;


public class RandomGround : MonoBehaviour{
    public List<Sprite> GroundSprites;
    private List<Tile> GroundTiles;
    public Vector2Int size = new Vector2Int(100,100);

    private Tilemap myTilemap;

    private void GenerateTiles(){
        GroundTiles = new List<Tile>();
        foreach (Sprite s in GroundSprites){
            Tile t = new Tile();
            t.sprite = s;
            GroundTiles.Add(t);
        }
    }
    
    private void Awake(){
        
        myTilemap = gameObject.GetComponent<Tilemap>();
        
        GenerateTiles();
        for (int i = -size.x / 2; i <= size.x / 2; i++){
            for (int j = -size.y / 2; j <= size.y / 2; j++){
                myTilemap.SetTile(new Vector3Int(i,j,0),GroundTiles[Random.Range(0,GroundTiles.Count)]);
            }
        }
    }
}
