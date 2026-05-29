using System;
using TypeObject.E1.Objects;
using TypeObject.E1.Types;
using UnityEngine;

namespace TypeObject.E1
{
    public class GameController : MonoBehaviour
    {
        
        private void Start()
        {
            var canFlyInstance = new CanFly();
            var cantFlyInstance = new CantFly();
            var nameOfFish = "fish";
            var nameOfBird = "bird";
            
            var bird = new Bird(canFlyInstance,ref nameOfFish);
            var fish = new Fish(cantFlyInstance, ref nameOfBird);

            bird.Update();
            fish.Update();
            
            bird.SetFly(cantFlyInstance);
            fish.SetFly(canFlyInstance);
            
            bird.Update();
            fish.Update();

        }
    }
}