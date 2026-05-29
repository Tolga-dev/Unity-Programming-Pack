using UnityEngine;

namespace MVP.E1
{
    public class HealthPresenter : MonoBehaviour
    {
        private Heath _health;
        private HeathBar _heathBar;

        public HealthPresenter(Heath health, HeathBar heathBar)
        {
            _health = health;
            _heathBar = heathBar;
        }

        public void TakeDamage(int damage)
        {
            _health.TakeDamage(damage);
            UpdateHealthBar();
        }

        private void UpdateHealthBar()
        {
            _heathBar.SetHealth(_health.CurrentHealth, _health.MaxHealth);
        }
    }
}