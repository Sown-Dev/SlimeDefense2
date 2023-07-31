using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;
using SysRandom = System.Random;


public class GameManager : MonoBehaviour{
    public List<SpawnInfo> SpawnInfoList;
    public SpawnInfo GreenSlime;

    static SysRandom rnd = new SysRandom();


    public static GameManager gm;

    public Player player;

    public GameObject greenSlime;

    [Header("Timer")] public TMP_Text timerTxt;
    public double TimeElapsed;
    public bool Prep = true;
    private float PrepTime = 50f;

//Director stuff:
    public long currentCredits;
    public int creditIncome; //added every 20 seconds
    private float creditElapsed;
    private float DirectorElapsed;
    public List<SpawnInfo> CurrentHand;
    private float handSize = 2;
    public int difficulty = 1; //increases credit income by 5

    public int currentSlimes = 0;

    private void Awake(){
        Slime.OnDeath += SlimeDeath;
        gm = this;
        Prep = true;
        //SpawnInfoList.Add(GreenSlime);
    }

    int wavesToSkip = 0;
    [HideInInspector] public float creditMult = 1f;
    [HideInInspector] public float statsMult = 1f;

    public void Update(){
        TimeElapsed += Time.deltaTime;
        creditElapsed += Time.deltaTime;
        DirectorElapsed += Time.deltaTime;


        creditIncome = (int)(90 + (TimeElapsed / 2.6f) + difficulty * 32);

        if (Prep && TimeElapsed > PrepTime){
            Prep = false;
            TimeElapsed = 0;
            difficulty += 1;
            currentCredits += 130;
        }


        if (!Prep){
            if (difficulty < TimeElapsed / 50){
                difficulty++;
            }


            //Director only functions outside of prep phase

            //Director Loop:
            creditMult = (float)((TimeElapsed - 40) / 500f) + 1f;
            if (creditElapsed > 14){
                creditElapsed = 0;
                if (wavesToSkip > 0){
                    wavesToSkip--;
                }
                else{
                    currentCredits += (long)(creditIncome * creditMult);
                    if (Utils.Random(0.14f)){ // chance to skip a wave
                        wavesToSkip = 1;
                    }
                }
            }

            //can spend excess credits to increase difficulty. needs to be high to avoid creating a feedback loop
            if (creditElapsed > 12 && currentCredits > 500){
                currentCredits -= 200;
                difficulty += 1;
            }

            // Credit Spending:
            if (DirectorElapsed > 0.45 / (1 + (TimeElapsed / 70))){
                DirectorElapsed = 0;
                DirectorTick();
            }
        }


        //Timer text
        if (Prep){
            float timeLeft = (float)(PrepTime - TimeElapsed);
            timerTxt.text = "<size=60>Horde Arriving in:</size>\n" + (int)timeLeft / 60 + ":" +
                            (timeLeft % 60).ToString("00");
        }
        else{
            timerTxt.text = (int)TimeElapsed / 60 + ":" + (TimeElapsed % 60).ToString("00");
        }
    }

    public void DirectorTick(){
        statsMult = (float)(TimeElapsed / 500f + 1f);


        if (CurrentHand.Count > 0){
            SpawnInfo highest = new SpawnInfo();
            List<SpawnInfo> toRemove = new List<SpawnInfo>();
            for (int i = 0; i < CurrentHand.Count; i++){
                SpawnInfo current = CurrentHand[i];
                if (current.CreditCost < currentCredits && current.SpawnTime < TimeElapsed){
                    if (current.CreditCost > highest.CreditCost){
                        highest = current;
                    }
                }
                else{
                    toRemove.Add(current);
                }
            }

            foreach (var v in toRemove){
                CurrentHand.Remove(v);
            }

            //spawn slime
            if (highest.Prefab != null){
                currentCredits -= highest.CreditCost;
                highest.Prefab.GetComponent<Slime>().creditCost = highest.CreditCost;
                SpawnSlime(highest.Prefab);
                CurrentHand.Remove(highest);
            }
        }
        else{
            //Grab new hand
            SpawnInfoList.OrderBy(x => Guid.NewGuid()).ToList();
            Utils.Shuffle(SpawnInfoList);
            for (int i = 0; i < handSize; i++){
                CurrentHand.Add(SpawnInfoList[i]);
            }
        }
    }

    public void SpawnSlime(GameObject slime){
        Vector3 Origin = player.transform.position;
        
        //Old random code
        /*
        Vector3 rand = new Vector3(Random.Range(8, 11), Random.Range(8, 11), 0);
        rand.x *= Random.Range(-1, 1) == 0 ? 1 : -1; //randomly invert x and y
        rand.y *= Random.Range(-1, 1) == 0 ? 1 : -1;   */
        
        //new

        Bounds inside = new Bounds(player.rb.velocity.normalized*-2, new Vector3(8,8,1));
        Gizmos.DrawWireCube(inside.center, inside.size);
        
        Vector3 rand = Utils.RandomBetweenBounds(new Bounds(Vector3.zero, new Vector3(12, 12, 1)), inside);

        Vector3 spawnPos = Origin + rand;
        SpawnSlime(spawnPos, slime, Slime.SlimeState.Aggressive);
    }

    public static void SpawnGreenSlime(Vector3 pos, Slime.SlimeState startState){
        SpawnSlime(pos, gm.greenSlime, startState);
    }

    //ID stuff
    public static long nextId = 0;
    [HideInInspector] public int lowestID;

    public static Slime SpawnSlime(Vector3 pos, GameObject prefab, Slime.SlimeState startState){
        gm.currentSlimes++;
        GameObject slimeGO = Instantiate(prefab, pos,
            Quaternion.identity);
        slimeGO.transform.parent = gm.transform;
        Slime s = slimeGO.GetComponent<Slime>();
        s.target = gm.player.transform;
        s.State = startState;
        s.maxHealth *= gm.statsMult;
        s.id = nextId++;

        return s;
    }

    void SlimeDeath(Slime s){
        currentSlimes--;
        currentCredits += 2; //dead slimes give back credits
        if (TimeElapsed > 200){
            currentCredits += 2;
        }
    }

    //deletes slime and returns credits based on health
    public void recycle(Slime s){
        currentCredits += s.creditCost;
        s.die();
    }
}