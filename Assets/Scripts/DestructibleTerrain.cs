    using System;
    using System.Collections;
    using UnityEngine;

    public class DestructibleTerrain: MonoBehaviour, IFriendlyDamagable{
        [field: SerializeField] public float maxHealth{ get; set; }
        public float Health{ get; set; }
        
        public SpriteRenderer hitsr;
        private SpriteRenderer mysr;

        private void Awake(){
            mysr = gameObject.GetComponent<SpriteRenderer>();
            hitsr.sprite = mysr.sprite;
            hitsr.color = Color.clear;
            Health = maxHealth;
        }


        public void TakeDamage(float dmg){
            Health -= dmg;
            StartCoroutine(FlashWhite());
            if (Health <= 0){
                Destroy(gameObject);
            }
        }

        public void Heal(float amt){
            Health += amt;
            if (Health > maxHealth){
                Health = maxHealth;
            }
        }

        //coroutine to make the sprite flash white
        private IEnumerator FlashWhite(){
            hitsr.color = Color.white;
            yield return new WaitForSeconds(0.09f);
            hitsr.color = Color.clear;
        }

    }
