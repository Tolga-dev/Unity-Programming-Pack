using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseStates;
using JetBrains.Annotations;
using Managers;
using Skills.AttackSkills;
using Skills.DefenceSkills;
using Skills.MovementSkills;
using UnityEngine;



namespace InstanceNS
{
    interface IEquality<Car>
    {
        bool Equals(Car obj);
        void OverRideCar(Car obj);
    }
     
    internal class Car : IEquality<Car>
    {
        [CanBeNull] public int Make { get; set; } // nullable reference
        [CanBeNull] public int Model { get; set; } // nullable reference
        [CanBeNull] public int Year { get; set; }// nullable reference
        
        public bool Equals([CanBeNull] Car car) // nullable reference
        {
            return (this.Make, this.Model, this.Year) == (car?.Make, car?.Model, car?.Year);
        }

        void IEquality<Car>.OverRideCar(Car obj)
        {
            this.Model = obj.Model;
            this.Make = obj.Make;
            this.Year = obj.Year;
            
        }
    }
}
class Instance : MonoBehaviour
{
    public GameObject _player;
    InstanceNS.Car car1 = new InstanceNS.Car();
    InstanceNS.Car car2 = new InstanceNS.Car();
    void Start()
    {
        
        car1.Make = 1;
        car1.Model = 1;
        car1.Year = 1;

        car2.Make = 1;
        car2.Model = 1;
        car2.Year = 1;
        
        

        Debug.Log(car1.Equals(car2));
        Debug.Log(((InstanceNS.IEquality<InstanceNS.Car>)car1).Equals(car2));
        
    }
}
