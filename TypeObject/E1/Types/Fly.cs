using System;
using System.Collections.Generic;
using UnityEngine;

namespace TypeObject.E1.Types
{
    public class Fly : ITypes
    {
        public virtual void Execute()
        {
        }
    }

    public class CanFly : Fly
    {
        public override void Execute()
        {
            Debug.Log("Can fly");
        }
    }
    public class CantFly : Fly
    {
        public override void Execute()
        {
            Debug.Log("Cant fly");
        }
    }
}