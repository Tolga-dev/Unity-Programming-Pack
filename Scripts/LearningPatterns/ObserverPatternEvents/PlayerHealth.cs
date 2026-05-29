using System;
using UnityEngine;

namespace ObserverPatternEvents
{
    public class PlayerHealth : MonoBehaviour
    {
        public int currentHealth, maxHealth;

        private void Awake()
        {
            currentHealth = maxHealth;
        }

        private void Start()
        {
            EventHandler.PlayerHealthEvent?.Invoke(currentHealth,maxHealth);
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.CompareTag("enemy"))
            {
                currentHealth--;
                EventHandler.PlayerHealthEvent?.Invoke(currentHealth, maxHealth);
            }
        }
    }
}