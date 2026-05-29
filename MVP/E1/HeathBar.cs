using System;
using UnityEngine;
using UnityEngine.UI;

namespace MVP.E1
{
    public class HeathBar : MonoBehaviour
    {
        [SerializeField] private Slider healthSlider;

        public void SetHealth(int currentHealth, int maxHealth)
        {
            
            healthSlider.value = currentHealth;
        }
        
    }
}