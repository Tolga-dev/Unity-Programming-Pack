using System;
using System.Collections;
using PrimaryPlayer.PlayerManager;
using UnityEngine;

namespace AmmoTypes.BasicAmmo
{
    public class SimpleAmmo : MonoBehaviour
    {
        public float defaultDamage = 10;
        public float speed = 50;
        public float netDamage = 0;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                var playerComponent = other.GetComponent<PlayerManager>();
                netDamage = defaultDamage * speed;
                playerComponent.health = playerComponent.health - netDamage;

                if (playerComponent.health <= 0)
                {
                    playerComponent.isPlayerDead = true;
                    playerComponent.animator.SetBool("OnDead",true);
                }

            }
        }
    }
}