using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using OverrideExs;
using OverrideExs.E1;
using OverrideExs.E2;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Modifiers : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OverrideE1()
    {
        OverrideExs.E1.Square square = new Square(31);
        Debug.Log(square.GetArea());
    }

    void OverrideE2()
    {
        OverrideExs.E2.Virtual s = new OverrideExs.E2.Virtual(2);
        OverrideExs.E2.Derived s2 = new Derived(2);
        
        Debug.Log(s.GetData());
        Debug.Log(s2.GetData());

    }
}

namespace Exceptions
{
    namespace E1
    {
        class Throw
        {
            
        }
        
    }
    
}


namespace Volatile
{
    namespace E1
    {
        public class Student
        {
            private volatile bool _stop = default;
            
            public void Study()
            {
                while (!_stop)
                {
                    Debug.Log("Working!");
                }
                Debug.Log("Finished!");
            }

            public void FinishedStudy()
            {
                _stop = true;
            }
        }

        public class Class
        {
            public static void Manager()
            {
                Student s = new Student();
                Thread s1 = new Thread(s.Study);
                Thread s2 = new Thread(s.Study);
                Thread s3 = new Thread(s.Study);
                
                s1.Start();
                s2.Start();
                s3.Start();

                while (!s1.IsAlive || !s2.IsAlive || !s3.IsAlive) ;
                
                Thread.Sleep(500);
                
                s.FinishedStudy();

                s1.Join();
                s2.Join();
                s3.Join();


            }
        }
        
                            
    }
}
namespace Static
{
    namespace E1
    {
        public class A
        {
            public static int Data = 31;

            public static int data() => 0;
        }

        internal class B
        {
            private void F()
            {
                var outScope = Static.E1.A.Data | A.data();
                
            }
        }
        
        /*
        internal class B : A //Cannot inherit from sealed class 'A'
        {
                        
        }
        */
        
        
    }
}
namespace Sealed
{
    namespace E1
    {
        class A
        {
            protected virtual void F() { }
            protected virtual void F2() { }
        }

        internal class B : A
        {
            protected sealed override void F(){}
            protected override void F2(){}
        }
        
        internal sealed class Sealed : B
        {
            protected override void F2(){}
        }
            
        /*
        class C : Sealed  //Cannot inherit from sealed class 'Sealed'
        {
            
        }
        */
        
    }
    
}
namespace OverrideExs
{
    namespace E1
    {
        internal abstract class  Shape
        {
            public abstract int GetArea();
        }
        
        internal class Square : Shape
        {
            private readonly int m_Side;
            
            public Square(int side) => m_Side = side;
            
            public override int GetArea() => m_Side * m_Side;
        }
        
    }

    namespace E2
    {
        class Virtual
        {
            private int m_data { get; }
            
            public Virtual(int data)
            {
                m_data = data;
            }

            public virtual int GetData()
            {
                return m_data;
            }
        }

        class Derived : Virtual
        {
            private int m_data { get; }
            
            public Derived(int data) : base(data)
            {
                m_data = data;
            }

            public override int GetData()
            {
                return m_data * m_data;
            }

        }
    }
    
}
