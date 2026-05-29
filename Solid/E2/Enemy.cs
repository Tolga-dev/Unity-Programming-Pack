using UnityEngine;

namespace Solid.E2
{
    public class Enemy : MonoBehaviour, IMovable, IDamageable
    {
        public float Health { get; set; }
        public void Die()
        {
            
        }

        public void TakeDamage()
        {
        }
    }
}