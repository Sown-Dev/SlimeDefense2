using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Chunk : MonoBehaviour{
    public GameObject Crate;
    public GameObject Tree;
    public GameObject MedCrate;
    
    
    public Vector2Int pos; //position in 100x100 grid. so (1,2) is the chunk at 100,200
    
    public List<Tile> GroundTiles;
    public Vector2Int size = new Vector2Int(100,100);

    public Tilemap myTilemap;

    
    
    
    
    private void Start(){
        int CratesSpawned = 1;
        
        for (int i = -size.x / 2; i <= size.x / 2; i++){
            for (int j = -size.y / 2; j <= size.y / 2; j++){
                myTilemap.SetTile(new Vector3Int(i+(pos.x*100),j+(pos.y*100),0),GroundTiles[Random.Range(0,GroundTiles.Count)]);
                if (Utils.Random(0.0016f/CratesSpawned)){
                    Instantiate(Crate, new Vector3(i+(pos.x*100),j+(pos.y*100),0), Quaternion.identity);
                    CratesSpawned++;
                }
            }
        }
        //once generated add objects
        //add trees
        for(int i= (int) Random.Range(70,100); i>0; i--){
            Vector3 randPos = new Vector3(Random.Range(-50,50),Random.Range(-50,50),0);
            GameObject go=Instantiate(Tree, randPos + (Vector3)(Vector2)(pos * 100), Quaternion.identity);
            go.transform.parent = transform;
        }
        for(int i= (int) Random.Range(24,35); i>0; i--){
            Vector3 randPos = new Vector3(Random.Range(-50,50),Random.Range(-50,50),0);
            GameManager.SpawnGreenSlime(randPos + (Vector3)(Vector2)(pos * 100), Slime.SlimeState.Passive);
        }
        for(int i= (int) Random.Range(7,10); i>0; i--){
            Vector3 randPos = new Vector3(Random.Range(-50,50),Random.Range(-50,50),0);
            GameObject go= Instantiate(MedCrate, randPos + (Vector3)(Vector2)(pos * 100), Quaternion.identity);
            go.transform.parent = transform;

        }
        
    }
}
