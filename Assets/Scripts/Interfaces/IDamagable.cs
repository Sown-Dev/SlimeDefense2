    public interface IDamagable{
        public void TakeDamage(float dmg);
        public void Heal(float amt);

        public float Health{
            get;
            set;
        }
        public float maxHealth{ get; set; }
    }
