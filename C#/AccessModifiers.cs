using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace privateProtected
{
    public class Parent
    {
        private protected int Val = default;
    }

    public class Child : Parent
    {
        public void ShowMe()
        {
            Parent parent = new Parent();

//            parent.Val = 5; protected err
            
            Val = 5;
        }
    }

    public class Main
    {
        private Parent parent = new Parent();
        private Child child = new Child();
        void main()
        {
             // parent.Val = 5; err
             // child.Val = 5; err
             
             child.ShowMe();
        }
        
    }
    ///////////////////
    
    public class FooEpic<T>
    {
        private protected T Val = default;
    }

    public class Epic : FooEpic<int>
    {
        void Access()
        {
            Val = 31;
        }
    }

}

namespace protectedInternal
{
    namespace Assembly1
    {
        public class Parent
        {
            protected internal int Val = default;
            public int Val2 = default;
        }

        public class Child
        {
            void Access()
            {
                Parent parent = new Parent();
                parent.Val = 5;
                
            }
        }
    }

    namespace Assembly2
    {
        class Child : Assembly1.Parent
        {

            void Access()
            {
                Assembly2.Child child = new Assembly2.Child();
                child.Val = 5;

            }

        }
    }
    //////////////////////////////
    public class Point
    {
        protected internal int X = default;
        protected internal int Y = default;
    }

    public class DefaultPoint
    {
        protected int X = default;
        protected int Y = default;
    }

    public class DetectPlace : DefaultPoint
    {
        public void SetDefault() // can only be accessed by derived class 
        {
            DetectPlace detectPlace = new DetectPlace() // protected
            {
                X = 6,
                Y = 6
            }; 
        
            Point point = new Point(); // protected internal 
            point.X = 5;
            point.Y = 5;
               
        }
    }

}

namespace Internal
{
    namespace Base
    {
        internal class BaseClass
        {
            public static int Val = 0;
        }
    }

    namespace Status
    {
        class Test
        {
            static void CheckBase()
            {
             //   var val = new BaseClass(); // // using Internal.Base; to fix

            }
        }
    }
    
    
}

namespace Protected
{
    class A
    {
        protected int Val = 123;
    }

    class B : A
    {
        void Access()
        {
            var a = new A();
            var b = new B();

         //   a.Val = 5;

            b.Val = 5;


        }
    }
}

namespace Abstract 
{
    interface ICoords
    {
        int M(int x, int y);
    
        
    }
    abstract class Shape : ICoords
    {
        internal abstract int getArea();

        public abstract int M(int x, int y);
    }

    class Square : Shape
    {
        private int _side;
        public Square(int n) => _side = n;

        internal override int getArea() => _side * _side;
        
        public override int M(int x,int y) => x*y;
        
        void Main()
        {
            Debug.Log(_side);   
        }
        
    }
}

namespace Async
{
    namespace e1
    {
        public class Async
        {
            public async Task Method1<T>()
            {
                for (int i = 0; i < 100; i++)
                {
                    Debug.Log("enter " + i );

                    Task<T> longTask = LongTask<T>(i);

                    T result = await longTask;
                
                    Debug.Log("exit " + i );

                }

            }

            private async Task<T> LongTask<T>(int i)
            {
            
                await Task.Delay(1000); // wait
                T temp = default;
                Debug.Log(i); // do something while waiting
                return temp;
                
            }
        }
    }

    namespace e2
    {
        class Async
        {
            public void Main<T>()
            {
                example();
                for (int i = 0; i < 10000; i++)
                {
                    Debug.Log("hi");
                }
            }

            async void example()
            {
                for (int i = 0; i < 1000; i++)
                {
                    int t = await Task.Run(() => LongAllocateOperation());
                    Debug.Log(t);
                    
                }
            }

            int LongAllocateOperation()
            {
                int size = default;
                for (int i = 0; i < 100; i++)
                {
                    for (int j = 0; j < 10000; j++)
                    {
                        string val = i.ToString();
                        size += val.Length;
                    }
                    
                }
                return size;

            }
        }
        
    }

    namespace e3
    {
        class Async
        {
            public async Task foo()
            {
                await Task.Run(
                    () =>
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            Debug.Log("1");
                            Task.Delay(100).Wait();
                        }
                    }
                );
            }
            public void bar()
            {
                for (int i = 0; i < 1000; i++)
                {
                    Debug.Log("2");
                }
            }
        }
    }

    namespace e4
    {
        class Async
        {
            async Task Execute()
            {
                Task<int> foo = LongJob();
                Task<int> bar = LongJob2();

                int val = await foo;
                
                Debug.Log(val);

                int val2 = await bar;
                
                Debug.Log(val2);
                
            }
            
            async Task<int> LongJob()
            {
                Debug.Log("Long 1 ");

                await Task.Delay(1000);
                
                Debug.Log("Long 1 comp ");
                
                return 10;
            }       
            async Task<int> LongJob2()
            {
                Debug.Log("Long 2 ");

                await Task.Delay(1000);
                
                Debug.Log("Long 2 comp ");


                return 20;
            }
        }
    }
    
}



public class AccessModifiers : MonoBehaviour
{
   // private Async.e1.Async yes = new Async.e1.Async();
   //  yes.Method1<int>();
  // private Async.e2.Async yes = new Async.e2.Async();
  //  yes.Main<int>();
//      private Async.e3.Async yes = new Async.e3.Async();
//    yes.foo();  
  //  yes.bar();  

    void Start()
    {
  
        
    }
 
}
