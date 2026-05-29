using Observer.E1.Scripts.ObserverExample;

namespace MVP.E1
{
    public class Heath
    {
        public int CurrentHealth { get; set; }
        public int MaxHealth { get; set; }

        public Heath(int maxHealth)
        {
            MaxHealth = maxHealth;
            CurrentHealth = maxHealth;
        }

        public bool TakeDamage(int damage)
        {
            CurrentHealth -= damage;
            if (CurrentHealth < 0)
            {
                CurrentHealth = 0;
                return false;
            }

            return true;
        }



    }
}