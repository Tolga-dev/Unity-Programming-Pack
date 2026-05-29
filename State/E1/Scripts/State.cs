using UnityEngine;

namespace State.E1.Scripts
{
    public class State : MonoBehaviour
    {
        // Start is called before the first frame update
        public GameObject zombieObj;
        private Zombie _zombie;
        void Start()
        {
            _zombie = new Zombie(zombieObj);
        }

        // Update is called once per frame
        void Update()
        {
            _zombie.UpdateStatus();
        
        }
    }

    public class EnemyBase
    {
        protected GameObject Enemy;
        protected enum States
        {
            Attack,
            Walk,
            Jump
        }

        public virtual void UpdateStatus() { }

        protected void CurrentStateUpdate(States states)
        {
            switch (states)
            {
                case States.Attack:
                    Debug.Log("Attack!");
                    break;
                case States.Jump:
                    Debug.Log("Jump!");
                    break;
                case States.Walk:
                    Debug.Log("Walk!");
                    break;
                default:
                    break;        
            }
        }

    }

    public class Zombie : EnemyBase
    {
        private readonly States _states = States.Walk;

        public Zombie(GameObject enemy)
        {
            base.Enemy = enemy;
        }

        public override void UpdateStatus()
        {
            switch (_states)
            {
                case States.Attack:
                    // change state
                    break;
                case States.Jump:
                    break;
                case States.Walk:
                    break;
                default:
                    break;        
            }
            CurrentStateUpdate(_states);
            
        }
    }
}