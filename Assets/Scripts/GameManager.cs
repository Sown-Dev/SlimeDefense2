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
    public DeathScreenUI deathScreenUI;

    [Header("Timer")] public TMP_Text taskTxt;
    public TMP_Text timerTxt;
    public double TimeElapsed;
    private float PrepTime = 50f;

//Director stuff:
    public long currentCredits;
    public int creditIncome; //added every 20 seconds
    private float creditElapsed;
    private float DirectorElapsed;
    public List<SpawnInfo> CurrentHand;
    private float handSize = 3;
    public int difficulty = 1; //increases credit income by 5

    public int currentSlimes = 0;

    private void Awake(){
        currentPhase = Phase.prep;
        Slime.OnDeath += SlimeDeath;
        gm = this;
    }

    int wavesToSkip = 0;
    [HideInInspector] public float creditMult = 1f;
    [HideInInspector] public float statsMult = 1f;
    private int wavesNotSkipped;

    public void Update(){
        TimeElapsed += Time.deltaTime;
        creditElapsed += Time.deltaTime;
        DirectorElapsed += Time.deltaTime;


        creditIncome = (int)(60 + (TimeElapsed / 2.6f) + difficulty * 32);


        //phase check

        if (currentPhase == Phase.prep && TimeElapsed > PrepTime){
            currentPhase = Phase.waitforsignal;
            TimeElapsed = 0;
            difficulty += 1;
            currentCredits += 90;
        }

        if (currentPhase == Phase.waitforsignal && TimeElapsed > SignalTime){
            GetSignal();
            difficulty += 2;
            wavesToSkip++;
        }

        if (currentPhase == Phase.waitforheli){
            heliElapsed += Time.deltaTime;
            if (heliElapsed > HeliTime){
                ArriveHeli();
                difficulty *= 2;
                wavesToSkip++;
            }
        }


        if (currentPhase > Phase.prep){
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
                    wavesNotSkipped = 0;
                }
                else{
                    currentCredits += (long)(creditIncome * creditMult);
                    wavesNotSkipped++;
                    if (Utils.Random(0.14f)){ // chance to skip a wave
                        wavesToSkip ++;
                    }

                    if (wavesNotSkipped > 10){
                        wavesToSkip++;
                    }
                }
            }

            //can spend excess credits to increase difficulty. needs to be high to avoid creating a feedback loop
            if (creditElapsed > 12 && currentCredits > 500){
                currentCredits -= 200;
                difficulty += 1;
            }

            // Credit Spending:
            if (DirectorElapsed > 0.55 / (1 + (TimeElapsed / 70))){
                DirectorElapsed = 0;
                DirectorTick();
            }
        }


        //Timer text

        if (currentPhase > Phase.prep){
            timerTxt.text = (int)TimeElapsed / 60 + ":" + ((int)TimeElapsed % 60).ToString("00");
        }
        else{
            timerTxt.text = "";
        }

        switch (currentPhase){
            case Phase.prep:
                float timeLeft = (float)(PrepTime - TimeElapsed);
                taskTxt.text = "<size=60>Horde Arriving in:</size>\n" + (int)timeLeft / 60 + ":" +
                               ((int)timeLeft % 60).ToString("00");
                break;
            case Phase.waitforsignal:
                taskTxt.text = "Survive";
                break;
            case Phase.followsignal:
                taskTxt.text = "Follow signal";
                break;
            case Phase.waitforheli:
                float heliRemainingTime = (HeliTime - heliElapsed);
                taskTxt.text = "<size=60>Rescue Helicopter Arriving:</size>\n" + (int)heliRemainingTime / 60 + ":" +
                               ((int)heliRemainingTime % 60).ToString("00");
                break;
            case Phase.followheli:
                taskTxt.text = "Get to Evacuation Point";
                break;
        }
    }

    public void DirectorTick(){
        statsMult = (float)(TimeElapsed / 340f + 1f);


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

            DirectorElapsed = -0.4f; // delay for grabbing new hand
        }
    }

    public void SpawnSlime(GameObject slime){
        Vector3 Origin = player.transform.position;


        Bounds inside = new Bounds(player.rb.velocity.normalized * -1.5f, new Vector3(15, 15, 0));
        //draw rectangle for inside bounds:
        /*Debug.DrawLine(Origin+ inside.center + new Vector3(inside.extents.x, inside.extents.y, 0),
            Origin+inside.center + new Vector3(-inside.extents.x, inside.extents.y, 0), Color.red, 1f);
        Debug.DrawLine(Origin+inside.center + new Vector3(inside.extents.x, inside.extents.y, 0),
            Origin+inside.center + new Vector3(inside.extents.x, -inside.extents.y, 0), Color.red, 1f);
        Debug.DrawLine(Origin+inside.center + new Vector3(-inside.extents.x, -inside.extents.y, 0),
            Origin+inside.center + new Vector3(-inside.extents.x, inside.extents.y, 0), Color.red, 1f);
        Debug.DrawLine(Origin+inside.center + new Vector3(-inside.extents.x, -inside.extents.y, 0),
            Origin+inside.center + new Vector3(inside.extents.x, -inside.extents.y, 0), Color.red, 1f);*/


        Vector3 rand = Utils.RandomBetweenBounds(new Bounds(Vector3.zero, new Vector3(20, 20, 0)), inside);

        Vector3 spawnPos = Origin + rand;
        Debug.DrawLine(Origin, inside.center, Color.blue, 1f);
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
        s.damage *= (int)gm.statsMult;
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

    //story stuff:

    public DeviceUI dui;
    public Phase currentPhase;

    public float SignalTime = 120;

    public void GetSignal(){
        dui.AcquireSignal();
        currentPhase = Phase.followsignal;
    }

    public void CallHeli(){
        currentPhase = Phase.waitforheli;
        dui.RemoveSignal();
    }

    public float HeliTime = 600;
    float heliElapsed = 0;

    public void ArriveHeli(){
        currentPhase = Phase.followheli;
        Vector2 HeliPos = (Random.insideUnitCircle * 150) + (Vector2) player.transform.position;
    }

    public void GameOver(){
        currentPhase = Phase.dead;
        deathScreenUI.Open((float)TimeElapsed, player.Upgrades.Count, player.Slimeskilled);
    }
}

public enum Phase{
    dead = -1,
    prep = 1,
    waitforsignal = 2,
    followsignal = 3,
    waitforheli = 4,
    followheli = 5,
}

