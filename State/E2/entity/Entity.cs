using State.E2.states.stateMachine;
using UnityEngine;

namespace State.E2.entity
{
    public class Entity : MonoBehaviour
    {
        public bool IsGrounded = false;
        public stateMachine StateMachine;
    }
}