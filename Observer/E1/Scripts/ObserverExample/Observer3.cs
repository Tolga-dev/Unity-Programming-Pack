using UnityEngine;

namespace Observer.E1.Scripts.ObserverExample
{
    public class Observer3 : MonoBehaviour
    {
        public int health = 100;
        public int speed = 100;

        private void OnEnable()
        {
            Debug.Log("On Enable");
            Health.TakeHealth += GetHealth;
            Health.TakeSlowness += GetSlow;
        
        }
    
        private void OnDisable()
        {
            Debug.Log("On Disable");
            Health.TakeHealth -= GetHealth;
            Health.TakeSlowness -= GetSlow;
        }

        private void GetHealth()
        {
            health += 10;
        }

        private void GetSlow()
        {
            speed -= 10;
        }

        public void CheckHealth()
        {
            if (health > 200)
            {
                Health.TakeHealth -= GetHealth;
                Debug.Log("max health!");
            }
        }

    }
}
