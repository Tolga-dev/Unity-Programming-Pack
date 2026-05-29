using Unity.Collections;
using Unity.Jobs;

namespace Threads.Job
{
    public class MyJob : IJob
    {
        public float a;
        public float b;
        public float result2;
        public void Execute()
        {
            result2 = a + b;
        }
    }
}