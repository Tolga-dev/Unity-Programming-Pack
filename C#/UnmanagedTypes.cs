using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using InstanceNS;

public struct Coords<T>
{
    public T X;
    public T Y;
    public bool Z;
}

namespace UnManaged
{ 
    class DetectPlace
    { 
        public void DisplayCoords<T>(Coords<T> k) where T : unmanaged
        {
            Debug.Log(typeof(T) + " " + k.X + " " + k.Y + " " + k.Z);
        }
 
    }
    
}
public class UnmanagedTypes : MonoBehaviour
{
    private UnManaged.DetectPlace detect;
    // Start is called before the first frame update
    void Start()
    {
        
        detect = new UnManaged.DetectPlace();
        Coords<int> k;
        k.X = default; // 0
        k.Y = default; // 0
        k.Z = default; // false
        detect.DisplayCoords<int>(k);
    } 
}
