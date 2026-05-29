using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class PatternManager : MonoBehaviour
    {
        public float health;
        public float walkSpeed;
        private void Start()
        {
            for (int i = 0; i < 100; i++)
            {
                Data newData = new Data();
                health = newData.health;
                walkSpeed = newData.walkSpeed;
                gameObject.SetActive(false);
                Instantiate(gameObject, transform.position, Quaternion.identity);
            }
        }
    }
}