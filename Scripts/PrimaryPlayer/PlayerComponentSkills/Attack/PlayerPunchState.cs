using Npc;
using PrimaryPlayer.BaseStates;
using PrimaryPlayer.PlayerComponentManagers.Attack;
using PrimaryPlayer.PlayerComponentManagers.Movement;
using UnityEngine;

namespace PrimaryPlayer.PlayerComponentSkills.Attack
{
    public class PlayerPunchState : PlayerAttackBase
    {

        // combo
        public float timeFrame = 1.5f; // Time frame to track clicks (in seconds)
        private float timer;
        private int clickCount;

        public PlayerPunchState(
            AttackManager attackManager,
            MovementManager movementManager,
            PlayerAttackBase playerAttackBase
        ) : base(attackManager, movementManager, playerAttackBase)
        {

        }

        public override void EnterState(ref AttackManager attackManager)
        {
            Debug.Log("Rifle Entered!");
        }

        public override void UpdateState(ref AttackManager attackManager)
        {

            if (UnityEngine.Input.GetKeyDown(KeyCode.Mouse0))
            {
                clickCount++;
                attackManager.playerAnimator.SetFloat("PunchPower", clickCount);
                Debug.Log("Punch");
            }

            timer += Time.deltaTime;

            if (timer >= timeFrame)
            {
                clickCount = 0;
                attackManager.playerAnimator.SetFloat("PunchPower", clickCount);
                
                timer = 0f;
            }

        }

        public override void ExitState(ref AttackManager attackManager)
        {
            Debug.Log("Rifle Exit!");

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Dummy>() != null)
            {
                var dum = other.GetComponent<Dummy>();
                dum.Health -= 5;
                Debug.Log(" dummy got damage");
                if (dum.Health <= 0)
                {
                    Debug.Log(" dummy died");
                    dum.gameObject.SetActive(false);
                }
            }
        }
    }
}