using UnityEngine;

namespace E1.Scripts
{
    public class Prototype : MonoBehaviour
    {
        private Sheep _animalSheep;
        private Ghost _enemyGhost;
        private readonly KeyCode _keyCodeInput;
    
        private void Start()
        {
            _animalSheep = new Sheep(100,10);
            _enemyGhost = new Ghost(100,10);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                var sheepSpawner = new AnimalSpawner(_animalSheep);
                var newSheep = sheepSpawner.AnimalSpawnTime() as Sheep;
                newSheep!.Instruction();
            
            }
            else if (Input.GetKeyDown(KeyCode.B))
            {
                var ghostSpawner = new EnemySpawner(_enemyGhost);
                var newGhost = ghostSpawner.EnemySpawnTime() as Ghost;
                newGhost!.Instruction();
            } 
        }
    }

    public abstract class Animal
    {
        public abstract Animal Clone();

        public abstract void Instruction();
    }

    public class Sheep : Animal
    {
        private readonly double _health;
        private readonly double _protection;

        public Sheep(double health, double protection)
        {
            this._health = health;
            this._protection = protection;
        }
            
        public override Animal Clone()
        {
            return new Sheep(_health,_protection);
        }
        public override void Instruction()
        {
            Debug.Log("Sheep: Message!");
        }
    }

    public abstract class Enemy
    {
        public abstract Enemy Clone();
        public abstract void Instruction();
    }

    public class Ghost : Enemy
    {
        private readonly double _AttackPower;
        private readonly double _Health;

        public Ghost(double attackPower, double health)
        {
            this._Health = health;
            this._AttackPower = attackPower;
        }

        public override void Instruction()
        {
            Debug.Log("Hi! U re gonna be die!");
        }

        public override Enemy Clone()
        {
            return new Ghost(_AttackPower, _Health);
        }
    }

    public class AnimalSpawner
    {
        private readonly Animal _animal;

        public AnimalSpawner(Animal animal)
        {
            this._animal = animal;
        }

        public Animal AnimalSpawnTime()
        {
            return _animal.Clone();
        }
    }

    public class EnemySpawner
    {
        private Enemy _enemy;

        public EnemySpawner(Enemy animal)
        {
            this._enemy = animal;
        }

        public Enemy EnemySpawnTime()
        {
            return _enemy.Clone();
        }
    }
}