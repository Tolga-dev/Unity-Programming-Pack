using System;
using UnityEngine;

namespace Solid.E2
{
    public interface IDamageable
    {
        [SerializeField] public float Health { get; set; }

        [SerializeField]public void Die();
        [SerializeField] public void TakeDamage();
    }
}