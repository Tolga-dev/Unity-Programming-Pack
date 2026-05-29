using System;
using UnityEngine;

namespace Events.E1.Scripts
{
    public class Action : MonoBehaviour
    {
        private delegate void PrintString(string msg);
        
        private delegate void Bar(Func<int, int, int> func);
        
        
        private void Start()
        {
            PrintString printMe = Foo;
            printMe("hello world");

            Action<string> printMe2 = Foo;
            printMe2("hello world");

            Action<Func<int, int, int>> printMe3 = Sum;
            printMe3((i, i1) => i + i1);

        }

        private void Foo(string msg)
        {
            Debug.Log(msg);
        }
        private void Sum(Func<int, int, int> func)
        {
            Debug.Log(func(1,2));
        }
        
    }
}