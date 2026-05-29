using System;
using UnityEngine;

namespace Events.E1.Scripts
{
    public class FunctionDelegates : MonoBehaviour
    {
        private delegate void Foo(Func<int, int, int> func);
        
        private void Start()
        {
            Func<int, int, int> add = (x, y) => x + y;
            Func<int, int, int> mult = (x, y) => x * y;
            Func<int, int, int> add2 = Sum;
            
            Debug.Log(add(1,2));
            Debug.Log(add2(1,2));

            add += add2;
            Debug.Log(add(12,22));

            Foo foo = Sum;
            
            foo(add);
            foo(mult);
            
        }
        
        

        private int Sum(int x, int y) => x + y;

        private void Sum(Func<int, int, int> func)
        {
            Debug.Log(func(1,2));
        }
        
    }
}