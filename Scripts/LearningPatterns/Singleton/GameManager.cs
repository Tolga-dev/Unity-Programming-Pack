using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Proporties
{
    private static GameManager _instance = null; 
    private static object _locked = new object();
    public static GameManager Instance
    {
        get
        {
            
            if(_instance == null)
            {
                _instance = new GameManager();
            }

            return _instance;

        }
        
    }

    private void Awake()
    {
        _instance = this;
    }
    
}
