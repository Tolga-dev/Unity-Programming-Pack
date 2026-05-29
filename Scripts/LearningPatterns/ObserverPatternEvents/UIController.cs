using System;
using UnityEngine;
using UnityEngine.UI;

namespace ObserverPatternEvents
{
    public class UIController : MonoBehaviour
    {
        public Text healhtText;

        private void Awake()
        {
            EventHandler.PlayerHealthEvent.AddListener(PlayerHealthEvent);
        }

        private void PlayerHealthEvent(int currentHealth, int MaxHealth)
        {
            healhtText.text = currentHealth.ToString();
        }
    }
}