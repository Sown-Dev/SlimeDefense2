﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;
using uRandom = UnityEngine.Random;

public class Utils : MonoBehaviour{
    public static Utils u;
    public GameObject ExplosionPrefab;
    public GameObject BuildingDebrisPrefab;
    void Awake(){
        u = this;
    }
    
    public static bool Random(float chance){
        return UnityEngine.Random.Range(0f, 1f) < chance;
    }

    public void CreateExplosion(Vector2 pos, float force, float damage, float radius){
        GameObject expGO = Instantiate(ExplosionPrefab, pos , Quaternion.identity);
        Explosion exp = expGO.GetComponent<Explosion>();
        Debug.Log("force: "+force+" damage: "+damage+" radius: "+radius);
        exp.damage = damage;
        exp.force = force;
        exp.radius = radius;
        
    }

    public static void AddExplosionForce(Rigidbody2D rb, float explosionForce, Vector2 explosionPosition,
        float explosionRadius, float upwardsModifier = 0.0F, ForceMode2D mode = ForceMode2D.Force){
        var explosionDir = rb.position - explosionPosition;
        var explosionDistance = explosionDir.magnitude;

        // Normalize without computing magnitude again
        if (upwardsModifier == 0)
            explosionDir /= explosionDistance;
        else{
            // From Rigidbody.AddExplosionForce doc:
            // If you pass a non-zero value for the upwardsModifier parameter, the direction
            // will be modified by subtracting that value from the Y component of the centre point.
            explosionDir.y += upwardsModifier;
            explosionDir.Normalize();
        }

        rb.AddForce(Mathf.Lerp(0, explosionForce, (1 - explosionDistance)) * explosionDir, mode);
    }
    
    public static IEnumerable<T> Shuffle<T>(IEnumerable<T> source, Random rng)
    {
        T[] elements = source.ToArray();
        for (int i = elements.Length - 1; i >= 0; i--)
        {
            // Swap element "i" with a random earlier element it (or itself)
            // ... except we don't really need to swap it fully, as we can
            // return it immediately, and afterwards it's irrelevant.
            int swapIndex = rng.Next(i + 1);
            yield return elements[swapIndex];
            elements[swapIndex] = elements[i];
        }
    }
    static Random rng = new Random();

    public static void Shuffle<T>(IList<T> list)  
    {  
        int n = list.Count;  
        while (n > 1) {  
            n--;  
            int k = rng.Next(n + 1);  
            T value = list[k];  
            list[k] = list[n];  
            list[n] = value;  
        }  
    }

    public static Vector2 RandomBetweenBounds(Bounds outside, Bounds inside)
    {
        Vector2 RandomPoint = new Vector2(
            uRandom.Range(outside.min.x, outside.max.x),
            uRandom.Range(outside.min.y, outside.max.y)); //get random
        while (inside.Contains(RandomPoint))
        {
            RandomPoint = new Vector2(
                uRandom.Range(outside.min.x, outside.max.x),
                uRandom.Range(outside.min.y, outside.max.y)); //get random
        }

        return RandomPoint;
    }

    public void CreateDebris(Vector2 pos, Color col){
        GameObject debris = Instantiate(BuildingDebrisPrefab, pos, Quaternion.identity);
        var main= debris.GetComponent<ParticleSystem>().main;
        main.startColor = col;
    }

    public static bool RayCastPoint(Vector2 Pos){
        return RayCastPoint(Pos, Physics2D.AllLayers);
    }

    public static bool RayCastPoint(Vector2 Pos, int layerMask){
        RaycastHit2D hit = Physics2D.Raycast(Pos, Vector2.zero, Mathf.Infinity, layerMask);
        if (hit.collider != null){
            return true;
        }
        else{
            return false;
        }
    }
    
    
}