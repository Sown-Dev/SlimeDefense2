    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Tilemaps;
    using Random = UnityEngine.Random;

    public class TerrainSpawner: MonoBehaviour{
        public Player p;


        public GameObject radioPrefab;
        public GameObject chunkPrefab;

        public Dictionary<Vector2Int,Chunk> Chunks;
        public Chunk currentChunk;
        
        public List<Sprite> GroundSprites;
        private List<Tile> GroundTiles;

        public  Tilemap myTilemap;

        public GameObject radioInstance;

        

        private void GenerateTiles(){
            GroundTiles = new List<Tile>();
            foreach (Sprite s in GroundSprites){
                Tile t = new Tile();
                t.sprite = s;
                GroundTiles.Add(t);
            }
        }
        void Awake(){
            GenerateTiles();
            Chunks = new Dictionary<Vector2Int, Chunk>();
            currentChunk = SpawnChunk(new Vector2Int(0,0));

            Vector2 randomPos = Random.insideUnitCircle * Random.Range(400, 500);
            radioInstance = Instantiate(radioPrefab, randomPos, Quaternion.identity);
        }

        void Update(){
            /*Rect chunkBounds = new Rect(currentChunk.pos*100, new Vector2(100, 100));
            Debug.DrawLine((Vector2)currentChunk.pos*100, currentChunk.pos*100 + new Vector2(50, 50), Color.red);
            if (!chunkBounds.Contains(p.transform.position)){
                foreach (KeyValuePair<Vector2Int, Chunk> c in Chunks){
                    Vector2 minBoundsB = c.Value.pos * 100- new Vector2(50, 50);
                    Vector2 maxBoundsB = minBoundsB + new Vector2(100, 100);

                    if (new Rect(minBoundsB, maxBoundsB - minBoundsB).Contains(p.transform.position))
                    {
                        Debug.Log("Reset Bounds");
                        currentChunk = c.Value;
                        break;
                    }
                }
                //SpawnChunk(Vector2Int.RoundToInt( p.transform.position/100));
            }*/
            
            if(p.transform.position.x > (Convert.ToInt32(p.transform.position.x/100)*100 + 30)){
                SpawnChunk(Vector2Int.RoundToInt( p.transform.position/100)+ Vector2Int.right);
            }
            if(p.transform.position.x < (Convert.ToInt32(p.transform.position.x/100)*100 - 30)){
                SpawnChunk(Vector2Int.RoundToInt( p.transform.position/100) + Vector2Int.left);
            }
            if(p.transform.position.y > (Convert.ToInt32(p.transform.position.y/100)*100 + 30)){
                SpawnChunk(Vector2Int.RoundToInt( p.transform.position/100)+ Vector2Int.up);
            }
            if(p.transform.position.y < (Convert.ToInt32(p.transform.position.y/100)*100 - 30)){
                SpawnChunk(Vector2Int.RoundToInt( p.transform.position/100) + Vector2Int.down);
            }
            //diagonals
            if(p.transform.position.x > (Convert.ToInt32(p.transform.position.x/100)*100 + 30) &&
               p.transform.position.y > (Convert.ToInt32(p.transform.position.y/100)*100 + 30)){
                SpawnChunk(Vector2Int.RoundToInt( p.transform.position/100)+ Vector2Int.right + Vector2Int.up);
            }
            if(p.transform.position.x < (Convert.ToInt32(p.transform.position.x/100)*100 - 30) &&
               p.transform.position.y > (Convert.ToInt32(p.transform.position.y/100)*100 + 30)){
                SpawnChunk(Vector2Int.RoundToInt( p.transform.position/100)+ Vector2Int.left + Vector2Int.up);
            }
            if(p.transform.position.x > (Convert.ToInt32(p.transform.position.x/100)*100 + 30) &&
               p.transform.position.y < (Convert.ToInt32(p.transform.position.y/100)*100 - 30)){
                SpawnChunk(Vector2Int.RoundToInt( p.transform.position/100)+ Vector2Int.right + Vector2Int.down);
            }
            if(p.transform.position.x < (Convert.ToInt32(p.transform.position.x/100)*100 - 30) &&
               p.transform.position.y < (Convert.ToInt32(p.transform.position.y/100)*100 - 30)){
                SpawnChunk(Vector2Int.RoundToInt( p.transform.position/100)+ Vector2Int.left + Vector2Int.down);
            }
            
           
            
        }
        public Chunk SpawnChunk(Vector2Int pos){
            if (Chunks.ContainsKey(pos)){
                return Chunks[pos];
            }
            GameObject c = Instantiate(chunkPrefab, transform);
            c.transform.position = (Vector2) pos * 100;
            Chunk chk = c.GetComponent<Chunk>();
            chk.GroundTiles = GroundTiles;
            chk.myTilemap = myTilemap;
            chk.pos = pos;
            Chunks[pos]=chk;
            return chk;
        }
    }
