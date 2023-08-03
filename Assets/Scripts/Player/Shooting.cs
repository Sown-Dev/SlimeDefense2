using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public delegate void SpawnBullet( int ammoRemaining, Player p, Bullet b); 

public class Shooting : MonoBehaviour{
    public static event SpawnBullet OnSpawnBullet;
    
    
    private float elapsed;
    public int currentAmmo;
    public bool shooting;
    private float shootingEl;

    private float reloadElapsed;
    private bool flashbool;

    private float muzIntensity;

    [Header("References")] 
    public Player player;
    public AudioSource src;
    public GameObject bullet;
    public Transform gunPos;
    public ParticleSystem muzzleflash;
    public Light2D muzlight;
    public SpriteRenderer sr;
    public Animator am;
    public Transform gun;
    [Header("UIReferences")]
    public ReloadCircle rc;
    public Image reloadSquare;
    public TMP_Text ammoText;


    private Vector3 startRot;

    private void Awake(){
        startRot = transform.rotation.eulerAngles;
        currentAmmo = Convert.ToInt32(player.finalStats[Stats.Statstype.AmmoCapacity]);
    }


    void Update(){
        if (sr.sprite != player.Weapon.sprite){
            sr.sprite = player.Weapon.sprite;
        }

        ammoText.text = currentAmmo + "/" + Convert.ToInt32(player.finalStats[Stats.Statstype.AmmoCapacity]);
        shootingEl += Time.deltaTime;
        if (shootingEl > 0.12f){
            shooting = false;
        }


        if (Input.GetMouseButtonDown(0) && currentAmmo <= 0 && !reloading){
            Reload();
        }

     
        if (Input.GetKeyDown(KeyCode.R) && !reloading){
            Reload();
        }

        if (reloading){
            reloadSquare.fillAmount= 1- (reloadElapsed / player.finalStats[Stats.Statstype.ReloadTime]);
            
            Cursor.cr.setMode(true);
            reloadElapsed += Time.deltaTime;
            rc.fillAmt = reloadElapsed / player.finalStats[Stats.Statstype.ReloadTime];
            if (reloadElapsed >= player.finalStats[Stats.Statstype.ReloadTime]){
                reloading = false;

                currentAmmo = Convert.ToInt32(player.finalStats[Stats.Statstype.AmmoCapacity]);

                //put reloadingElapsed=0 here if u want to not be able to cancel reload by pressing r again
            }
        }
        else{
            reloadSquare.fillAmount= 0;
            rc.fillAmt = 0;
            Cursor.cr.setMode(false);
        }


        elapsed += Time.deltaTime;

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rot = (mousePosition - transform.position).normalized;
        float ang = Mathf.Atan2(rot.y, rot.x) * Mathf.Rad2Deg;
        //ang = Mathf.RoundToInt(ang / 10f) * 10f;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, ang) + startRot);
        gun.localScale = new Vector3(1, (ang > 90 || ang < -90) ? -1 : 1, 1);
        //transform.localPosition = (Vector2.left );

        if (Input.GetMouseButton(0) || Input.GetKey(KeyCode.C) ){
            Shoot();
        }


        muzIntensity -= 1 * (Time.deltaTime / 0.06f);
        muzlight.intensity = flashbool ? muzIntensity : 0;
    }


    void Shoot(){
        if (elapsed < 1 / player.finalStats[Stats.Statstype.AttackSpeed] || currentAmmo <= 0 || reloading){
            return;
        }

        shootingEl = 0;
        shooting = true;

        flashbool = true;
        muzIntensity = 1.6f;
        StartCoroutine(flash());

        muzzleflash.Play();
        //ScreenShake.camShake.Shake(0.014f, 0.03f);
        
        
        elapsed = 0f;

        //animation, sound, flash
        player.rb.AddForce((Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z) * Vector2.right) * - player.finalStats[Stats.Statstype.Recoil]);
        am.SetTrigger("Shoot");
        //src.PlayOneShot(equipped.sound);
        int numProjectiles = Convert.ToInt32(player.finalStats[Stats.Statstype.Projectiles]);
        float totDegrees =
            player.finalStats[Stats.Statstype.Spread] +
            (2.1f * (numProjectiles - 1)); //slightly increases spread for each extra bullet
        if (numProjectiles > 1){
            float startDeg = -totDegrees / 2;
            float currentDeg = startDeg;
            for (int i = 0; i < numProjectiles; i++){
                ShootBullet(currentDeg, 0);
                currentDeg += totDegrees / (numProjectiles - 1);
            }
        }
        else{
            float randomAngle = Random.Range(totDegrees / -2, totDegrees / 2);
            ShootBullet(randomAngle, 0);
        }
        //back projectiles:
        
        int numBackProjectiles = Convert.ToInt32(player.finalStats[Stats.Statstype.BackProjectiles]);
        if (numBackProjectiles > 1){
            float totBackDegrees =
                player.finalStats[Stats.Statstype.Spread] +
                (2.5f * (numBackProjectiles - 1)); //slightly increases spread for each extra bullet
            float startDeg = -totBackDegrees / 2;
            float currentDeg = startDeg;
            for (int i = 0; i < numBackProjectiles; i++){
                ShootBullet(currentDeg+180, 0);
                currentDeg += totDegrees / (numBackProjectiles - 1);
            }
        }
        else{
            if (numBackProjectiles > 0.5f){
                float randomAngle = Random.Range(totDegrees / -2, totDegrees / 2);
                ShootBullet(randomAngle+180, 0);
            }
        }
        
        //reduce ammo at end:
        
        if(currentAmmo>=Convert.ToInt32(player.finalStats[Stats.Statstype.AmmoUsage])){
            currentAmmo -= Convert.ToInt32(player.finalStats[Stats.Statstype.AmmoUsage]);
        }else{
            //allows it to shoot on one ammo
            currentAmmo = 0;
        }
    }

    //angle: deviation from forward in degrees, offset: distance from gun sideways
    public void ShootBullet(float angle, float offset){
        //shoot bullet code
        Vector3 rot = gunPos.rotation.eulerAngles + new Vector3(0, 0, angle);
        SpawnBullet(gunPos.position, rot, offset);
    }

    public Bullet SpawnBullet(Vector3 pos, Vector3 rotation, float offset){
        GameObject bul = Instantiate(bullet, pos, Quaternion.Euler(rotation));
        bul.transform.localScale = Vector3.one;
        bul.transform.localScale *= player.finalStats[Stats.Statstype.BulletSize];

        Bullet b = bul.GetComponent<Bullet>();
        b.Init(player.finalStats[Stats.Statstype.Damage],
            player.finalStats[Stats.Statstype.ProjSpeed],
            Convert.ToInt32(player.finalStats[Stats.Statstype.Penetration]));

        OnSpawnBullet?.Invoke( currentAmmo, player, bul.GetComponent<Bullet>());
        
        Destroy(bul, 2f);

        return b;
    }


    IEnumerator flash(){
        yield return new WaitForSeconds(0.06f);
        flashbool = false;
    }

    private bool reloading;

    void Reload(){
        if (currentAmmo < Convert.ToInt32(player.finalStats[Stats.Statstype.AmmoCapacity])){
            //src.PlayOneShot(equipped.reloadSound, 1f);
            reloadElapsed = 0;
            reloading = true;
        }
    }

    public void AddAmmo(int amount){
        currentAmmo += amount;
        if (currentAmmo > Convert.ToInt32(player.finalStats[Stats.Statstype.AmmoCapacity])){
            currentAmmo = Convert.ToInt32(player.finalStats[Stats.Statstype.AmmoCapacity]);
        }
    }
}