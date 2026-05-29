using State.E2.entity;
using UnityEngine;

namespace State.E2.Player
{
    public class PlayerController : MonoBehaviour
    {
        public Player2 Player2;

        private void Start()
        {
            Player2.StateMachine.Init(Player2.StateMachine.Idle);
        }

        private void Update()
        {
            Player2.StateMachine.Update();
        }
    }
}