    using System;
    using UnityEngine;

    [Serializable]
    public class Weapon{
        public Stats baseStats;
        public bool Auto;
        public string Name;
        [TextArea(15,20)]public string Description;
        public GameObject projectile;
        public Sprite sprite;
    }
