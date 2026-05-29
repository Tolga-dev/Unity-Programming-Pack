using System;
using UnityEngine;

namespace Solid.E2
{
    [Serializable]
    public class ExplodingBarrel : MonoBehaviour, IDamageable
    {
        [SerializeField]public float Health { get; set; }
        
        [SerializeField] public float Timeout { get; set; }
        [SerializeField] public float ExplosiveForce { get; set; }


        private void Start()
        {
            Health = 100;
        }

        private void OnTriggerEnter(Collider other)
        {
             TakeDamage();
             if(Health < 0)
                 Die();
        }

        public void Die()
        {
            Debug.Log("Barrel Die");
        }

        public void TakeDamage()
        {
            Debug.Log("Barrel Take Damage");
            Health -= 10;
        }

    }
}