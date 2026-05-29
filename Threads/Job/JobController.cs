using System;
using System.Collections.Generic;
using UnityEngine;

namespace Threads.Job
{
    public class JobController : MonoBehaviour
    {
        public List<MyJob> MyJobs = new List<MyJob>();

        private void Start()
        {
            for (int i = 0; i < 10; i++)
            {
                MyJobs.Add(new MyJob());
            }
                
            foreach (var job in MyJobs )
            {
                job.Execute();
            }
            
            foreach (var job in MyJobs)
            {
                Debug.Log(job.result2);
            }
        }
    }
}