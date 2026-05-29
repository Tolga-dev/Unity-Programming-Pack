using System;
using UnityEngine;

namespace Events.E1.Scripts
{
    public class Delegates : MonoBehaviour
    {
        private delegate void Foo(string msg);
        private delegate void Bar(int c);

        private delegate T Add<T>(T p1, T p2);

        private void Start()
        {
            var foo = new Foo(Method);
            Foo foo2 = Method;
            Foo foo3 = Debug.Log;

            foo.Invoke("method A");
            foo2("Method B");
            foo3("method C");


            foo3 += foo + foo2;
            foo3("method 4");
            
            foo3 -= foo + foo2;
            foo3("method 5");


            Add<int> sum = Sum;
            Add<int> sum2 = Sum;
            
            Debug.Log(sum(31,31));

            sum += sum2;

            Debug.Log(sum(31,31));
        }
        
        

        private void Method(string msg)
        {
            Debug.Log(msg);
        }
         
        public int Sum(int val1, int val2)
        {
            return val1 + val2;
        }

        
        
    }
}