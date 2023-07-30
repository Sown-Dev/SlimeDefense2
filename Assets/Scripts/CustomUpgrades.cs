    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    public class CustomUpgrades: MonoBehaviour{
        
        [Header("<h2>CustomUpgrades</h2>")]
        public BulletExplodeUpgrade exp;
        public PoisonDebuffUpgrade poison;
        
        public List<Upgrade> upgrades = new List<Upgrade>();
        void Awake(){
            upgrades= this.GetType()
                .GetFields()
                .Where(field => field.FieldType is Upgrade || field.FieldType.IsSubclassOf(typeof(Upgrade)))
                .Select(field => field.GetValue(this))
                .Select(field => field as Upgrade)
                .ToList();
        }
    }
