using TypeObject.E1.Types;
using UnityEngine;

namespace TypeObject.E1.Objects
{
    public class Animal
    {
        protected Fly Fly;
        protected string Name;

        protected Animal()
        {
        }
        
        public void SetFly(Fly fly)
        {
            Fly = fly;
        }

        public void Update()
        {
            FlyExecute();
        }

        public void FlyExecute()
        {
             Fly.Execute();
        }
    }

    public class Bird : Animal
    {
        public Bird(Fly fly, ref string name)
        {
            Fly = fly;
            Name = name;
        }
    }
    public class Fish : Animal
    {
        public Fish(Fly fly, ref string name)
        {
            Fly = fly;
            Name = name;
        }
    }
}