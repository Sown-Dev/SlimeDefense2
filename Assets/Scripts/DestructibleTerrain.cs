    using System;
    using System.Collections;
    using UnityEngine;

    public class DestructibleTerrain: MonoBehaviour, IFriendlyDamagable{
        public float maxHealth = 100;
        private float health;
        
        public SpriteRenderer hitsr;
        private SpriteRenderer mysr;

        private void Awake(){
            mysr = gameObject.GetComponent<SpriteRenderer>();
            hitsr.sprite = mysr.sprite;
            hitsr.color = Color.clear;
            health = maxHealth;
        }


        public void TakeDamage(float dmg){
            health -= dmg;
            StartCoroutine(FlashWhite());
            if (health <= 0){
                Destroy(gameObject);
            }
        }

        public void Heal(float amt){
            health += amt;
            if (health > maxHealth){
                health = maxHealth;
            }
        }

        //coroutine to make the sprite flash white
        private IEnumerator FlashWhite(){
            hitsr.color = Color.white;
            yield return new WaitForSeconds(0.09f);
            hitsr.color = Color.clear;
        }

    }
